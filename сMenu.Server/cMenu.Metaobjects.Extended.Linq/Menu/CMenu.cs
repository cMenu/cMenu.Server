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
    public class CMenu : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]> _thumbnailAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_ATTR_IMAGE };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<bool> _primaryAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<bool>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_ATTR_PRIMARY };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]> ThumbnailAttribute
        {
            get { return _thumbnailAttribute; }
            set { _thumbnailAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<bool> PrimaryAttribute
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
        public CMenu(DataContext Context)
            : base(Context)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._primaryAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMenu;
            this.Thumbnail = new byte[0];
            this.Primary = false;
        }
        public CMenu(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._primaryAttribute.Attributes = this._attributes;
        }
        public CMenu(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
            this._primaryAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
