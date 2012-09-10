using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Linq;
using cMenu.Security.Linq.MetaobjectsManagement;
using cMenu.Security.Linq.PoliciesManagement;

using Newtonsoft.Json;

namespace cMenu.Security.Linq.UsersManagement
{
    public static class CSystemUserEx
    {
        public static bool PolicyAllowEditUserList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowViewUserList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_VIEW_USERS_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowEditDevicesList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_EDIT_DEVICES_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowViewDevicesList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_VIEW_DEVICES_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowBindDevices(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_BIND_DEVICES_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowEditSessionsList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowViewSessionsList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowEditObjectsList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_EDIT_OBJECTS_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowViewObjectsList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowViewOrdersList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowEditOrdersList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_EDIT_ORDERS_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowViewCommentsList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_VIEW_COMMENTS_ID).ToList().Count != 0);
            return R;
        }
        public static bool PolicyAllowEditCommentsList(this CSystemUser User)
        {
            bool R = false;
            R = (User.Policies.Where(P => P.ID.ToString().ToUpper() == CEmbeddedSecurityConsts.CONST_POLICY_EDIT_COMMENTS_ID).ToList().Count != 0);
            return R;
        }
    }

    [Serializable]
    public class CSystemUser : cMenu.Metaobjects.Linq.CMetaobject
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<EnSystemUserType> _sysTypeAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<EnSystemUserType>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_SYS_TYPE };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<int> _entTypeAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<int>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_ENT_TYPE };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _loginAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_LOGIN };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _passhashAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_PASSHASH };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]> _photoAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_PHOTO };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _emailAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_MAIL };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _homePhoneAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_HOME_PHONE };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _mobilePhoneAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_MOB_PHONE };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _workPhoneAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_WORK_PHONE };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _firstNameAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_NAME };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _secNameAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_SEC_NAME };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _surNameAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_SUR_NAME };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _addressAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_ADDR };

        protected List<CSystemPolicy> _policies = new List<CSystemPolicy>();
        protected List<CSystemUserSession> _sessions = new List<CSystemUserSession>();
        protected List<CMetaobjectSecurityRecord> _securityRecords = new List<CMetaobjectSecurityRecord>();
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<EnSystemUserType> SystemTypeAttribute
        {
            get { return _sysTypeAttribute; }
            set { _sysTypeAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<int> EnterpriseTypeAttribute
        {
            get { return _entTypeAttribute; }
            set { _entTypeAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> LoginAttribute
        {
            get { return _loginAttribute; }
            set { _loginAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> PasshashAttribute
        {
            get { return _passhashAttribute; }
            set { _passhashAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]> PhotoAttribute
        {
            get { return _photoAttribute; }
            set { _photoAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> EmailAttribute
        {
            get { return _emailAttribute; }
            set { _emailAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> HomePhoneAttribute
        {
            get { return _homePhoneAttribute; }
            set { _homePhoneAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> MobilePhoneAttribute
        {
            get { return _mobilePhoneAttribute; }
            set { _mobilePhoneAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> WorkPhoneAttribute
        {
            get { return _workPhoneAttribute; }
            set { _workPhoneAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> FirstNameAttribute
        {
            get { return _firstNameAttribute; }
            set { _firstNameAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> SecondNameAttribute
        {
            get { return _secNameAttribute; }
            set { _secNameAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> SurnameAttribute
        {
            get { return _surNameAttribute; }
            set { _surNameAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> AddressAttribute
        {
            get { return _addressAttribute; }
            set { _addressAttribute = value; }
        }

        public int EnterpriseType
        {
            get { return _entTypeAttribute.DefaultValue; }
            set { _entTypeAttribute.DefaultValue = value; }
        }
        public EnSystemUserType SystemType
        {
            get { return _sysTypeAttribute.DefaultValue; }
            set { _sysTypeAttribute.DefaultValue = value; }
        }
        public string Login
        {
            get { return _loginAttribute.DefaultValue; }
            set { _loginAttribute.DefaultValue = value; }
        }
        public string Passhash
        {
            get { return _passhashAttribute.DefaultValue; }
            set { _passhashAttribute.DefaultValue = value; }
        }
        public byte[] Photo
        {
            get { return _photoAttribute.DefaultValue; }
            set { _photoAttribute.DefaultValue = value; }
        }
        public string Email
        {
            get { return _emailAttribute.DefaultValue; }
            set { _emailAttribute.DefaultValue = value; }
        }
        public string HomePhone
        {
            get { return _homePhoneAttribute.DefaultValue; }
            set { _homePhoneAttribute.DefaultValue = value; }
        }
        public string MobilePhone
        {
            get { return _mobilePhoneAttribute.DefaultValue; }
            set { _mobilePhoneAttribute.DefaultValue = value; }
        }
        public string WorkPhone
        {
            get { return _workPhoneAttribute.DefaultValue; }
            set { _workPhoneAttribute.DefaultValue = value; }
        }
        public string FirstName
        {
            get { return _firstNameAttribute.DefaultValue; }
            set { _firstNameAttribute.DefaultValue = value; }
        }
        public string SecondName
        {
            get { return _secNameAttribute.DefaultValue; }
            set { _secNameAttribute.DefaultValue = value; }
        }
        public string Surname
        {
            get { return _surNameAttribute.DefaultValue; }
            set { _surNameAttribute.DefaultValue = value; }
        }
        public string Address
        {
            get { return _addressAttribute.DefaultValue; }
            set { _addressAttribute.DefaultValue = value; }
        }

        public List<CSystemUserSession> Sessions
        {
            get { return _sessions; }
            set { _sessions = value; }
        }
        public List<CMetaobjectSecurityRecord> SecurityRecords
        {
            get { return _securityRecords; }
            set { _securityRecords = value; }
        }
        public List<CSystemPolicy> Policies
        {
            get { return _policies; }
            set { _policies = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CSystemUser()
            : base()
        {
            this._sysTypeAttribute.Attributes = this._attributes;
            this._entTypeAttribute.Attributes = this._attributes;
            this._loginAttribute.Attributes = this._attributes;
            this._passhashAttribute.Attributes = this._attributes;
            this._photoAttribute.Attributes = this._attributes;
            this._emailAttribute.Attributes = this._attributes;
            this._homePhoneAttribute.Attributes = this._attributes;
            this._mobilePhoneAttribute.Attributes = this._attributes;
            this._workPhoneAttribute.Attributes = this._attributes;
            this._firstNameAttribute.Attributes = this._attributes;
            this._secNameAttribute.Attributes = this._attributes;
            this._surNameAttribute.Attributes = this._attributes;
            this._addressAttribute.Attributes = this._attributes;
        }
        public CSystemUser(DataContext Context)
            : base(Context)
        {
            this._sysTypeAttribute.Attributes = this._attributes;
            this._entTypeAttribute.Attributes = this._attributes;
            this._loginAttribute.Attributes = this._attributes;
            this._passhashAttribute.Attributes = this._attributes;
            this._photoAttribute.Attributes = this._attributes;
            this._emailAttribute.Attributes = this._attributes;
            this._homePhoneAttribute.Attributes = this._attributes;
            this._mobilePhoneAttribute.Attributes = this._attributes;
            this._workPhoneAttribute.Attributes = this._attributes;
            this._firstNameAttribute.Attributes = this._attributes;
            this._secNameAttribute.Attributes = this._attributes;
            this._surNameAttribute.Attributes = this._attributes;
            this._addressAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ESystemUser;
            this.EnterpriseType = -1;
            this.SystemType = EnSystemUserType.EUser;
            this.Login = "";
            this.Passhash = "";
            this.Photo = null;
            this.Email = "";
            this.HomePhone = "";
            this.MobilePhone = "";
            this.WorkPhone = "";
            this.FirstName = "";
            this.SecondName = "";
            this.Surname = "";
            this.Address = "";
        }
        public CSystemUser(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._sysTypeAttribute.Attributes = this._attributes;
            this._entTypeAttribute.Attributes = this._attributes;
            this._loginAttribute.Attributes = this._attributes;
            this._passhashAttribute.Attributes = this._attributes;
            this._photoAttribute.Attributes = this._attributes;
            this._emailAttribute.Attributes = this._attributes;
            this._homePhoneAttribute.Attributes = this._attributes;
            this._mobilePhoneAttribute.Attributes = this._attributes;
            this._workPhoneAttribute.Attributes = this._attributes;
            this._firstNameAttribute.Attributes = this._attributes;
            this._secNameAttribute.Attributes = this._attributes;
            this._surNameAttribute.Attributes = this._attributes;
            this._addressAttribute.Attributes = this._attributes;
        }
        public CSystemUser(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._sysTypeAttribute.Attributes = this._attributes;
            this._entTypeAttribute.Attributes = this._attributes;
            this._loginAttribute.Attributes = this._attributes;
            this._passhashAttribute.Attributes = this._attributes;
            this._photoAttribute.Attributes = this._attributes;
            this._emailAttribute.Attributes = this._attributes;
            this._homePhoneAttribute.Attributes = this._attributes;
            this._mobilePhoneAttribute.Attributes = this._attributes;
            this._workPhoneAttribute.Attributes = this._attributes;
            this._firstNameAttribute.Attributes = this._attributes;
            this._secNameAttribute.Attributes = this._attributes;
            this._surNameAttribute.Attributes = this._attributes;
            this._addressAttribute.Attributes = this._attributes;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CSystemPolicy> GetPolicies(DataContext Context)
        {
            this._policies.Clear();

            var Links = cMenu.Metaobjects.Linq.CMetaobjectLink.sGetInternalLinksByClass(this._key, EnMetaobjectClass.ESystemPolicy, Context);
            foreach (cMenu.Metaobjects.Linq.CMetaobjectLink Link in Links)
            {
                CSystemPolicy Policy = new CSystemPolicy(Link.LinkedObjectKey, Context);
                this._policies.Add(Policy);
            }

            Links = cMenu.Metaobjects.Linq.CMetaobjectLink.sGetExternalLinksByClass(this._key, EnMetaobjectClass.ESystemUserGroup, Context);
            foreach (cMenu.Metaobjects.Linq.CMetaobjectLink Link in Links)
            {
                CSystemUserGroup Group = new CSystemUserGroup(Context);
                Group.Key = Link.SourceObjectKey;
                Group.GetPolicies(Context);
                foreach (CSystemPolicy Policy in Group.Policies)
                {
                    if (this._policies.Where(P => P.ID.ToString() == Policy.ID.ToString()).ToList().Count == 0)
                        this._policies.Add(Policy);
                }
            }
            return this._policies;
        }
        public List<CMetaobjectSecurityRecord> GetSecurityRecords(DataContext Context)
        {
            this._securityRecords = CMetaobjectSecurityRecord.sGetRecordsByUser(this._key, Context);

            var Links = cMenu.Metaobjects.Linq.CMetaobjectLink.sGetInternalLinksByClass(this._key, EnMetaobjectClass.ESystemUserGroup, Context);
            foreach (cMenu.Metaobjects.Linq.CMetaobjectLink Link in Links)
            {
                CSystemUserGroup Group = new CSystemUserGroup(Context);
                Group.Key = Link.SourceObjectKey;
                Group.GetSecurityRecords(Context);
                foreach (CMetaobjectSecurityRecord Record in Group.SecurityRecords)
                {
                    if (this._securityRecords.Where(R => R.MetaobjectKey == Record.MetaobjectKey).ToList().Count == 0)
                        this._securityRecords.Add(Record);
                }
            }

            return this._securityRecords;
        }
        public List<CSystemUserSession> GetSessions(DataContext Context)
        {
            this._sessions = CSystemUserSession.sGetSessionsByUser(this._key, Context);
            return this._sessions;
        }
        public decimal GetRightsForMetaobject(cMenu.Metaobjects.Linq.CMetaobject Metaobject)
        {
            var UserRecords = this._securityRecords.Where(R => R.MetaobjectKey == Metaobject.Key && R.UserKey == this._key).ToList();
            var GroupRecords = this._securityRecords.Where(R => R.MetaobjectKey == Metaobject.Key && R.UserKey != this._key).ToList();

            if (UserRecords.Count == 0 && GroupRecords.Count == 0)
                return 0;
            if (UserRecords.Count != 0)
                return UserRecords[0].Rights;
            if (GroupRecords.Count != 0)
                return GroupRecords[0].Rights;

            return 0;
        }

        public override int ObjectDeleteByID(DataContext Context)
        {
            var R = CMetaobjectSecurityRecord.sDeleteRecordsByUser(this._key, Context);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            R = CSystemUserSession.sDeleteSessionsByUser(this._key, Context);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return base.ObjectDeleteByID(Context);
        }
        public override int ObjectDeleteByKey(DataContext Context)
        {
            var R = CMetaobjectSecurityRecord.sDeleteRecordsByUser(this._key, Context);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            R = CSystemUserSession.sDeleteSessionsByUser(this._key, Context);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return base.ObjectDeleteByKey(Context);
        }
        public override int ObjectGetByID(DataContext Context)
        {
            return base.ObjectGetByID(Context);
        }
        public override int ObjectGetByKey(DataContext Context)
        {
            return base.ObjectGetByKey(Context);
        }
        public override int ObjectInsert(DataContext Context)
        {
            var R = CSystemUser.sGetUserByLogin(this.Login, Context);
            if (R != null)
                return CErrors.ERR_DB_INSERT_OBJECT;

            return base.ObjectInsert(Context);
        }
        public override int ObjectUpdate(DataContext Context)
        {
            return base.ObjectUpdate(Context);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CSystemUser> sGetAllUsers(DataContext Context)
        {
            var Users = Context.GetTable<cMenu.Metaobjects.Linq.CMetaobject>();
            var Query = from User in Users
                        where User.Class == EnMetaobjectClass.ESystemUser
                        select (CSystemUser)User;

            return Query.ToList();
        }
        public static CSystemUser sGetUserByLogin(string Login, DataContext Context)
        {
            List<CSystemUser> Users = CSystemUser.sGetAllUsers(Context);
            foreach (CSystemUser User in Users)
            {
                User.Attributes.Context = Context;
                if (User.Login.ToUpper() == Login.ToUpper())
                    return User;
            }                
            return null;
        }
        public static CSystemUser sGetUserByEmail(string Email, DataContext Context)
        {
            List<CSystemUser> Users = CSystemUser.sGetAllUsers(Context);
            foreach (CSystemUser User in Users)
            {
                User.Attributes.Context = Context;
                if (User.Email.ToUpper() == Email.ToUpper())
                    return User;
            }
            return null;
        }
        public static CSystemUser sGetUserByMobilePhone(string Phone, DataContext Context)
        {
            List<CSystemUser> Users = CSystemUser.sGetAllUsers(Context);
            foreach (CSystemUser User in Users)
            {
                User.Attributes.Context = Context;
                if (User.MobilePhone.ToUpper() == Phone.ToUpper())
                    return User;
            }
            return null;
        }
        public static CSystemUser sGetUserByLoginPasshash(string Login, string Password, string Passhash, DataContext Context)
        {
            List<CSystemUser> Users = CSystemUser.sGetAllUsers(Context);
            foreach (CSystemUser User in Users)
            {
                User.Attributes.Context = Context;
                var R = CSecurityHelper.sVerifyPasshash(Login, Password, User.Passhash);
                if (R) return User;
            }
            return null;
        }
        #endregion
    }
}
