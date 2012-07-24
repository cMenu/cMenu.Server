using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects;
using cMenu.Metadata.Attributes;
using cMenu.Security.MetaobjectsManagement;
using cMenu.Security.DevicesManagement;
using cMenu.Security.PoliciesManagement;

namespace cMenu.Security.UsersManagement
{
    [Serializable]
    public class CSystemUserGroup : CMetaobject
    {
        #region PROTECTED FIELDS
        protected List<CSystemPolicy> _policies = new List<CSystemPolicy>();
        protected List<CMetaobjectSecurityRecord> _securityRecords = new List<CMetaobjectSecurityRecord>();
        protected List<CSystemUser> _users = new List<CSystemUser>();
        #endregion

        #region PUBLIC FIELDS
        public List<CSystemPolicy> Policies
        {
            get { return _policies; }
            set { _policies = value; }
        }
        public List<CMetaobjectSecurityRecord> SecurityRecords
        {
            get { return _securityRecords; }
            set { _securityRecords = value; }
        }
        public List<CSystemUser> Users
        {
            get { return _users; }
            set { _users = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CSystemUserGroup()
            : base()
        {
            this._class = EnMetaobjectClass.ESystemUserGroup;
        }
        public CSystemUserGroup(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.ESystemUserGroup;
        }
        public CSystemUserGroup(int Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
        }
        public CSystemUserGroup(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CSystemUser> GetUsers(IDatabaseProvider Provider)
        {
            var Links = CMetaobjectLink.sGetInternalLinksByClass(this._key, EnMetaobjectClass.ESystemUser, Provider);
            foreach (CMetaobjectLink Link in Links)
            {
                CSystemUser User = new CSystemUser(Link.LinkedObjectKey, Provider);
                this._users.Add(User);
            }

            return this._users;
        }
        public List<CSystemPolicy> GetPolicies(IDatabaseProvider Provider)
        {
            var Links = CMetaobjectLink.sGetInternalLinksByClass(this._key, EnMetaobjectClass.ESystemPolicy, Provider);
            foreach (CMetaobjectLink Link in Links)
            {
                CSystemPolicy Policy = new CSystemPolicy(Link.LinkedObjectKey, Provider);
                this._policies.Add(Policy);
            }

            return this._policies;
        }
        public List<CMetaobjectSecurityRecord> GetSecurityRecords(IDatabaseProvider Provider)
        {
            this._securityRecords = CMetaobjectSecurityRecord.sGetRecordsByUser(Provider, this._key);
            return this._securityRecords;
        }

        public override int ObjectDeleteByID(IDatabaseProvider Provider)
        {
            CMetaobjectSecurityRecord.sDeleteRecordsByUser(Provider, this._key);
            CClientDeviceSession.sDeleteSessionsByUser(this._key, Provider);
            return base.ObjectDeleteByID(Provider);
        }
        public override int ObjectDeleteByKey(IDatabaseProvider Provider)
        {
            CMetaobjectSecurityRecord.sDeleteRecordsByUser(Provider, this._key);
            CClientDeviceSession.sDeleteSessionsByUser(this._key, Provider);
            return base.ObjectDeleteByKey(Provider);
        }
        public override int ObjectGetByID(IDatabaseProvider Provider)
        {
            base.ObjectGetByID(Provider);
            return CErrors.ERR_SUC;
        }
        public override int ObjectGetByKey(IDatabaseProvider Provider)
        {
            base.ObjectGetByKey(Provider);
            return CErrors.ERR_SUC;
        }
        public override int ObjectInsert(IDatabaseProvider Provider)
        {
            base.ObjectInsert(Provider);

            return CErrors.ERR_SUC;
        }
        public override int ObjectUpdate(IDatabaseProvider Provider)
        {
            base.ObjectUpdate(Provider);
            return CErrors.ERR_SUC;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CSystemUserGroup> sGetAllGroups(IDatabaseProvider Provider)
        {
            List<CSystemUserGroup> R = new List<CSystemUserGroup>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_CLASS, EnMetaobjectClass.ESystemUserGroup);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null)
                return R;
            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Group = new CSystemUserGroup(Provider);
                Group.Key = T.Rows[i][0].CheckDBNULLValue<decimal>();
                Group.ID = Guid.Parse(T.Rows[i][1].CheckDBNULLValue<string>());
                Group.Parent = T.Rows[i][2].CheckDBNULLValue<decimal>();
                Group.Class = (EnMetaobjectClass)T.Rows[i][3].CheckDBNULLValue<int>();
                Group.System = (T.Rows[i][4].CheckDBNULLValue<int>() == 1);
                Group.ModificatonDate = T.Rows[0][5].CheckDBNULLValue<DateTime>();
                Group.Status = (EnMetaobjectStatus)T.Rows[0][6].CheckDBNULLValue<int>();
                Group.ExternalLinks = CMetaobjectLink.sGetExternalLinks(Group.Key, Provider);
                Group.InternalLinks = CMetaobjectLink.sGetInternalLinks(Group.Key, Provider);
                Group.Attributes.ObjectKey = Group.Key;
                R.Add(Group);
            }

            return R;
        }
        #endregion
    }
}
