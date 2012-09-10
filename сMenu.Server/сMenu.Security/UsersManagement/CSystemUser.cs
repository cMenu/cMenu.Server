using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects;
using cMenu.Metadata.Attributes;
using cMenu.Security.MetaobjectsManagement;
using cMenu.Security.PoliciesManagement;

using Newtonsoft.Json;

namespace cMenu.Security.UsersManagement
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
    public class CSystemUser : CMetaobject
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<EnSystemUserType> _sysTypeAttribute = new CMetaobjectAttributeLocalized<EnSystemUserType>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_SYS_TYPE };
        protected CMetaobjectAttributeLocalized<int> _entTypeAttribute = new CMetaobjectAttributeLocalized<int>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_ENT_TYPE };
        protected CMetaobjectAttributeLocalized<string> _loginAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_LOGIN };
        protected CMetaobjectAttributeLocalized<string> _passhashAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_PASSHASH };
        protected CMetaobjectAttributeLocalized<byte[]> _photoAttribute = new CMetaobjectAttributeLocalized<byte[]>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_PHOTO };
        protected CMetaobjectAttributeLocalized<string> _emailAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_MAIL };
        protected CMetaobjectAttributeLocalized<string> _homePhoneAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_HOME_PHONE };
        protected CMetaobjectAttributeLocalized<string> _mobilePhoneAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_MOB_PHONE };
        protected CMetaobjectAttributeLocalized<string> _workPhoneAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_WORK_PHONE };
        protected CMetaobjectAttributeLocalized<string> _firstNameAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_NAME };
        protected CMetaobjectAttributeLocalized<string> _secNameAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_SEC_NAME };
        protected CMetaobjectAttributeLocalized<string> _surNameAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_SUR_NAME };
        protected CMetaobjectAttributeLocalized<string> _addressAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_USER_ATTR_ADDR };

        protected List<CSystemPolicy> _policies = new List<CSystemPolicy>();
        protected List<CSystemUserSession> _sessions = new List<CSystemUserSession>();
        protected List<CMetaobjectSecurityRecord> _securityRecords = new List<CMetaobjectSecurityRecord>();
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<EnSystemUserType> SystemTypeAttribute
        {
            get { return _sysTypeAttribute; }
            set { _sysTypeAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<int> EnterpriseTypeAttribute
        {
            get { return _entTypeAttribute; }
            set { _entTypeAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> LoginAttribute
        {
            get { return _loginAttribute; }
            set { _loginAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> PasshashAttribute
        {
            get { return _passhashAttribute; }
            set { _passhashAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<byte[]> PhotoAttribute
        {
            get { return _photoAttribute; }
            set { _photoAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> EmailAttribute
        {
            get { return _emailAttribute; }
            set { _emailAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> HomePhoneAttribute
        {
            get { return _homePhoneAttribute; }
            set { _homePhoneAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> MobilePhoneAttribute
        {
            get { return _mobilePhoneAttribute; }
            set { _mobilePhoneAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> WorkPhoneAttribute
        {
            get { return _workPhoneAttribute; }
            set { _workPhoneAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> FirstNameAttribute
        {
            get { return _firstNameAttribute; }
            set { _firstNameAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> SecondNameAttribute
        {
            get { return _secNameAttribute; }
            set { _secNameAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> SurnameAttribute
        {
            get { return _surNameAttribute; }
            set { _surNameAttribute = value; }
        }
        public CMetaobjectAttributeLocalized<string> AddressAttribute
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
        public CSystemUser(IDatabaseProvider Provider)
            : base(Provider)
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
        public CSystemUser(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
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
        public CSystemUser(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
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
        public List<CSystemPolicy> GetPolicies(IDatabaseProvider Provider)
        {
            this._policies.Clear();

            var Links = CMetaobjectLink.sGetInternalLinksByClass(this._key, EnMetaobjectClass.ESystemPolicy, Provider);
            foreach (CMetaobjectLink Link in Links)
            {
                CSystemPolicy Policy = new CSystemPolicy(Link.LinkedObjectKey, Provider);
                this._policies.Add(Policy);
            }

            Links = CMetaobjectLink.sGetExternalLinksByClass(this._key, EnMetaobjectClass.ESystemUserGroup, Provider);
            foreach (CMetaobjectLink Link in Links)
            {
                CSystemUserGroup Group = new CSystemUserGroup(Provider);
                Group.Key = Link.SourceObjectKey;
                Group.GetPolicies(Provider);
                foreach (CSystemPolicy Policy in Group.Policies)
                {
                    if (this._policies.Where(P => P.ID.ToString() == Policy.ID.ToString()).ToList().Count == 0)
                        this._policies.Add(Policy);
                }
            }
            return this._policies;
        }
        public List<CMetaobjectSecurityRecord> GetSecurityRecords(IDatabaseProvider Provider)
        {
            this._securityRecords = CMetaobjectSecurityRecord.sGetRecordsByUser(this._key, Provider);

            var Links = CMetaobjectLink.sGetInternalLinksByClass(this._key, EnMetaobjectClass.ESystemUserGroup, Provider);
            foreach (CMetaobjectLink Link in Links)
            {
                CSystemUserGroup Group = new CSystemUserGroup(Provider);
                Group.Key = Link.SourceObjectKey;
                Group.GetSecurityRecords(Provider);
                foreach (CMetaobjectSecurityRecord Record in Group.SecurityRecords)
                {
                    if (this._securityRecords.Where(R => R.MetaobjectKey == Record.MetaobjectKey).ToList().Count == 0)
                        this._securityRecords.Add(Record);
                }
            }

            return this._securityRecords;
        }
        public List<CSystemUserSession> GetSessions(IDatabaseProvider Provider)
        {
            this._sessions = CSystemUserSession.sGetSessionsByUser(this._key, Provider);
            return this._sessions;
        }
        public decimal GetRightsForMetaobject(CMetaobject Metaobject)
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

        public override int ObjectDeleteByID(IDatabaseProvider Provider)
        {
            var R = CMetaobjectSecurityRecord.sDeleteRecordsByUser(this._key, Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            R = CSystemUserSession.sDeleteSessionsByUser(this._key, Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return base.ObjectDeleteByID(Provider);
        }
        public override int ObjectDeleteByKey(IDatabaseProvider Provider)
        {
            var R = CMetaobjectSecurityRecord.sDeleteRecordsByUser(this._key, Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            R = CSystemUserSession.sDeleteSessionsByUser(this._key, Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return base.ObjectDeleteByKey(Provider);
        }
        public override int ObjectGetByID(IDatabaseProvider Provider)
        {
            return base.ObjectGetByID(Provider);
        }
        public override int ObjectGetByKey(IDatabaseProvider Provider)
        {
            return base.ObjectGetByKey(Provider);
        }
        public override int ObjectInsert(IDatabaseProvider Provider)
        {
            var R = CSystemUser.sGetUserByLogin(this.Login, Provider);
            if (R != null)
                return CErrors.ERR_DB_INSERT_OBJECT;

            return base.ObjectInsert(Provider);            
        }
        public override int ObjectUpdate(IDatabaseProvider Provider)
        {
            return base.ObjectUpdate(Provider);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CSystemUser> sGetAllUsers(IDatabaseProvider Provider)
        {
            List<CSystemUser> R = new List<CSystemUser>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_CLASS, EnMetaobjectClass.ESystemUser);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null)
                return R;
            for (int i = 0; i < T.Rows.Count; i++)
            {
                var User = new CSystemUser(Provider);
                User.Key = T.Rows[i][0].PostProcessValue<decimal>();
                User.ID = Guid.Parse(T.Rows[i][1].PostProcessValue<string>());
                User.Parent = T.Rows[i][2].PostProcessValue<decimal>();
                User.Class = T.Rows[i][3].PostProcessValue<EnMetaobjectClass>();
                User.System = (T.Rows[i][4].PostProcessValue<int>() == 1);
                User.ModificatonDate = T.Rows[0][5].PostProcessValue<DateTime>();
                User.Status = T.Rows[0][6].PostProcessValue<EnMetaobjectStatus>();
                User.Attributes.ObjectKey = User.Key;
                R.Add(User);
            }

            return R;
        }
        public static CSystemUser sGetUserByLogin(string Login, IDatabaseProvider Provider)
        {
            List<CSystemUser> Users = CSystemUser.sGetAllUsers(Provider);
            foreach (CSystemUser User in Users)
            {
                User.Attributes.AttributesGet(Provider);
                if (User.Login.ToUpper() == Login.ToUpper())
                    return User;
            }
            return null;
        }
        public static CSystemUser sGetUserByEmail(string Email, IDatabaseProvider Provider)
        {
            List<CSystemUser> Users = CSystemUser.sGetAllUsers(Provider);
            foreach (CSystemUser User in Users)
            {
                User.Attributes.AttributesGet(Provider);
                if (User.Email.ToUpper() == Email.ToUpper())
                    return User;
            }
            return null;
        }
        public static CSystemUser sGetUserByMobilePhone(string Phone, IDatabaseProvider Provider)
        {
            List<CSystemUser> Users = CSystemUser.sGetAllUsers(Provider);
            foreach (CSystemUser User in Users)
            {
                User.Attributes.AttributesGet(Provider);
                if (User.MobilePhone.ToUpper() == Phone.ToUpper())
                    return User;
            }
            return null;
        }
        public static CSystemUser sGetUserByLoginPasshash(string Login, string Password, string Passhash, IDatabaseProvider Provider)
        {
            List<CSystemUser> Users = CSystemUser.sGetAllUsers(Provider);
            foreach (CSystemUser User in Users)
            {
                User.Attributes.AttributesGet(Provider);
                var R = CSecurityHelper.sVerifyPasshash(Login, Password, User.Passhash);
                if (R) return User;
            }
            return null;
        }
        #endregion
    }
}