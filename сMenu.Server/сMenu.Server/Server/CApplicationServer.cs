using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.ServiceModel.Web;

using cMenu.Common.Base;
using cMenu.Common.ErrorHandling;
using cMenu.IO;
using cMenu.Metadata;
using cMenu.Metadata.Attributes;
using cMenu.Communication.Server.Configuration;
using cMenu.Communication.Server.Environment;
using cMenu.Security;
using cMenu.Security.DevicesManagement;
using cMenu.Security.UsersManagement;
using cMenu.Security.PoliciesManagement;
using cMenu.Security.MetaobjectsManagement;
using cMenu.Globalization;

namespace cMenu.Communication.Server
{
    public class CApplicationServer : CBaseEntity, IApplicationServer
    {
        #region PROTECTED FIELDS
        protected static List<IServerFunction> _functions = new List<IServerFunction>();
        #endregion

        #region PUBLIC FIELDS
        public static CApplicationServerConfiguration Configuration
        {
            get
            {
                return CFunctionExecutionEnvironment.ApplicationServerConfiguration;
            }
            set
            {
                CFunctionExecutionEnvironment.ApplicationServerConfiguration = value;
            }
        }
        public static List<IServerFunction> Functions
        {
            get { return _functions; }
            set { _functions = value; }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected object _formatResult(CFunctionResult Result, EnFunctionResultFormat Format)
        {
            object R = "";
            Stream S = null;
            switch (Format)
            {
                case EnFunctionResultFormat.EBinary:
                    S = Result.SerializeBinaryStream();
                    R = S.ToDataByteArray();
                    break;
                case EnFunctionResultFormat.EXML:
                    S = Result.SerializeXMLStream(typeof(CFunctionResult));
                    R = S.ToDataString();
                    break;
                case EnFunctionResultFormat.EJSON:
                    S = Result.SerializeJSONStream();
                    R = S.ToDataString();
                    break;
            }

            return R;
        }
        protected Stream _formatStreamJSONResult(CFunctionResult Result)
        {
            string JSON = null;
            var Stream = Result.SerializeJSONStream();
            JSON = Stream.ToDataString();
            byte[] resultBytes = Encoding.Default.GetBytes(JSON);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
            return new MemoryStream(resultBytes);
        }
        protected CFunctionResult _checkUserPermissions(string FunctionID, Dictionary<string, object> Parameters)
        {
            CFunctionResult Result = new CFunctionResult();
            CultureInfo ClientCulture = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_LOCALE) ? CultureInfo.GetCultureInfo((int)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_LOCALE]) : CultureInfo.CurrentCulture);

            string DeviceID = (string)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_DEVICE_ID];
            string DeviceMAC = (string)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_DEVICE_MAC];
            string AuthToken = (string)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_AUTH_TOKEN];

            if (AuthToken == "" || AuthToken == string.Empty)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.InputParameters = Parameters;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_AUTH_TOKEN_NULL", ClientCulture);
                return Result;
            }

            CClientDevice Device = new CClientDevice(Guid.Parse(DeviceID), CFunctionExecutionEnvironment.sGetCurrentProvider());
            if (Device.Key == -1)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.InputParameters = Parameters;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_DEVICE_ID_NULL", ClientCulture);
                return Result;
            }
            if (Device.Status != Metaobjects.EnMetaobjectStatus.EEnabled)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.InputParameters = Parameters;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_DEVICE_BANNED", ClientCulture);
                return Result;
            }

            CClientDeviceSession Session = CClientDeviceSession.sGetSessionBytokenAndDevice(Device.Key, AuthToken, CFunctionExecutionEnvironment.sGetCurrentProvider());
            if (Session == null)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.InputParameters = Parameters;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_SESSION_NULL", ClientCulture);
                return Result;
            }
            if (Session.DeadLine <= DateTime.Now)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.InputParameters = Parameters;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_SESSION_EXPIRED", ClientCulture);
                return Result;
            }

            CSystemUser User = new CSystemUser(Session.UserKey, CFunctionExecutionEnvironment.sGetCurrentProvider());
            if (User.ID == Guid.Empty)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.InputParameters = Parameters;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_SESSION_NULL", ClientCulture);
                return Result;
            }
            if (User.Status != Metaobjects.EnMetaobjectStatus.EEnabled)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.InputParameters = Parameters;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_BANNED", ClientCulture);
                return Result;
            }

            CFunctionExecutionEnvironment.CurrentUser = User;

            Result.ResultType = EnFunctionResultType.ESuccess;

            return Result;
        }
        #endregion

        #region PUBLIC FUNCTION
        public Stream ExecuteFunctionFromWeb(string Parameters)
        {
            IServerFunction Function = null;
            CFunctionResult Result = new CFunctionResult();            

            var ParametersStream = Parameters.ToDataStream();
            if (ParametersStream == null)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_APP_PARAMS_JSON", CultureInfo.CurrentCulture);
                return this._formatStreamJSONResult(Result);
            }

            Dictionary<string, object> ParametersDictionary = ParametersStream.DeserializeJSONStream<Dictionary<string, object>>(typeof(Dictionary<string, object>));            
            if (ParametersDictionary == null)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_APP_PARAMS_JSON", CultureInfo.CurrentCulture);
                return this._formatStreamJSONResult(Result);
            }

            CultureInfo ClientCulture = (ParametersDictionary.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_LOCALE) ? CultureInfo.GetCultureInfo((int)ParametersDictionary[CServerFunctionParams.CONST_FUNC_PARAM_LOCALE]) : CultureInfo.CurrentCulture);
            string FunctionID = (string)ParametersDictionary[CServerFunctionParams.CONST_FUNC_PARAM_FUNC_ID];

            Guid TempFunctionID = Guid.NewGuid();
            if (!Guid.TryParse(FunctionID, out TempFunctionID))
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_SERVER_APP_ID", ClientCulture);
                Result.InputParameters = ParametersDictionary;
                return this._formatStreamJSONResult(Result);
            }

            CServerFunctionConfiguration FunctionConfig = CServerFunctionConfiguration.sFindByID(CApplicationServer.Configuration.Functions, TempFunctionID);
            if (FunctionConfig == null)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_SERVER_CONFIG_FILE", ClientCulture);
                Result.InputParameters = ParametersDictionary;
                return this._formatStreamJSONResult(Result);
            }
            if (!FunctionConfig.Enabled)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_SERVER_APP_DISABLED", ClientCulture);
                Result.InputParameters = ParametersDictionary;
                return this._formatStreamJSONResult(Result);
            }

            if (FunctionConfig.Internal)
                Function = CFunctionExecutionEnvironment.sGetInternalFunction(FunctionID, "IServerFunction");
            else
                Function = CFunctionExecutionEnvironment.sGetExternalFunction(CFunctionExecutionEnvironment.ApplicationServerConfiguration.FunctionsPath + FunctionConfig.Path, FunctionConfig.ModuleFileName, "IServerFunction");

            var Meta = CMetadataHelper.sGetFunctionMetadata(Function.GetType());
            if (Meta.NeedAuthentication)
            {
                Result = this._checkUserPermissions(FunctionID, ParametersDictionary);
                if (Result.ResultType == EnFunctionResultType.EError)
                    return this._formatStreamJSONResult(Result);
            }

            if (Function == null)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_SERVER_APP_NULL", ClientCulture);
                Result.InputParameters = ParametersDictionary;
                return this._formatStreamJSONResult(Result);
            }

            try
            {
                Function.Initialize(ParametersDictionary);
                Result = Function.Execute(ParametersDictionary);
            }
            catch (Exception ex)
            {
                CErrorHelper.sLogException(ex);
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_SERVER_APP_INTERNAL_ERROR", ClientCulture);
                Result.Message = ex.Message;
                Result.InputParameters = ParametersDictionary;
            }

            return this._formatStreamJSONResult(Result);
        }
        public object ExecuteFunction(string Parameters, int Format = 2)
        {
            IServerFunction Function = null;
            CFunctionResult Result = new CFunctionResult();

            var ParametersStream = Parameters.ToDataStream();
            if (ParametersStream == null)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_APP_PARAMS_JSON", CultureInfo.CurrentCulture);
                return this._formatResult(Result, EnFunctionResultFormat.EJSON);
            }

            Dictionary<string, object> ParametersDictionary = ParametersStream.DeserializeJSONStream<Dictionary<string, object>>(typeof(Dictionary<string, object>));
            if (ParametersDictionary == null)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_APP_PARAMS_JSON", CultureInfo.CurrentCulture);
                return this._formatResult(Result, EnFunctionResultFormat.EJSON);
            }

            CultureInfo ClientCulture = (ParametersDictionary.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_LOCALE) ? CultureInfo.GetCultureInfo((int)ParametersDictionary[CServerFunctionParams.CONST_FUNC_PARAM_LOCALE]) : CultureInfo.CurrentCulture);
            string FunctionID = (string)ParametersDictionary[CServerFunctionParams.CONST_FUNC_PARAM_FUNC_ID];

            try
            { 
                EnFunctionResultFormat TempFormat = (EnFunctionResultFormat)Format; 
            }
            catch (Exception ex)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Message = ex.Message;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_RESULT_FORMAT_TYPE", ClientCulture);
                Result.InputParameters = ParametersDictionary;
                return this._formatResult(Result, EnFunctionResultFormat.EJSON);
            }

            Guid TempFunctionID = Guid.NewGuid();
            if (!Guid.TryParse(FunctionID, out TempFunctionID))
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_SERVER_APP_ID", ClientCulture);
                Result.InputParameters = ParametersDictionary;
                return this._formatResult(Result, (EnFunctionResultFormat)Format);
            }

            CServerFunctionConfiguration FunctionConfig = CServerFunctionConfiguration.sFindByID(CApplicationServer.Configuration.Functions, TempFunctionID);
            if (FunctionConfig == null)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_SERVER_CONFIG_FILE", ClientCulture);
                Result.InputParameters = ParametersDictionary;
                return this._formatResult(Result, (EnFunctionResultFormat)Format);
            }
            if (!FunctionConfig.Enabled)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_SERVER_APP_DISABLED", ClientCulture);
                Result.InputParameters = ParametersDictionary;
                return this._formatResult(Result, (EnFunctionResultFormat)Format);
            }

            if (FunctionConfig.Internal)
                Function = CFunctionExecutionEnvironment.sGetInternalFunction(FunctionID, "IServerFunction");
            else
                Function = CFunctionExecutionEnvironment.sGetExternalFunction(CFunctionExecutionEnvironment.ApplicationServerConfiguration.FunctionsPath + FunctionConfig.Path, FunctionConfig.ModuleFileName, "IServerFunction");

            var Meta = CMetadataHelper.sGetFunctionMetadata(Function.GetType());
            if (Meta.NeedAuthentication)
            {
                Result = this._checkUserPermissions(FunctionID, ParametersDictionary);
                if (Result.ResultType == EnFunctionResultType.EError)
                    return this._formatResult(Result, (EnFunctionResultFormat)Format);
            }

            if (Function == null)
            {
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_SERVER_APP_NULL", ClientCulture);
                Result.InputParameters = ParametersDictionary;
                return this._formatResult(Result, (EnFunctionResultFormat)Format);
            }

            try
            {
                Function.Initialize(ParametersDictionary);
                Result = Function.Execute(ParametersDictionary);
            }
            catch (Exception ex)
            {
                CErrorHelper.sLogException(ex);
                Result.ResultType = EnFunctionResultType.EError;
                Result.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_SERVER_APP_INTERNAL_ERROR", ClientCulture);
                Result.Message = ex.Message;
                Result.InputParameters = ParametersDictionary;
            }

            return this._formatResult(Result, (EnFunctionResultFormat)Format);
        }
        #endregion
    }
}
