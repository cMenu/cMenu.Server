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
    public class UserController : Controller
    {
        #region PROTECTED FUNCTIONS
        protected CFunctionResult _userLogout(string Login, string Email, string Phone, string Passhash, string TableID)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("Login", Login);
            InputParameters.Add("Email", Email);
            InputParameters.Add("Phone", Phone);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("TableID", TableID);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "UserLogin",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (Login.Trim() == "" && Email == "" && Phone == "")
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = null;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
                return R;
            }

            CSystemUser User = null;

            if (Login.Trim() != "")
                User = CSystemUser.sGetUserByLogin(Login.Trim(), CServerEnvironment.DataContext);
            else if (Email.Trim() != "")
                User = CSystemUser.sGetUserByEmail(Email.Trim(), CServerEnvironment.DataContext);
            else if (Phone.Trim() != "")
                User = CSystemUser.sGetUserByMobilePhone(Phone.Trim(), CServerEnvironment.DataContext);

            if (User == null)
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = null;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
                return R;
            }

            var Verified = (Passhash == User.Passhash);
            if (!Verified)
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = null;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
                return R;
            }

            var Sessions = CSystemUserSession.sGetSessionsByUser(User.Key, CServerEnvironment.DataContext);
            foreach (CSystemUserSession Session in Sessions)
            {
                Session.Status = EnSessionStatus.EClosed;
                Session.SessionUpdate(CServerEnvironment.DataContext);
            }

            CServerEnvironment.DataContext.SubmitChanges();

            return R;
        }
        protected CFunctionResult _userLogin(string Login, string Email, string Phone, string Password, string TableID)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("Login", Login);
            InputParameters.Add("Email", Email);
            InputParameters.Add("Phone", Phone);
            InputParameters.Add("Password", Password);
            InputParameters.Add("TableID", TableID);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "UserLogin",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (Login.Trim() == "" && Email == "" && Phone == "")
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = null;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
                return R;
            }

            CSystemUser User = null;

            if (Login.Trim() != "")            
                User = CSystemUser.sGetUserByLogin(Login.Trim(), CServerEnvironment.DataContext);
            else if (Email.Trim() != "")
                User = CSystemUser.sGetUserByEmail(Email.Trim(), CServerEnvironment.DataContext);
            else if (Phone.Trim() != "")
                User = CSystemUser.sGetUserByMobilePhone(Phone.Trim(), CServerEnvironment.DataContext);

            if (User == null)
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = null;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
                return R;
            }

            var Verified = CSecurityHelper.sVerifyPasshash(Login.Trim(), Password.Trim(), User.Passhash);
            if (!Verified)
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = null;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
                return R;
            }

            COrganizationTable Table = new COrganizationTable(Guid.Parse(TableID), CServerEnvironment.DataContext);
            if (Table.Key == CDBConst.CONST_OBJECT_EMPTY_KEY)
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = null;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
                return R;
            }

            var Links = Table.GetExternalLinks(CServerEnvironment.DataContext);
            if (Links.Count == 0 || Links.Count > 1)
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = null;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
                return R;
            }

            COrganization Organization = (COrganization)Links[0].GetSourceObject(CServerEnvironment.DataContext);
            if (Organization == null)
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = null;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
                return R;
            }

            var Sessions = CSystemUserSession.sGetSessionsByUser(User.Key, CServerEnvironment.DataContext);
            CSystemUserSession Session = null;

            if (Sessions.Count != 0)            
            {
                var Query = from Ses in Sessions
                            where 
                                Ses.Status == EnSessionStatus.EEnabled &&
                                Ses.Type == EnSessionType.ETablet &&
                                Ses.DeadLine < DateTime.Now
                            select Ses;
                Session = (Query.Count() == 0 ? null : Query.ToList()[0]);

                if (Session == null)
                {
                    Session = new CSystemUserSession()
                    {
                        ID = Guid.NewGuid(),
                        Key = CDatabaseSequence.sGetObjectKey(CServerEnvironment.DatabaseProvider),
                        Status = EnSessionStatus.EEnabled,
                        Type = EnSessionType.ETablet,
                        UserKey = User.Key,
                        Variables = new byte[0],
                        DeadLine = DateTime.Now.AddHours(8)
                    };
                    Session.SessionInsert(CServerEnvironment.DataContext);
                }
            }
            else
            {
                Session = new CSystemUserSession()
                {
                    ID = Guid.NewGuid(),
                    Key = CDatabaseSequence.sGetObjectKey(CServerEnvironment.DatabaseProvider),
                    Status = EnSessionStatus.EEnabled,
                    Type = EnSessionType.ETablet,
                    UserKey = User.Key,
                    Variables = new byte[0],
                    DeadLine = DateTime.Now.AddHours(8)
                };
                Session.SessionInsert(CServerEnvironment.DataContext);
            }

            var RR = Session.SessionInsert(CServerEnvironment.DataContext);
            if (RR != CErrors.ERR_SUC)
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = null;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
                return R;
            }

            CServerEnvironment.DataContext.SubmitChanges();

            R.Content = string.Format("{0}|{1}|{2}|{3}|{4}", User.Passhash, User.Login, Session.ID.ToString().ToUpper(), Organization.Key, User.Key);
            return R;
        }
        protected CFunctionResult _userGetInformation(decimal Key)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string,object>();
            InputParameters.Add("Key", Key);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "UserGetInformation",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            CSystemUser User = new CSystemUser(Key, CServerEnvironment.DataContext);
            if (User.ID == Guid.Empty)
            {
                R.Content = null;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
                R.ResultType = Communication.EnFunctionResultType.EError;
            }
            else
            {
                R.Content = User;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);
            }

            return R;
        }
        protected CFunctionResult _userRegistrate(string Login, string Email, string Phone, string Password)
        {
            var InputParameters = new Dictionary<string, object>();
            InputParameters.Add("Login", Login);
            InputParameters.Add("Email", Email);
            InputParameters.Add("Phone", Phone);
            InputParameters.Add("Password", Password);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "UserRegistrate",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            CFunctionResult TempResult = null;

            if (Login.Trim() != "")
            {
                TempResult = this._userCheckLoginExistence(Login.Trim());
                if (TempResult.Content != null)
                {
                    R.ResultType = Communication.EnFunctionResultType.EError;
                    R.Content = TempResult.Content;
                    R.Message = TempResult.Message;
                    return R;
                }
            }
            if (Email.Trim() != "")
            {
                TempResult = this._userCheckEmailExistence(Email.Trim());
                if (TempResult.Content != null)
                {
                    R.ResultType = Communication.EnFunctionResultType.EError;
                    R.Content = TempResult.Content;
                    R.Message = TempResult.Message;
                    return R;
                }
            }
            if (Phone.Trim() != "")
            {
                TempResult = this._userCheckPhoneExistence(Phone.Trim());
                if (TempResult.Content != null)
                {
                    R.ResultType = Communication.EnFunctionResultType.EError;
                    R.Content = TempResult.Content;
                    R.Message = TempResult.Message;
                    return R;
                }
            }

            CSystemUser User = new CSystemUser(CServerEnvironment.DataContext)
            {
                Email = Email,
                Login = Login,
                MobilePhone = Phone,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_USERS_KEY,
                Passhash = CSecurityHelper.sGeneratePasshash(Login.Trim(), Password.Trim()),
                Status = EnMetaobjectStatus.EEnabled,
                System = false,
                SystemType = EnSystemUserType.EUser,
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(CServerEnvironment.DatabaseProvider)
            };

            var RR = User.ObjectInsert(CServerEnvironment.DataContext);
            if (RR != CErrors.ERR_SUC)
                return R;

            CServerEnvironment.DataContext.SubmitChanges();

            cMenu.Metaobjects.Linq.CMetaobjectLink L = new cMenu.Metaobjects.Linq.CMetaobjectLink()
            {
                LinkedObjectKey = User.Key,
                LinkType = EnMetaobjectLinkType.ESecurity,
                LinkValue = 1,
                SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY
            };

            RR = L.LinkInsert(CServerEnvironment.DataContext);
            if (RR != CErrors.ERR_SUC)
                return R;

            CServerEnvironment.DataContext.SubmitChanges();

            return R;
        }
        protected CFunctionResult _userCheckLoginExistence(string Login)
        {
            var InputParameters = new Dictionary<string, object>();
            InputParameters.Add("Login", Login);

            var R = new CFunctionResult()
            {
                FunctionID = "UserCheckLoginExistence",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            var User = CSystemUser.sGetUserByLogin(Login, CServerEnvironment.DataContext);
            if (User != null)
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = User;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);                
            }

            return R;
        }
        protected CFunctionResult _userCheckEmailExistence(string Email)
        {
            var InputParameters = new Dictionary<string, object>();
            InputParameters.Add("Email", Email);

            var R = new CFunctionResult()
            {
                FunctionID = "UserCheckEmailExistence",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            var User = CSystemUser.sGetUserByEmail(Email, CServerEnvironment.DataContext);
            if (User != null)
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = User;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);                
            }

            return R;
        }
        protected CFunctionResult _userCheckPhoneExistence(string Phone)
        {
            var InputParameters = new Dictionary<string, object>();
            InputParameters.Add("Phone", Phone);

            var R = new CFunctionResult()
            {
                FunctionID = "UserCheckEmailExistence",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            var User = CSystemUser.sGetUserByMobilePhone(Phone, CServerEnvironment.DataContext);
            if (User != null)
            {
                R.ResultType = Communication.EnFunctionResultType.EError;
                R.Content = User;
                /// Empty resource
                R.Message = CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture);                
            }

            return R;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public string Register(string Login, string Email, string Mobile, string Password)
        {
            var R = this._userRegistrate(Login, Email, Mobile, Password);
            var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();

            return JSON;
        }
        public string GetUser(decimal Key)
        {
            var R = this._userGetInformation(Key);
            var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();

            return JSON;
        }
        public string UserLogin(string Login, string Email, string Phone, string Password, string TableID)
        {
            var R = this._userLogin(Login, Email, Phone, Password, TableID);
            var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();

            return JSON;
        }
        public string UserLogout(string Login, string Email, string Phone, string Passhash, string TableID)
        {
            var R = this._userLogout(Login, Email, Phone, Passhash, TableID);
            var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();

            return JSON;
        }

        public string CheckLoginExistence(string Login)
        {
            var R = this._userCheckLoginExistence(Login);
            var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();

            return JSON;
        }
        public string CheckEmailExistence(string Email)
        {
            var R = this._userCheckEmailExistence(Email);
            var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();

            return JSON;
        }
        public string CheckPhoneExistence(string Phone)
        {
            var R = this._userCheckPhoneExistence(Phone);
            var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();

            return JSON;
        }
        #endregion

        public string Index(decimal? Key)
        {
            if (Key == null)
                return "";

            return this.GetUser((decimal)Key);
        }
    }
}
