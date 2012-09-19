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
        protected CFunctionResult _userEdit(string UserIdentity, string Passhash, Guid SessionID, string JSON)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "UserEdit",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "UserEdit";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CSystemUser User = (CSystemUser)(UserResult.Content as object[])[0];

            CSystemUser UpdatedUser = JSON.ToDataStream().DeserializeJSONStream<CSystemUser>(typeof(CSystemUser));
            if (UpdatedUser == null)
            {
                R = CServerHelper.sCompileFunctionResult("UserEdit", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_USER_INPUT_PARAMS_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            User.Attributes.Context = CServerEnvironment.DataContext;

            if (User.Key != UpdatedUser.Key || User.ID != UpdatedUser.ID)
            {
                R = CServerHelper.sCompileFunctionResult("UserEdit", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_EDIT_IDENTITY_NOT_EQUAL", CultureInfo.CurrentCulture), null);
                return R;
            }

            CFunctionResult TempResult = null;
            if (User.Login.Trim() != UpdatedUser.Login.Trim())
            {
                TempResult = this._userCheckLoginExistence(UpdatedUser.Login.Trim());
                if (TempResult.ResultType != Communication.EnFunctionResultType.ESuccess && User.Key != (TempResult.Content as CSystemUser).Key)
                {
                    R = CServerHelper.sCompileFunctionResult("UserEdit", Communication.EnFunctionResultType.EError, InputParameters, TempResult.Message, TempResult.Content);
                    return R;
                }
            }
            if (User.Email.Trim() != UpdatedUser.Email.Trim())
            {
                TempResult = this._userCheckEmailExistence(UpdatedUser.Email.Trim());
                if (TempResult.ResultType != Communication.EnFunctionResultType.ESuccess && User.Key != (TempResult.Content as CSystemUser).Key)
                {
                    R = CServerHelper.sCompileFunctionResult("UserEdit", Communication.EnFunctionResultType.EError, InputParameters, TempResult.Message, TempResult.Content);
                    return R;
                }
            }
            if (User.MobilePhone.Trim() != UpdatedUser.MobilePhone.Trim())
            {
                TempResult = this._userCheckPhoneExistence(UpdatedUser.MobilePhone.Trim());
                if (TempResult.ResultType != Communication.EnFunctionResultType.ESuccess && User.Key != (TempResult.Content as CSystemUser).Key)
                {
                    R = CServerHelper.sCompileFunctionResult("UserEdit", Communication.EnFunctionResultType.EError, InputParameters, TempResult.Message, TempResult.Content);
                    return R;
                }
            }

            User.Address = UpdatedUser.Address;
            User.Email = UpdatedUser.Email;
            User.FirstName = UpdatedUser.FirstName;
            User.FullDescription = UpdatedUser.FullDescription;
            User.HomePhone = UpdatedUser.HomePhone;
            User.Login = UpdatedUser.Login;
            User.MobilePhone = UpdatedUser.MobilePhone;
            User.Name = UpdatedUser.Name;
            User.Photo = UpdatedUser.Photo;
            User.SecondName = UpdatedUser.SecondName;
            User.ShortDescription = UpdatedUser.ShortDescription;
            User.Surname = UpdatedUser.Surname;
            User.WorkPhone = UpdatedUser.WorkPhone;

            User.ObjectUpdate(CServerEnvironment.DataContext);

            try
            { CServerEnvironment.DataContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                R = CServerHelper.sCompileFunctionResult("UserEdit", Communication.EnFunctionResultType.EError, InputParameters, Exception.Message, null);
                return R;
            }

            R.Content = User;

            return R;
        }
        protected CFunctionResult _userRegister(string Login, string Email, string Phone, string Password)
        {
            var InputParameters = new Dictionary<string, object>();
            InputParameters.Add("Login", Login);
            InputParameters.Add("Email", Email);
            InputParameters.Add("Phone", Phone);
            InputParameters.Add("Password", Password);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "UserRegister",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            CFunctionResult TempResult = null;

            if (Login.Trim() == "" && Email.Trim() == "" && Phone.Trim() == "")
            {
                R = CServerHelper.sCompileFunctionResult("UserRegister", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_USER_INPUT_PARAMS_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            if (Login.Trim() != "")
            {
                TempResult = this._userCheckLoginExistence(Login.Trim());
                if (TempResult.ResultType != Communication.EnFunctionResultType.ESuccess)
                {
                    R = CServerHelper.sCompileFunctionResult("UserRegister", Communication.EnFunctionResultType.EError, InputParameters, TempResult.Message, TempResult.Content);
                    return R;
                }
            }
            if (Email.Trim() != "")
            {
                TempResult = this._userCheckEmailExistence(Email.Trim());
                if (TempResult.ResultType != Communication.EnFunctionResultType.ESuccess)
                {
                    R = CServerHelper.sCompileFunctionResult("UserRegister", Communication.EnFunctionResultType.EError, InputParameters, TempResult.Message, TempResult.Content);
                    return R;
                }
            }
            if (Phone.Trim() != "")
            {
                TempResult = this._userCheckPhoneExistence(Phone.Trim());
                if (TempResult.ResultType != Communication.EnFunctionResultType.ESuccess)
                {
                    R = CServerHelper.sCompileFunctionResult("UserRegister", Communication.EnFunctionResultType.EError, InputParameters, TempResult.Message, TempResult.Content);
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
            {
                R = CServerHelper.sCompileFunctionResult("UserRegister", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_INSERT_UNABLE", CultureInfo.CurrentCulture), null);
                return R;
            }

            try
            { CServerEnvironment.DataContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                R = CServerHelper.sCompileFunctionResult("UserRegister", Communication.EnFunctionResultType.EError, InputParameters, Exception.Message, null);
                return R;
            }

            cMenu.Metaobjects.Linq.CMetaobjectLink L = new cMenu.Metaobjects.Linq.CMetaobjectLink()
            {
                LinkedObjectKey = User.Key,
                LinkType = EnMetaobjectLinkType.ESecurity,
                LinkValue = 1,
                SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY
            };

            RR = L.LinkInsert(CServerEnvironment.DataContext);
            if (RR != CErrors.ERR_SUC)
            {
                /// Empty resource
                R = CServerHelper.sCompileFunctionResult("UserRegister", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("", CultureInfo.CurrentCulture), null);
                return R;
            }

            try
            { CServerEnvironment.DataContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                /// Empty resource
                return CServerHelper.sCompileFunctionResult("UserRegister", Communication.EnFunctionResultType.EError, InputParameters, Exception.Message, null);
            }

            R.Content = User;

            return R;
        }
        protected CFunctionResult _userLogout(string UserIdentity, string Passhash, Guid SessionID)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "UserLogout",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "UserLogout";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CSystemUser User = (CSystemUser)UserResult.Content;

            var Sessions = CSystemUserSession.sGetSessionsByUser(User.Key, CServerEnvironment.DataContext);
            foreach (CSystemUserSession Session in Sessions)
            {
                if (Session.Type == EnSessionType.ETablet)
                    Session.Status = EnSessionStatus.EClosed;
                var RR = Session.SessionUpdate(CServerEnvironment.DataContext);
                if (RR != CErrors.ERR_SUC)
                {
                    R = CServerHelper.sCompileFunctionResult("UserLogout", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SESSION_UPDATE_UNABLE", CultureInfo.CurrentCulture), null);
                    return R;
                }
            }

            try
            { CServerEnvironment.DataContext.SubmitChanges(); }
            catch (Exception)
            {
                R = CServerHelper.sCompileFunctionResult("UserLogout", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SESSION_UPDATE_UNABLE", CultureInfo.CurrentCulture), null);
                return R;
            }

            return R;
        }
        protected CFunctionResult _userLogin(string UserIdentity, string Password, Guid TableID)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("IdentityKey", UserIdentity);
            InputParameters.Add("Password", Password);
            InputParameters.Add("TableID", TableID);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "UserLogin",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            CFunctionResult UserResult = CServerHelper.sCheckUserByPassword(UserIdentity, Password);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "UserLogout";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CSystemUser User = (CSystemUser)UserResult.Content;

            Guid TempGUID = TableID;

            COrganizationTable Table = new COrganizationTable(TempGUID, CServerEnvironment.DataContext);
            if (Table.Key == CDBConst.CONST_OBJECT_EMPTY_KEY)
            {
                R = CServerHelper.sCompileFunctionResult("UserLogin", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_TABLE_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            var Links = Table.GetExternalLinks(CServerEnvironment.DataContext);
            if (Links.Count == 0 || Links.Count > 1)
            {
                R = CServerHelper.sCompileFunctionResult("UserLogin", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_ORG_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            cMenu.Metaobjects.Linq.CMetaobject Organization = Links[0].GetSourceObject(CServerEnvironment.DataContext);
            if (Organization == null)
            {
                R = CServerHelper.sCompileFunctionResult("UserLogin", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_ORG_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
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
                                Ses.DeadLine > DateTime.Now
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
                    Session.VariablesDictionary.Add(CSecurityConsts.CONST_SESSION_TABLE_ID, Table.ID);
                    Session.SessionVariablesSave();

                    var RR = Session.SessionInsert(CServerEnvironment.DataContext);
                    if (RR != CErrors.ERR_SUC)
                    {
                        R = CServerHelper.sCompileFunctionResult("UserLogin", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SESSION_INSERT_UNABLE", CultureInfo.CurrentCulture), null);
                        return R;
                    }
                }
                else
                    Session.SessionVariablesLoad();
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
                Session.VariablesDictionary.Add(CSecurityConsts.CONST_SESSION_TABLE_ID, Table.ID);
                Session.SessionVariablesSave();

                var RR = Session.SessionInsert(CServerEnvironment.DataContext);
                if (RR != CErrors.ERR_SUC)
                {
                    R = CServerHelper.sCompileFunctionResult("UserLogin", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SESSION_INSERT_UNABLE", CultureInfo.CurrentCulture), null);
                    return R;
                }
            }

            try
            { CServerEnvironment.DataContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                R = CServerHelper.sCompileFunctionResult("UserLogin", Communication.EnFunctionResultType.EError, InputParameters, Exception.Message, null);
                return R;
            }

            var Content = new object[3];
            Content[0] = User;
            Content[1] = Session;
            Content[2] = Organization;

            R.Content = Content;

            return R;
        }
        protected CFunctionResult _userGetInformation(string UserIdentity, string Passhash, Guid SessionID, decimal Key)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string,object>();
            InputParameters.Add("Key", Key);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "UserGetInformation",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "UserLogout";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CSystemUser User = new CSystemUser(Key, CServerEnvironment.DataContext);
            if (User.ID == Guid.Empty || User.Class != EnMetaobjectClass.ESystemUser)
                R = CServerHelper.sCompileFunctionResult("UserGetInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
            else
                R = CServerHelper.sCompileFunctionResult("UserGetInformation", Communication.EnFunctionResultType.ESuccess, InputParameters, "", User);

            return R;
        }
        protected CFunctionResult _userGetInformation(string UserIdentity, string Passhash, Guid SessionID, Guid ID)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("ID", ID);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "UserGetInformation",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "UserLogout";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CSystemUser User = new CSystemUser(ID, CServerEnvironment.DataContext);
            if (User.Key == CDBConst.CONST_OBJECT_EMPTY_KEY|| User.Class != EnMetaobjectClass.ESystemUser)
                R = CServerHelper.sCompileFunctionResult("UserGetInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
            else
                R = CServerHelper.sCompileFunctionResult("UserGetInformation", Communication.EnFunctionResultType.ESuccess, InputParameters, "", User);

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
                ResultType = Communication.EnFunctionResultType.ESuccess,
                Message = CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture)
            };

            var User = CSystemUser.sGetUserByLogin(Login, CServerEnvironment.DataContext);
            if (User != null)
                R = CServerHelper.sCompileFunctionResult("UserCheckLoginExistence", Communication.EnFunctionResultType.ESuccess, InputParameters, "", User);

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
                R = CServerHelper.sCompileFunctionResult("UserCheckEmailExistence", Communication.EnFunctionResultType.ESuccess, InputParameters, "", User);

            return R;
        }
        protected CFunctionResult _userCheckPhoneExistence(string Phone)
        {
            var InputParameters = new Dictionary<string, object>();
            InputParameters.Add("Phone", Phone);

            var R = new CFunctionResult()
            {
                FunctionID = "UserCheckPhoneExistence",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            var User = CSystemUser.sGetUserByMobilePhone(Phone, CServerEnvironment.DataContext);
            if (User != null)
                R = CServerHelper.sCompileFunctionResult("UserCheckPhoneExistence", Communication.EnFunctionResultType.ESuccess, InputParameters, "", User);

            return R;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public string Register(string Login, string Email, string Mobile, string Password)
        {
            try
            {
                var R = this._userRegister(Login, Email, Mobile, Password);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("Login", Login);
                InputParameters.Add("Email", Email);
                InputParameters.Add("Mobile", Mobile);
                InputParameters.Add("Password", Password);

                var R = CServerHelper.sCompileFunctionResult("UserRegister", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        public string Edit(string UserIdentity, string Passhash, Guid SessionID, string JSON)
        {
            try
            {
                var R = this._userEdit(UserIdentity, Passhash, SessionID, JSON);
                var JSONResult = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSONResult;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("JSON", JSON);

                var R = CServerHelper.sCompileFunctionResult("UserEdit", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSONResult = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSONResult;
            }
            
        }
        public string GetUserByKey(string UserIdentity, string Passhash, Guid? SessionID, decimal? Key)
        {
            try
            {
                if (Key == null)
                    return "";

                var R = this._userGetInformation(UserIdentity, Passhash, (Guid)SessionID, (decimal)Key);
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

                var R = CServerHelper.sCompileFunctionResult("UserGetInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSONResult = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSONResult;
            }            
        }
        public string GetUserByID(string UserIdentity, string Passhash, Guid? SessionID, Guid? ID)
        {
            try
            {
                if (ID == null)
                    return "";

                var R = this._userGetInformation(UserIdentity, Passhash, (Guid)SessionID, (Guid)ID);
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

                var R = CServerHelper.sCompileFunctionResult("UserGetInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        public string Login(string UserIdentity, string Password, Guid? TableID)
        {
            try
            {
                var R = this._userLogin(UserIdentity, Password, (Guid)TableID);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Password", Password);
                InputParameters.Add("TableID", (TableID == null ? "" : TableID.ToString().ToUpper()));

                var R = CServerHelper.sCompileFunctionResult("UserLogin", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        public string Logout(string UserIdentity, string Passhash, Guid SessionID)
        {
            try
            {
                var R = this._userLogout(UserIdentity, Passhash, SessionID);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("TableID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));

                var R = CServerHelper.sCompileFunctionResult("UserLogout", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }

        public string CheckLoginExistence(string Login)
        {
            try
            {
                var R = this._userCheckLoginExistence(Login);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("Login", Login);

                var R = CServerHelper.sCompileFunctionResult("UserCheckLoginExistence", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        public string CheckEmailExistence(string Email)
        {
            try
            {
                var R = this._userCheckEmailExistence(Email);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("Email", Email);

                var R = CServerHelper.sCompileFunctionResult("UserCheckEmailExistence", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        public string CheckPhoneExistence(string Phone)
        {
            try
            {
                var R = this._userCheckPhoneExistence(Phone);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("Phone", Phone);

                var R = CServerHelper.sCompileFunctionResult("UserCheckPhoneExistence", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
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
