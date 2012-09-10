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
    public class CMenu : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<byte[]> _thumbnailAttribute = new CMetaobjectAttributeLocalized<byte[]>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_ATTR_IMAGE };
        protected CMetaobjectAttributeLocalized<bool> _primaryAttribute = new CMetaobjectAttributeLocalized<bool>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_ATTR_PRIMARY };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<byte[]> ThumbnailAttribute
        {
            get { return _thumbnailAttribute; }
            set { _thumbnailAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<bool> PrimaryAttribute
        {
            get { return _primaryAttribute; }
            set { _primaryAttribute = value; }
        }

        public byte[] Thumbnail
        {
            get { return _thumbnailAttribute.DefaultValue; }
            set { _thumbnailAttribute.DefaultValue = value; }
        }
        public bool Primary
        {
            get { return _primaryAttribute.DefaultValue; }
            set { _primaryAttribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMenu()
            : base()
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._primaryAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMenu;
        }
        public CMenu(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._primaryAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMenu;
            this.Thumbnail = new byte[0];
            this.Primary = false;
        }
        public CMenu(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._primaryAttribute.Attributes = this._attributes;
        }
        public CMenu(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._primaryAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
