using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;

namespace cMenu.Metaobjects.Extended
{
    public class CMetaobjectShortcut : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<decimal> _sourceObjectKeyAttribute = new CMetaobjectAttributeLocalized<decimal>() { AttributeID = CMetaobjectsExConsts.CONST_SC_SOURCE_OBJECT };
        #endregion

        #region PUBLIC FIELDS
        public CMetaobjectAttributeLocalized<decimal> SourceObjectKeyAttribute
        {
            get { return _sourceObjectKeyAttribute; }
            set { _sourceObjectKeyAttribute = value; }
        }
        public decimal SourceObjectKey
        {
            get { return _sourceObjectKeyAttribute.DefaultValue; }
            set { _sourceObjectKeyAttribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaobjectShortcut()
            : base()
        {
            this._sourceObjectKeyAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EShortcut;
            this.SourceObjectKey = -1;
        }
        public CMetaobjectShortcut(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._sourceObjectKeyAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EShortcut;
            this.SourceObjectKey = -1;
        }
        public CMetaobjectShortcut(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._sourceObjectKeyAttribute.Attributes = this._attributes;
        }
        public CMetaobjectShortcut(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._sourceObjectKeyAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
