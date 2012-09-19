using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects;

using Newtonsoft.Json;

namespace cMenu.Security
{
    [Serializable]
    public class CWebApplication : CMetaobject
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<string> _addrAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_ADDR };
        protected CMetaobjectAttributeLocalized<bool> _allowOrdersAttribute = new CMetaobjectAttributeLocalized<bool>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_ADDR };
        protected CMetaobjectAttributeLocalized<decimal> _groupsFolderAttribute = new CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_GROUPS_FOLDER };
        protected CMetaobjectAttributeLocalized<decimal> _usersFolderAttribute = new CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_USERS_FOLDER };
        protected CMetaobjectAttributeLocalized<decimal> _servicesFolderAttribute = new CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_SERVICES_FOLDER };
        protected CMetaobjectAttributeLocalized<decimal> _mediaFolderAttribute = new CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_MEDIA_FOLDER };
        protected CMetaobjectAttributeLocalized<decimal> _dictionariesFolderAttribute = new CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_DICT_FOLDER };
        protected CMetaobjectAttributeLocalized<decimal> _advertisementFolderAttribute = new CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_ADVERT_FOLDER };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> AddrAttribute
        {
            get { return _addrAttribute; }
            set { _addrAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<bool> AllowOrdersAttribute
        {
            get { return _allowOrdersAttribute; }
            set { _allowOrdersAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<decimal> GroupsFolderAttribute
        {
            get { return _groupsFolderAttribute; }
            set { _groupsFolderAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<decimal> UsersFolderAttribute
        {
            get { return _usersFolderAttribute; }
            set { _usersFolderAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<decimal> ServicesFolderAttribute
        {
            get { return _servicesFolderAttribute; }
            set { _servicesFolderAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<decimal> MediaFolderAttribute
        {
            get { return _mediaFolderAttribute; }
            set { _mediaFolderAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<decimal> DictionariesFolderAttribute
        {
            get { return _dictionariesFolderAttribute; }
            set { _dictionariesFolderAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<decimal> AdvertiementFolderAttribute
        {
            get { return _advertisementFolderAttribute; }
            set { _advertisementFolderAttribute = value; }
        }

        public string Address
        {
            get { return _addrAttribute.DefaultValue; }
            set { _addrAttribute.DefaultValue = value; }
        }
        public bool AllowOrders
        {
            get { return _allowOrdersAttribute.DefaultValue; }
            set { _allowOrdersAttribute.DefaultValue = value; }
        }
        public decimal GroupsFolderKey
        {
            get { return _groupsFolderAttribute.DefaultValue; }
            set { _groupsFolderAttribute.DefaultValue = value; }
        }
        public decimal UsersFolderKey
        {
            get { return _usersFolderAttribute.DefaultValue; }
            set { _usersFolderAttribute.DefaultValue = value; }
        }
        public decimal ServicesFolderKey
        {
            get { return _servicesFolderAttribute.DefaultValue; }
            set { _servicesFolderAttribute.DefaultValue = value; }
        }
        public decimal MediaFolderKey
        {
            get { return _mediaFolderAttribute.DefaultValue; }
            set { _mediaFolderAttribute.DefaultValue = value; }
        }
        public decimal DictionariesFolderKey
        {
            get { return _dictionariesFolderAttribute.DefaultValue; }
            set { _dictionariesFolderAttribute.DefaultValue = value; }
        }
        public decimal AdvertisementFolderKey
        {
            get { return _advertisementFolderAttribute.DefaultValue; }
            set { _advertisementFolderAttribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CWebApplication()
            : base()
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;
            this._groupsFolderAttribute.Attributes = this._attributes;
            this._usersFolderAttribute.Attributes = this._attributes;
            this._servicesFolderAttribute.Attributes = this._attributes;
            this._mediaFolderAttribute.Attributes = this._attributes;
            this._dictionariesFolderAttribute.Attributes = this._attributes;
        }
        public CWebApplication(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;
            this._groupsFolderAttribute.Attributes = this._attributes;
            this._usersFolderAttribute.Attributes = this._attributes;
            this._servicesFolderAttribute.Attributes = this._attributes;
            this._mediaFolderAttribute.Attributes = this._attributes;
            this._dictionariesFolderAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EWebApplication;
            this.Address = "";
            this.AllowOrders = false;
            this.GroupsFolderKey = -1;
            this.UsersFolderKey = -1;
            this.ServicesFolderKey = -1;
            this.MediaFolderKey = -1;
            this.DictionariesFolderKey = -1;
        }
        public CWebApplication(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;
            this._groupsFolderAttribute.Attributes = this._attributes;
            this._usersFolderAttribute.Attributes = this._attributes;
            this._servicesFolderAttribute.Attributes = this._attributes;
            this._mediaFolderAttribute.Attributes = this._attributes;
            this._dictionariesFolderAttribute.Attributes = this._attributes;
        }
        public CWebApplication(decimal Key, IDatabaseProvider Provider) 
            : base(Key, Provider)
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;
            this._groupsFolderAttribute.Attributes = this._attributes;
            this._usersFolderAttribute.Attributes = this._attributes;
            this._servicesFolderAttribute.Attributes = this._attributes;
            this._mediaFolderAttribute.Attributes = this._attributes;
            this._dictionariesFolderAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
