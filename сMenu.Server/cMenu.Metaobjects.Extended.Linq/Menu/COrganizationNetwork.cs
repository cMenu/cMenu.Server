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
    public class COrganizationNetwork : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _urlAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_NET_ATTR_URL };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _mailAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_NET_ATTR_MAIL };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> UrlAttribute
        {
            get { return _urlAttribute; }
            set { _urlAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> MailAttribute
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
        public COrganizationNetwork(DataContext Context)
            : base(Context)
        {
            this._urlAttribute.Attributes = this._attributes;
            this._mailAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EOrganizationNetwork;
            this.Url = "";
            this.Email = "";
        }
        public COrganizationNetwork(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._urlAttribute.Attributes = this._attributes;
            this._mailAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EOrganizationNetwork;
        }
        public COrganizationNetwork(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._urlAttribute.Attributes = this._attributes;
            this._mailAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EOrganizationNetwork;
        }
        #endregion
    }
}
