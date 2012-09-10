using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.DB;
using cMenu.Metaobjects;
using cMenu.Common;
using cMenu.Common.Base;
using cMenu.Metaobjects.Linq;
using cMenu.Security.Linq;

namespace cMenu.Rds.Linq
{
    [Serializable]
    [Table(Name = CDBConst.CONST_TABLE_OBJECTS)]
    public class CRdsDictionary : CSecuredMetaobject
    {
        #region PROTECTED FIELDS
        protected List<CRdsAttribute> _dictAttributes = new List<CRdsAttribute>();
        protected List<CRdsElement> _dictElements = new List<CRdsElement>();
        #endregion

        #region PUBLIC FIELDS
        public List<CRdsElement> DictionaryElements
        {
            get { return _dictElements; }
            set { _dictElements = value; }
        }
        public List<CRdsAttribute> DictionaryAttributes
        {
            get { return _dictAttributes; }
            set { _dictAttributes = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CRdsDictionary()
            : base()
        {
            this._class = EnMetaobjectClass.ERDSDictionary;
        }
        public CRdsDictionary(DataContext Context)
            : base(Context)
        {
            this._class = EnMetaobjectClass.ERDSDictionary;
        }
        public CRdsDictionary(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._class = EnMetaobjectClass.ERDSDictionary;
        }
        public CRdsDictionary(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._class = EnMetaobjectClass.ERDSDictionary;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CRdsElement> GetElements(DataContext Context)
        {
            this._dictElements = CRdsElement.sElementsGetByDictionary(this.Key, Context);
            return this._dictElements;
        }
        public List<CRdsAttribute> GetAttributes(DataContext Context)
        {
            this._dictAttributes = CRdsAttribute.sAttributesGetByDictionary(this._key, Context);
            return this._dictAttributes;
        }

        public override int ObjectDeleteByKey(DataContext Context)
        {
            var R = CRdsElement.sElementsDeleteByDictionary(this._key, Context);
            if (R != -1)
                return R;

            R = CRdsAttribute.sAttributesDeleteByDictionary(this._key, Context);
            if (R != -1)
                return R;

            R = base.ObjectDeleteByKey(Context);
            if (R != -1)
                return R;

            return -1;

        }
        public override int ObjectDeleteByID(DataContext Context)
        {
            var R = CRdsElement.sElementsDeleteByDictionary(this._key, Context);
            if (R != -1)
                return R;

            R = CRdsAttribute.sAttributesDeleteByDictionary(this._key, Context);
            if (R != -1)
                return R;

            R = base.ObjectDeleteByID(Context);
            if (R != -1)
                return R;

            return -1;
        }
        public override int ObjectGetByKey(DataContext Context)
        {
            var R = base.ObjectGetByKey(Context);
            return R;
        }
        public override int ObjectGetByID(DataContext Context)
        {
            var R = base.ObjectGetByID(Context);
            return R;
        }
        #endregion
    }
}
