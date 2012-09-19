using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects.Extended;
using cMenu.Metaobjects.Linq;
using cMenu.Security.Linq;

using Newtonsoft.Json;

namespace cMenu.Metaobjects.Extended.Linq.Menu
{
    [Serializable]
    public class CMenuService : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]> _thumbnailAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_IMAGE };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _compositionAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_COMPOS };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]> ThumbnailAttribute
        {
            get { return _thumbnailAttribute; }
            set { _thumbnailAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> CompositionAttribute
        {
            get { return _compositionAttribute; }
            set { _compositionAttribute = value; }
        }

        public byte[] Thumbnail
        {
            get { return _thumbnailAttribute.DefaultValue; }
            set { _thumbnailAttribute.DefaultValue = value; }
        }
        public string Composition
        {
            get { return _compositionAttribute.DefaultValue; }
            set { _compositionAttribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMenuService()
            : base()
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._compositionAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMenuService;
        }
        public CMenuService(DataContext Context)
            : base(Context)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._compositionAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMenuService;
            this.Composition = "";
            this.Thumbnail = new byte[0];
        }
        public CMenuService(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._compositionAttribute.Attributes = this._attributes;
        }
        public CMenuService(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._compositionAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
