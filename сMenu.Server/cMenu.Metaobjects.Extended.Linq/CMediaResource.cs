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

namespace cMenu.Metaobjects.Extended.Linq
{
    [Serializable]
    public class CMediaResource : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _linkAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_MEDIA_ATTR_LINK };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<EnMediaResourceType> _typeAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<EnMediaResourceType>() { AttributeID = CMetaobjectsExConsts.CONST_MEDIA_ATTR_TYPE };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]> _mediaAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]>() { AttributeID = CMetaobjectsExConsts.CONST_MEDIA_ATTR_MEDIA };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<EnMediaResourceType> TypeAttribute
        {
            get { return _typeAttribute; }
            set { _typeAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<byte[]> MediaAttribute
        {
            get { return _mediaAttribute; }
            set { _mediaAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> LinkAttribute
        {
            get { return _linkAttribute; }
            set { _linkAttribute = value; }
        }

        public EnMediaResourceType Type
        {
            get { return _typeAttribute.DefaultValue; }
            set { _typeAttribute.DefaultValue = value; }
        }
        public byte[] Media
        {
            get { return _mediaAttribute.DefaultValue; }
            set { _mediaAttribute.DefaultValue = value; }
        }
        public string Link
        {
            get { return _linkAttribute.DefaultValue; }
            set { _linkAttribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMediaResource()
            : base()
        {
            this._typeAttribute.Attributes = this._attributes;
            this._mediaAttribute.Attributes = this._attributes;
            this._linkAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMediaResource;
        }
        public CMediaResource(DataContext Context)
            : base(Context)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._mediaAttribute.Attributes = this._attributes;
            this._linkAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMediaResource;
            this.Media = new byte[0];
            this.Link = "";
        }
        public CMediaResource(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._mediaAttribute.Attributes = this._attributes;
            this._linkAttribute.Attributes = this._attributes;
        }
        public CMediaResource(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._mediaAttribute.Attributes = this._attributes;
            this._linkAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
