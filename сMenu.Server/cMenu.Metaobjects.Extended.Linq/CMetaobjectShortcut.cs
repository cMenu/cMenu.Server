using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects.Linq;
using cMenu.Security.Linq;

using Newtonsoft.Json;

namespace cMenu.Metaobjects.Extended.Linq
{
    [Serializable]
    public class CMetaobjectShortcut : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> _sourceObjectKeyAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal>() { AttributeID = CMetaobjectsExConsts.CONST_SC_SOURCE_OBJECT };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> SourceObjectKeyAttribute
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
        }
        public CMetaobjectShortcut(DataContext Context)
            : base(Context)
        {
            this._sourceObjectKeyAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EShortcut;
            this.SourceObjectKey = -1;
        }
        public CMetaobjectShortcut(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._sourceObjectKeyAttribute.Attributes = this._attributes;
        }
        public CMetaobjectShortcut(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._sourceObjectKeyAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
