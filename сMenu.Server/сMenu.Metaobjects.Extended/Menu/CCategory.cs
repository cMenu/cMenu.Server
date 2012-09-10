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
    public class CCategory : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<byte[]> _thumbnailAttribute = new CMetaobjectAttributeLocalized<byte[]>() { AttributeID = CMetaobjectsExConsts.CONST_CAT_ATTR_IMAGE };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<byte[]> ThumbnailAttribute
        {
            get { return _thumbnailAttribute; }
            set { _thumbnailAttribute = value; }
        }

        public byte[] Thumbnail
        {
            get { return _thumbnailAttribute.DefaultValue; }
            set { _thumbnailAttribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CCategory()
            : base()
        {            
            this._thumbnailAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ECategory;
        }
        public CCategory(IDatabaseProvider Provider)
            : base(Provider)
        {            
            this._thumbnailAttribute.Attributes = this._attributes;

            this._thumbnailAttribute.DefaultValue = new byte[0];
            this._class = EnMetaobjectClass.ECategory;
        }
        public CCategory(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
        }
        public CCategory(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
