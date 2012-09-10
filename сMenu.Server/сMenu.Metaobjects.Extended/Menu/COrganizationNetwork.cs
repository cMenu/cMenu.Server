using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.Security;

using Newtonsoft.Json;

namespace cMenu.Metaobjects.Extended.Menu
{
    [Serializable]
    public class COrganizationNetwork : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<string> _urlAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_NET_ATTR_URL };
        protected CMetaobjectAttributeLocalized<string> _mailAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_NET_ATTR_MAIL };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> UrlAttribute
        {
            get { return _urlAttribute; }
            set { _urlAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> MailAttribute
        {
            get { return _mailAttribute; }
            set { _mailAttribute = value; }
        }

        public string Url
        {
            get { return _urlAttribute.DefaultValue; }
            set { _urlAttribute.DefaultValue = value; }
        }
        public string Email
        {
            get { return _mailAttribute.DefaultValue; }
            set { _mailAttribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTUCTORS
        public COrganizationNetwork() 
            : base()
        {
            this._urlAttribute.Attributes = this._attributes;
            this._mailAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EOrganizationNetwork;
        }
        public COrganizationNetwork(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._urlAttribute.Attributes = this._attributes;
            this._mailAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EOrganizationNetwork;
            this.Url = "";
            this.Email = "";
        }
        public COrganizationNetwork(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._urlAttribute.Attributes = this._attributes;
            this._mailAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EOrganizationNetwork;
        }
        public COrganizationNetwork(decimal Key, IDatabaseProvider Provider) 
            : base(Key, Provider)
        {
            this._urlAttribute.Attributes = this._attributes;
            this._mailAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EOrganizationNetwork;
        }
        #endregion
    }
}
