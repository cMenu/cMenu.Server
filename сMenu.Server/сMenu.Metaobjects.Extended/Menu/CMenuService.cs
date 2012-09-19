using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using cMenu.DB;
using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.Security;

using Newtonsoft.Json;

namespace cMenu.Metaobjects.Extended.Menu
{
    [Serializable]
    public class CMenuService : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<byte[]> _thumbnailAttribute = new CMetaobjectAttributeLocalized<byte[]>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_IMAGE };
        protected CMetaobjectAttributeLocalized<string> _compositionAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_COMPOS };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<byte[]> ThumbnailAttribute
        {
            get { return _thumbnailAttribute; }
            set { _thumbnailAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> CompositionAttribute
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
        public CMenuService(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._compositionAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMenuService;
            this.Composition = "";
            this.Thumbnail = new byte[0];
        }
        public CMenuService(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._compositionAttribute.Attributes = this._attributes;
        }
        public CMenuService(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._compositionAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
