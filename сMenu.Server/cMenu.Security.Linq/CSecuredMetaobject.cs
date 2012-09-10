using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects.Linq;
using cMenu.Security.Linq.MetaobjectsManagement;

namespace cMenu.Security.Linq
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
        public CSecuredMetaobject(DataContext Context)
            : base(Context)
        {
        }
        public CSecuredMetaobject(decimal Key, DataContext Context)
            : base(Key, Context)
        {
        }
        public CSecuredMetaobject(Guid ID, DataContext Context)
            : base(ID, Context)
        {
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public override int ObjectDeleteByID(DataContext Context)
        {
            var R = CMetaobjectSecurityRecord.sDeleteRecordsByMetaobject(this._key, Context);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return base.ObjectDeleteByID(Context);
        }
        public override int ObjectDeleteByKey(DataContext Context)
        {
            var R = CMetaobjectSecurityRecord.sDeleteRecordsByMetaobject(this._key, Context);
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

        public List<CMetaobjectSecurityRecord> GetSecurityRecords(DataContext Context)
        {
            this._securityRecords = CMetaobjectSecurityRecord.sGetRecordsByMetaobject(this._key, Context);
            return this._securityRecords;
        }
        #endregion
    }
}
