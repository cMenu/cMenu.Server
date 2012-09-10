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
    public class CCategory : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]> _thumbnailAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]>() { AttributeID = CMetaobjectsExConsts.CONST_CAT_ATTR_IMAGE };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]> ThumbnailAttribute
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
        public CCategory(DataContext Context)
            : base(Context)
        {            
            this._thumbnailAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ECategory;
            this.Thumbnail = new byte[0];
        }
        public CCategory(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
        }
        public CCategory(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._thumbnailAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
