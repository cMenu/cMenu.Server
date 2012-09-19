using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Communication;
using cMenu.Communication.Server;
using cMenu.Security;
using cMenu.Security.Linq.UsersManagement;
using cMenu.Common;

namespace cMenu.Web.Server.Tablet.Common
{
    public class CServerHelper
    {
        #region STATIC FUNCTIONS
        public static CFunctionResult sCompileFunctionResult(string FunctionID, EnFunctionResultType Type, Dictionary<string, object> Parameters, string Message, object Content)
        {
            CFunctionResult R = new CFunctionResult
            {
                ResultType = Type,
                InputParameters = Parameters,
                Message = Message,
                Content = Content,
                FunctionID = FunctionID
            };
            return R;
        }
        public static CFunctionResult sCheckUser(string UserIdentity, string Passhash, Guid SessionID)
        {
            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "CheckUser",
                ResultType = EnFunctionResultType.ESuccess
            };

            if (UserIdentity == null || Passhash == null || SessionID == null)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_USER_INPUT_PARAMS_NULL", null);
                return R;
            }
            if (UserIdentity.Trim() == "")
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_USER_INPUT_PARAMS_NULL", null);
                return R;
            }

            CSystemUser User = null;

            User = CSystemUser.sGetUserByLogin(UserIdentity.Trim(), CServerEnvironment.DataContext);
            if (User == null)
            {
                User = CSystemUser.sGetUserByEmail(UserIdentity.Trim(), CServerEnvironment.DataContext);
                if (User == null)
                    User = CSystemUser.sGetUserByMobilePhone(UserIdentity.Trim(), CServerEnvironment.DataContext);
            }

            if (User == null)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_OBJECT_UNABLE_TO_FIND", null);
                return R;
            }

            var Verified = (Passhash == User.Passhash);
            if (!Verified)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_USER_BAD_IDENTITY_OR_PASS", null);
                return R;
            }

            CSystemUserSession Session = new CSystemUserSession() { ID = SessionID };
            var RR = Session.SessionGetByID(SessionID, CServerEnvironment.DataContext);
            if (RR != CErrors.ERR_SUC)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_SESSION_NULL", null);
                return R;
            }

            if (Session.UserKey != User.Key)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_SESSION_KEYS_NOT_EQUALS", null);
                return R;
            }

            if (Session.DeadLine <= DateTime.Now)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_SESSION_DEADLINE_EXPIRES", null);
                return R;
            }

            if (Session.Status != EnSessionStatus.EEnabled)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_SESSION_NOT_ENABLED", null);
                return R;
            }

            var Content = new object[2];
            Content[0] = User;
            Content[1] = Session;

            R.Content = Content;

            return R;
        }
        public static CFunctionResult sCheckUser(string UserIdentity, string Passhash)
        {
            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "CheckUser",
                ResultType = EnFunctionResultType.ESuccess
            };

            if (UserIdentity == null || Passhash == null)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_USER_INPUT_PARAMS_NULL", null);
                return R;
            }
            if (UserIdentity.Trim() == "")
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_USER_INPUT_PARAMS_NULL", null);
                return R;
            }

            CSystemUser User = null;

            User = CSystemUser.sGetUserByLogin(UserIdentity.Trim(), CServerEnvironment.DataContext);
            if (User == null)
            {
                User = CSystemUser.sGetUserByEmail(UserIdentity.Trim(), CServerEnvironment.DataContext);
                if (User == null)
                    User = CSystemUser.sGetUserByMobilePhone(UserIdentity.Trim(), CServerEnvironment.DataContext);
            }

            if (User == null)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_OBJECT_UNABLE_TO_FIND", null);
                return R;
            }

            var Verified = (Passhash == User.Passhash);
            if (!Verified)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_USER_BAD_IDENTITY_OR_PASS", null);
                return R;
            }

            R.Content = User;

            return R;
        }
        public static CFunctionResult sCheckUserByPassword(string UserIdentity, string Password)
        {
            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "CheckUser",
                ResultType = EnFunctionResultType.ESuccess
            };

            if (UserIdentity == null || Password == null)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_USER_INPUT_PARAMS_NULL", null);
                return R;
            }
            if (UserIdentity.Trim() == "")
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_USER_INPUT_PARAMS_NULL", null);
                return R;
            }

            CSystemUser User = null;

            User = CSystemUser.sGetUserByLogin(UserIdentity.Trim(), CServerEnvironment.DataContext);
            if (User == null)
            {
                User = CSystemUser.sGetUserByEmail(UserIdentity.Trim(), CServerEnvironment.DataContext);
                if (User == null)
                    User = CSystemUser.sGetUserByMobilePhone(UserIdentity.Trim(), CServerEnvironment.DataContext);
            }

            if (User == null)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_OBJECT_UNABLE_TO_FIND", null);
                return R;
            }

            var Verified = CSecurityHelper.sVerifyPasshash(User.Login, Password, User.Passhash);
            if (!Verified)
            {
                R = CServerHelper.sCompileFunctionResult("CheckUser", Communication.EnFunctionResultType.EError, null, "ERROR_USER_BAD_IDENTITY_OR_PASS", null);
                return R;
            }

            R.Content = User;

            return R;
        }
        #endregion
    }
}
