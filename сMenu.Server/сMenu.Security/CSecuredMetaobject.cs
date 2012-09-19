using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects;
using cMenu.Metadata.Attributes;
using cMenu.Security.MetaobjectsManagement;

namespace cMenu.Security
{
    [Serializable]
    public class CSecuredMetaobject : CMetaobject
    {
        #region PROTECTED FIELDS
        protected List<CMetaobjectSecurityRecord> _securityRecords = new List<CMetaobjectSecurityRecord>();
        #endregion

        #region PUBLIC FIELDS
        public List<CMetaobjectSecurityRecord> SecurityRecords
        {
            get { return _securityRecords; }
            set { _securityRecords = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CSecuredMetaobject() 
            : base()
        { 
        }
        public CSecuredMetaobject(IDatabaseProvider Provider)
            : base(Provider)
        {
        }
        public CSecuredMetaobject(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
        }
        public CSecuredMetaobject(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
        }
        #endregion

        #region PROTECTED FUNCTIONS
        #endregion

        #region PUBLIC FUNCTIONS
        public override int ObjectDeleteByID(IDatabaseProvider Provider)
        {
            var R = CMetaobjectSecurityRecord.sDeleteRecordsByMetaobject(this._key, Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return base.ObjectDeleteByID(Provider);
        }
        public override int ObjectDeleteByKey(IDatabaseProvider Provider)
        {
            var R = CMetaobjectSecurityRecord.sDeleteRecordsByMetaobject(this._key, Provider);
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
            return base.ObjectInsert(Provider);
        }
        public override int ObjectUpdate(IDatabaseProvider Provider)
        {
            return base.ObjectUpdate(Provider);
        }

        public List<CMetaobjectSecurityRecord> GetSecurityRecords(IDatabaseProvider Provider)
        {
            this._securityRecords = CMetaobjectSecurityRecord.sGetRecordsByMetaobject(this._key, Provider);
            return this._securityRecords;
        }
        #endregion
    }
}
