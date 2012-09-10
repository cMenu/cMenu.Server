using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;

using cMenu.DB;
using cMenu.Metaobjects;
using cMenu.Common;
using cMenu.Common.Base;
using cMenu.Metadata.Attributes;

namespace cMenu.Rds
{
    [Serializable]
    public class CRdsDictionary : CMetaobject
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
        public CRdsDictionary(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.ERDSDictionary;
        }
        public CRdsDictionary(decimal Key, IDatabaseProvider Provider) 
            : base(Key, Provider)
        {
            this._class = EnMetaobjectClass.ERDSDictionary;
        }
        public CRdsDictionary(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._class = EnMetaobjectClass.ERDSDictionary;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CRdsElement> GetElements(IDatabaseProvider Provider)
        {
            this._dictElements = CRdsElement.sElementsGetByDictionary(this.Key, Provider);
            return this._dictElements;
        }
        public List<CRdsAttribute> GetAttributes(IDatabaseProvider Provider)
        {
            this._dictAttributes = CRdsAttribute.sAttributesGetByDictionary(this._key, Provider);
            return this._dictAttributes;
        }

        public override int ObjectDeleteByKey(IDatabaseProvider Provider)
        {
            var R = CRdsElement.sElementsDeleteByDictionary(this._key, Provider);
            if (R != -1)
                return R;

            R = CRdsAttribute.sAttributesDeleteByDictionary(this._key, Provider);
            if (R != -1)
                return R;

            R = base.ObjectDeleteByKey(Provider);
            if (R != -1)
                return R;

            return -1;
            
        }
        public override int ObjectDeleteByID(IDatabaseProvider Provider)
        {
            var R = CRdsElement.sElementsDeleteByDictionary(this._key, Provider);
            if (R != -1)
                return R;

            R = CRdsAttribute.sAttributesDeleteByDictionary(this._key, Provider);
            if (R != -1)
                return R;

            R = base.ObjectDeleteByID(Provider);
            if (R != -1)
                return R;

            return -1;
        }
        public override int ObjectGetByKey(IDatabaseProvider Provider)
        {
            var R = base.ObjectGetByKey(Provider);
            return R;
        }
        public override int ObjectGetByID(IDatabaseProvider Provider)
        {
            var R = base.ObjectGetByID(Provider);
            return R;
        }
        #endregion
    }
}
