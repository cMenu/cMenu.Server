using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

using cMenu.Web.Server.Tablet.Common;
using cMenu.Communication.Server;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Linq;
using cMenu.Metaobjects.Extended.Linq.Menu;
using cMenu.Security;
using cMenu.Security.Linq.UsersManagement;
using cMenu.Globalization;
using cMenu.IO;
using cMenu.Common;
using cMenu.DB;

namespace cMenu.Web.Server.Tablet.Controllers
{
    public class OrganizationController : Controller
    {
        #region PROTECTED FUNCTIONS
        protected CFunctionResult _organizationGetInformation(string UserIdentity, string Passhash, Guid SessionID, decimal Key)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());
            InputParameters.Add("Key", Key);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "OrganizationGetInformation",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (Key == null)
            {
                R = CServerHelper.sCompileFunctionResult("OrganizationGetInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_KEY_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "OrganizationGetInformation";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            COrganization Organization = new COrganization(Key, CServerEnvironment.DataContext);
            if (Organization.ID == Guid.Empty)
            {
                R = CServerHelper.sCompileFunctionResult("OrganizationGetInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            R.Content = Organization;

            return R;
        }
        protected CFunctionResult _organizationGetInformation(string UserIdentity, string Passhash, Guid SessionID, Guid ID)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());
            InputParameters.Add("ID", ID);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "OrganizationGetInformation",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (ID == null)
            {
                R = CServerHelper.sCompileFunctionResult("OrganizationGetInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_ID_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "OrganizationGetInformation";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            COrganization Organization = new COrganization(ID, CServerEnvironment.DataContext);
            if (Organization.Key == CDBConst.CONST_OBJECT_EMPTY_KEY)
            {
                R = CServerHelper.sCompileFunctionResult("OrganizationGetInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            R.Content = Organization;

            return R;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public string GetOrganizationByKey(string UserIdentity, string Passhash, Guid? SessionID, decimal? Key)
        {
            try
            {
                var R = this._organizationGetInformation(UserIdentity, Passhash, (Guid)SessionID, (decimal)Key);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("Key", Key);

                var R = CServerHelper.sCompileFunctionResult("OrganizationGetInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        public string GetOrganizationByID(string UserIdentity, string Passhash, Guid? SessionID, Guid? ID)
        {
            try
            {
                var R = this._organizationGetInformation(UserIdentity, Passhash, (Guid)SessionID, (Guid)ID);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("ID", (ID == null ? "" : ID.ToString().ToUpper()));

                var R = CServerHelper.sCompileFunctionResult("OrganizationGetInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        #endregion

        public string Index()
        {
            return "";
        }
    }
}
