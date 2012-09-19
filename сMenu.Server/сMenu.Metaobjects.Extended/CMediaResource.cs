using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.Security;

using Newtonsoft.Json;

namespace cMenu.Metaobjects.Extended
{
    [Serializable]
    public class CMediaResource : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<string> _linkAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_MEDIA_ATTR_LINK };
        protected CMetaobjectAttributeLocalized<EnMediaResourceType> _typeAttribute = new CMetaobjectAttributeLocalized<EnMediaResourceType>() { AttributeID = CMetaobjectsExConsts.CONST_MEDIA_ATTR_TYPE };
        protected CMetaobjectAttributeLocalized<byte[]> _mediaAttribute = new CMetaobjectAttributeLocalized<byte[]>() { AttributeID = CMetaobjectsExConsts.CONST_MEDIA_ATTR_MEDIA };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<EnMediaResourceType> TypeAttribute
        {
            get { return _typeAttribute; }
            set { _typeAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<byte[]> MediaAttribute
        {
            get { return _mediaAttribute; }
            set { _mediaAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> LinkAttribute
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
        public CMediaResource(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._mediaAttribute.Attributes = this._attributes;
            this._linkAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMediaResource;
            this.Media = new byte[0];
            this.Link = "";
        }
        public CMediaResource(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._mediaAttribute.Attributes = this._attributes;
            this._linkAttribute.Attributes = this._attributes;
        }
        public CMediaResource(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._mediaAttribute.Attributes = this._attributes;
            this._linkAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
