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
using cMenu.Metaobjects.Extended.Linq;
using cMenu.Metaobjects.Extended.Linq.Menu;
using cMenu.Security;
using cMenu.Security.Linq.UsersManagement;
using cMenu.Globalization;
using cMenu.IO;
using cMenu.Common;
using cMenu.DB;

namespace cMenu.Web.Server.Tablet.Controllers
{
    public class MenuController : Controller
    {
        #region PROTECTED FUNCTIONS
        protected CFunctionResult _getMenuActive(string UserIdentity, string Passhash, Guid SessionID, decimal OrganizationKey)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());
            InputParameters.Add("OrganizationKey", OrganizationKey);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "GetMenuActive",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (OrganizationKey == null)
            {
                R = CServerHelper.sCompileFunctionResult("GetMenuActive", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_KEY_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }


            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "GetMenuActive";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            COrganization Organization = new COrganization(OrganizationKey, CServerEnvironment.DataContext);
            if (Organization.ID == Guid.Empty)
            {
                R = CServerHelper.sCompileFunctionResult("GetMenuActive", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            var Menus = Organization.GetChildren(CServerEnvironment.DataContext, false);
            CMenu ActiveMenu = null;
            foreach (CMenu Menu in Menus)
            {
                if (Menu.Primary)
                {
                    ActiveMenu = Menu;
                    break;
                }
            }

            if (ActiveMenu == null)
            {
                R = CServerHelper.sCompileFunctionResult("GetMenuActive", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_ACTIVE_MENU_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            ActiveMenu.GetChildren(CServerEnvironment.DataContext, false);
            R.Content = ActiveMenu;

            return R;
        }
        protected CFunctionResult _getMenuActive(string UserIdentity, string Passhash, Guid SessionID, Guid OrganizationID)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());
            InputParameters.Add("OrganizationID", OrganizationID);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "GetMenuActive",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (OrganizationID == null)
            {
                R = CServerHelper.sCompileFunctionResult("GetMenuActive", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("Не указан идентификатор объекта", CultureInfo.CurrentCulture), null);
                return R;
            }


            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "GetMenuActive";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            COrganization Organization = new COrganization(OrganizationID, CServerEnvironment.DataContext);
            if (Organization.Key == CDBConst.CONST_OBJECT_EMPTY_KEY)
            {
                R = CServerHelper.sCompileFunctionResult("GetMenuActive", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            var Menus = Organization.GetChildren(CServerEnvironment.DataContext, false);
            CMenu ActiveMenu = null;
            foreach (CMenu Menu in Menus)
            {
                if (Menu.Primary)
                {
                    ActiveMenu = Menu;
                    break;
                }
            }

            if (ActiveMenu == null)
            {
                R = CServerHelper.sCompileFunctionResult("GetMenuActive", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_ACTIVE_MENU_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            ActiveMenu.GetChildren(CServerEnvironment.DataContext, false);
            R.Content = ActiveMenu;

            return R;
        }

        protected CFunctionResult _getCategoryInformation(string UserIdentity, string Passhash, Guid SessionID, decimal CategoryKey)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());
            InputParameters.Add("CategoryKey", CategoryKey);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "GetCategoryInformation",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (CategoryKey == null)
            {
                R = CServerHelper.sCompileFunctionResult("GetCategoryInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_KEY_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "GetCategoryInformation";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CCategory Category = new CCategory(CategoryKey, CServerEnvironment.DataContext);
            if (Category.ID == Guid.Empty)
            {
                R = CServerHelper.sCompileFunctionResult("GetCategoryInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            Category.GetChildren(CServerEnvironment.DataContext, false);
            R.Content = Category;
            return R;
        }
        protected CFunctionResult _getCategoryInformation(string UserIdentity, string Passhash, Guid SessionID, Guid CategoryID)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());
            InputParameters.Add("CategoryID", CategoryID);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "GetCategoryInformation",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (CategoryID == null)
            {
                R = CServerHelper.sCompileFunctionResult("GetCategoryInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_ID_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "GetCategoryInformation";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CCategory Category = new CCategory(CategoryID, CServerEnvironment.DataContext);
            if (Category.Key == CDBConst.CONST_OBJECT_EMPTY_KEY)
            {
                R = CServerHelper.sCompileFunctionResult("GetCategoryInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            Category.GetChildren(CServerEnvironment.DataContext, false);
            R.Content = Category;
            return R;
        }

        protected CFunctionResult _getServiceInformation(string UserIdentity, string Passhash, Guid SessionID, decimal ServiceKey)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());
            InputParameters.Add("ServiceKey", ServiceKey);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "GetServiceInformation",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (ServiceKey == null)
            {
                R = CServerHelper.sCompileFunctionResult("GetServiceInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_KEY_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "GetServiceInformation";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CMetaobjectShortcut ServiceShortcut = new CMetaobjectShortcut(ServiceKey, CServerEnvironment.DataContext);
            if (ServiceShortcut.ID == Guid.Empty)
            {
                R = CServerHelper.sCompileFunctionResult("GetServiceInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            ServiceKey = ServiceShortcut.SourceObjectKey;

            CMenuService Service = new CMenuService(ServiceKey, CServerEnvironment.DataContext);
            if (Service.ID == Guid.Empty)
            {
                R = CServerHelper.sCompileFunctionResult("GetServiceInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            Service.GetChildren(CServerEnvironment.DataContext, false);
            R.Content = Service;

            return R;
        }
        protected CFunctionResult _getServiceInformation(string UserIdentity, string Passhash, Guid SessionID, Guid ServiceID)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());
            InputParameters.Add("ServiceID", ServiceID);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "GetServiceInformation",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (ServiceID == null)
            {
                R = CServerHelper.sCompileFunctionResult("GetServiceInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_ID_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "GetServiceInformation";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CMetaobjectShortcut ServiceShortcut = new CMetaobjectShortcut(ServiceID, CServerEnvironment.DataContext);
            if (ServiceShortcut.Key == CDBConst.CONST_OBJECT_EMPTY_KEY)
            {
                R = CServerHelper.sCompileFunctionResult("GetServiceInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            var ServiceKey = ServiceShortcut.SourceObjectKey;

            CMenuService Service = new CMenuService(ServiceKey, CServerEnvironment.DataContext);
            if (Service.ID == Guid.Empty)
            {
                R = CServerHelper.sCompileFunctionResult("GetServiceInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            Service.GetChildren(CServerEnvironment.DataContext, false);
            R.Content = Service;

            return R;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public string GetActiveMenuByKey(string UserIdentity, string Passhash, Guid? SessionID, decimal? OrganizationKey)
        {
            try
            {
                var R = this._getMenuActive(UserIdentity, Passhash, (Guid)SessionID, (decimal)OrganizationKey);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("OrganizationKey", OrganizationKey);

                var R = CServerHelper.sCompileFunctionResult("GetMenuActive", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
        }
        public string GetActiveMenuByID(string UserIdentity, string Passhash, Guid? SessionID, Guid? OrganizationID)
        {
            try
            {
                var R = this._getMenuActive(UserIdentity, Passhash, (Guid)SessionID, (Guid)OrganizationID);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("OrganizationID", (OrganizationID == null ? "" : OrganizationID.ToString().ToUpper()));

                var R = CServerHelper.sCompileFunctionResult("GetMenuActive", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }

        public string GetCategoryInformationByKey(string UserIdentity, string Passhash, Guid? SessionID, decimal? CategoryKey)
        {
            try
            {
                var R = this._getCategoryInformation(UserIdentity, Passhash, (Guid)SessionID, (decimal)CategoryKey);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("CategoryKey", CategoryKey);

                var R = CServerHelper.sCompileFunctionResult("GetCategoryInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        public string GetCategoryInformationByID(string UserIdentity, string Passhash, Guid? SessionID, Guid? CategoryID)
        {
            try
            {
                var R = this._getCategoryInformation(UserIdentity, Passhash, (Guid)SessionID, (Guid)CategoryID);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {                
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("CategoryID", (CategoryID == null ? "" : CategoryID.ToString().ToUpper()));

                var R = CServerHelper.sCompileFunctionResult("GetCategoryInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }

        public string GetServiceInformationByKey(string UserIdentity, string Passhash, Guid? SessionID, decimal? ServiceKey)
        {
            try
            {
                var R = this._getCategoryInformation(UserIdentity, Passhash, (Guid)SessionID, (decimal)ServiceKey);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("ServiceKey", ServiceKey);

                var R = CServerHelper.sCompileFunctionResult("GetServiceInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        public string GetServiceInformationByID(string UserIdentity, string Passhash, Guid? SessionID, Guid? ServiceID)
        {
            try
            {
                var R = this._getCategoryInformation(UserIdentity, Passhash, (Guid)SessionID, (Guid)ServiceID);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("ServiceID", (ServiceID == null ? "" : ServiceID.ToString().ToUpper()));

                var R = CServerHelper.sCompileFunctionResult("GetServiceInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
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
