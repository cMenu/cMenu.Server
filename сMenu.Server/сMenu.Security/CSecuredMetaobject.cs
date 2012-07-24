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

        #region PROTECTED FUNCTIONS
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

        #region PUBLIC FUNCTIONS
        public override int ObjectDeleteByID(IDatabaseProvider Provider)
        {
            CMetaobjectSecurityRecord.sDeleteRecordsByMetaobject(Provider, this._key);
            return base.ObjectDeleteByID(Provider);
        }
        public override int ObjectDeleteByKey(IDatabaseProvider Provider)
        {
            CMetaobjectSecurityRecord.sDeleteRecordsByMetaobject(Provider, this._key);
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

        public List<CMetaobjectSecurityRecord> GetSecurityRecords(IDatabaseProvider Provider)
        {
            this._securityRecords = CMetaobjectSecurityRecord.sGetRecordsByMetaobject(Provider, this._key);
            return this._securityRecords;
        }
        #endregion
    }
}
