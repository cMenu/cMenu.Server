using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using cMenu.Common.Base;
using cMenu.Common.ErrorHandling;
using cMenu.Metadata;
using cMenu.Metadata.Attributes;
using cMenu.Security;
using cMenu.Security.UsersManagement;
using cMenu.Security.MetaobjectsManagement;
using cMenu.Security.DevicesManagement;
using cMenu.Communication.Server.Environment;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Extended;
using cMenu.Globalization;

namespace cMenu.Communication.Server.Functions
{
    [CMetaFunctionAttribute(CServerFunctionID.CONST_FUNC_ID_AUTHENTICATE, true, false)]
    public class CFunctionAuthenticate : CServerFunction
    {
        #region PROTECTED FIELDS
        #endregion

        #region PROTECTED FUNCTIONS
        protected CFunctionResult _compileResult(EnFunctionResultType Type, Dictionary<string, object> Parameters, string Message)
        {
            CFunctionResult R = new CFunctionResult
            {
                ResultType = Type,
                InputParameters = Parameters,
                Message = Message
            };
            return R;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        /// <summary>
        /// Параметры функции
        /// PARAM_DEVICE_ID - идентификатор устройства        
        /// PARAM_DEVICE_TYPE - тип устройства
        /// PARAM_DEVICE_MAC - MAC адрес устройства        
        /// PARAM_DEVICE_NAME - имя устройства
        /// PARAM_LOGIN - логин пользователя
        /// PARAM_PASSWORD - пароль пользователя
        /// PARAM_SESSION_DEADLINE - дата окончания сессии
        /// PARAM_LOCALE - локаль клиентского устройства
        /// </summary>
        /// <param name="Parameters">Параметры</param>
        /// <returns></returns>
        public override CFunctionResult Execute(Dictionary<string, object> Parameters)
        {
            CFunctionResult Result = new CFunctionResult();
            CultureInfo ClientCulture = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_LOCALE) ? CultureInfo.GetCultureInfo((int)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_LOCALE]) : CultureInfo.CurrentCulture);

            if (!Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_LOGIN))
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_LOGIN_NULL", ClientCulture));
            if (!Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_PASSWORD))
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_PASSWORD_NULL", ClientCulture));
            if (!Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_SESSION_DEADLINE))
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_DEADLINE_NULL", ClientCulture));


            string Login = (string)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_LOGIN];
            string Password = (string)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_PASSWORD];
            string Passhash = CSecurityHelper.sGeneratePasshash(Login, Password);
            DateTime Deadline = (DateTime)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_SESSION_DEADLINE];            

            string DeviceID = "";
            string AuthToken = "";
            CClientDevice Device = null;

            CSystemUser User = CSystemUser.sGetUserByLoginPasshash(CFunctionExecutionEnvironment.sGetCurrentProvider(), Login, Password, Passhash);
            if (User == null)
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_LOGIN_PASS_NULL", ClientCulture));

            User.GetPolicies(CFunctionExecutionEnvironment.sGetCurrentProvider());
            if (User.Status != EnMetaobjectStatus.EEnabled)
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_DISABLED", ClientCulture));
            if (!User.PolicyAllowBindDevices())
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_NOT_ALLOWED_BIND_DEVICES", ClientCulture));
            if (!User.PolicyAllowEditSessionsList())
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_NOT_ALLOWED_CREATE_SESSIONS", ClientCulture));

            if (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_DEVICE_ID))
            {
                DeviceID = (string)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_DEVICE_ID];
                Device = new CClientDevice(Guid.Parse(DeviceID), CFunctionExecutionEnvironment.sGetCurrentProvider());
                if (Device.Key == -1)
                    return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_DEVICE_ID_NULL", ClientCulture));
            }
            else
            {
                if (!User.PolicyAllowEditDevicesList())
                    return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_NOT_ALLOWED_CREATE_DEVICES", ClientCulture));
                if (!Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_DEVICE_NAME))
                    return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_DEVICE_NAME_EMPTY", ClientCulture));
                if (!Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_DEVICE_MAC))
                    return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_DEVICE_MAC_EMPTY", ClientCulture));

                string DeviceName = (string)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_DEVICE_NAME];
                string DeviceMAC = (string)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_DEVICE_MAC];
                DeviceID = Guid.NewGuid().ToString();

                Device = new CClientDevice(CFunctionExecutionEnvironment.sGetCurrentProvider());
                Device.Name = DeviceName;
                Device.ID = Guid.Parse(DeviceID);
                Device.MACAddress = DeviceMAC;
                Device.Type = EnClientDeviceType.EExternal;
                Device.Parent = CEmbeddedObjectsConsts.CONST_FOLDER_DEVICES_KEY;
                var R = Device.ObjectInsert(CFunctionExecutionEnvironment.sGetCurrentProvider());
                if (R != -1)
                    return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_NOT_ALLOWED_CREATE_DEVICES", ClientCulture));

                List<CSystemUserGroup> Groups = CSystemUserGroup.sGetAllGroups(CFunctionExecutionEnvironment.sGetCurrentProvider());
                foreach (CSystemUserGroup Group in Groups)
                {
                    var Rights = 1;
                    if (Group.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_ID)
                    {
                        Rights = 3;
                    }
                    if (Group.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_ID)
                    {
                        Rights = 3;
                    }

                    CMetaobjectSecurityRecord Record = new CMetaobjectSecurityRecord()
                    {
                        MetaobjectKey = Device.Key,
                        Rights = Rights,
                        UserKey = Group.Key
                        
                    };
                    R = Record.RecordInsert(CFunctionExecutionEnvironment.sGetCurrentProvider());
                    if (R != -1)
                        return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_NOT_ALLOWED_CREATE_SEC_RECORD", ClientCulture));
                }
            }

            AuthToken = CSecurityHelper.sGenerateAuthToken(DeviceID, Login, Password);
            CClientDeviceSession Session = new CClientDeviceSession();
            Session.DeadLine = Deadline;
            Session.DeviceKey = Device.Key;
            Session.UserKey = User.Key;
            Session.ID = Guid.NewGuid();
            Session.AuthToken = AuthToken;
            if (Session.SessionInsert(CFunctionExecutionEnvironment.sGetCurrentProvider()) != -1)
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_NOT_ALLOWED_CREATE_SESSIONS", ClientCulture));

            Result.InputParameters = Parameters;
            Result.ResultType = EnFunctionResultType.ESuccess;
            Result.Message = CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_AUTHENTICATED", ClientCulture);
            Result.Content = "DeviceID=" + DeviceID + ";AuthToken=" + AuthToken + ";SessionDeadline=" + Deadline.ToString("G");
            return Result;
        }
        #endregion
    }
}
