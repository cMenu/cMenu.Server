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

namespace cMenu.Security.Linq.UsersManagement
{
    [Serializable]
    public class CSystemUserGroup : cMenu.Metaobjects.Linq.CMetaobject
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
        public CSystemUserGroup(DataContext Context)
            : base(Context)
        {
            this._class = EnMetaobjectClass.ESystemUserGroup;
        }
        public CSystemUserGroup(decimal Key, DataContext Context)
            : base(Key, Context)
        {
        }
        public CSystemUserGroup(Guid ID, DataContext Context)
            : base(ID, Context)
        {
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CSystemUser> GetUsers(DataContext Context)
        {
            var Links = cMenu.Metaobjects.Linq.CMetaobjectLink.sGetInternalLinksByClass(this._key, EnMetaobjectClass.ESystemUser, Context);
            foreach (cMenu.Metaobjects.Linq.CMetaobjectLink Link in Links)
            {
                CSystemUser User = new CSystemUser(Link.LinkedObjectKey, Context);
                User.Attributes.Context = Context;
                this._users.Add(User);
            }

            return this._users;
        }
        public List<CSystemPolicy> GetPolicies(DataContext Context)
        {
            var Links = cMenu.Metaobjects.Linq.CMetaobjectLink.sGetInternalLinksByClass(this._key, EnMetaobjectClass.ESystemPolicy, Context);
            foreach (cMenu.Metaobjects.Linq.CMetaobjectLink Link in Links)
            {
                CSystemPolicy Policy = new CSystemPolicy(Link.LinkedObjectKey, Context);
                Policy.Attributes.Context = Context;
                this._policies.Add(Policy);
            }

            return this._policies;
        }
        public List<CMetaobjectSecurityRecord> GetSecurityRecords(DataContext Context)
        {
            this._securityRecords = CMetaobjectSecurityRecord.sGetRecordsByUser(this._key, Context);
            return this._securityRecords;
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
            return base.ObjectInsert(Context);
        }
        public override int ObjectUpdate(DataContext Context)
        {
            return base.ObjectUpdate(Context);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CSystemUserGroup> sGetAllGroups(DataContext Context)
        {
            var Groups = Context.GetTable<cMenu.Metaobjects.Linq.CMetaobject>();
            var Query = from Group in Groups
                        where Group.Class == EnMetaobjectClass.ESystemUserGroup
                        select (CSystemUserGroup)Group;

            return Query.ToList();
        }
        #endregion
    }
}
