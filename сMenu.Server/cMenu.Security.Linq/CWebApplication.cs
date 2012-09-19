using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Linq;

using Newtonsoft.Json;

namespace cMenu.Security.Linq
{
    [Serializable]
    public class CWebApplication : CSecuredMetaobject
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _addrAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_ADDR };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<bool> _allowOrdersAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<bool>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_ADDR };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> _groupsFolderAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_GROUPS_FOLDER };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> _usersFolderAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_USERS_FOLDER };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> _servicesFolderAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_SERVICES_FOLDER };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> _mediaFolderAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_MEDIA_FOLDER };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> _dictionariesFolderAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_DICT_FOLDER };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> _advertisementFolderAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal>() { AttributeID = CSecurityConsts.CONST_WEB_APP_ATTR_ADVERT_FOLDER };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> AddrAttribute
        {
            get { return _addrAttribute; }
            set { _addrAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<bool> AllowOrdersAttribute
        {
            get { return _allowOrdersAttribute; }
            set { _allowOrdersAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> GroupsFolderAttribute
        {
            get { return _groupsFolderAttribute; }
            set { _groupsFolderAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> UsersFolderAttribute
        {
            get { return _usersFolderAttribute; }
            set { _usersFolderAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> ServicesFolderAttribute
        {
            get { return _servicesFolderAttribute; }
            set { _servicesFolderAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> MediaFolderAttribute
        {
            get { return _mediaFolderAttribute; }
            set { _mediaFolderAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> DictionariesFolderAttribute
        {
            get { return _dictionariesFolderAttribute; }
            set { _dictionariesFolderAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<decimal> AdvertiementFolderAttribute
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
        public CWebApplication(DataContext Context)
            : base(Context)
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
            this.AllowOrders = false;
            this.GroupsFolderKey = -1;
            this.UsersFolderKey = -1;
            this.ServicesFolderKey = -1;
            this.MediaFolderKey = -1;
            this.DictionariesFolderKey = -1;
        }
        public CWebApplication(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;
            this._groupsFolderAttribute.Attributes = this._attributes;
            this._usersFolderAttribute.Attributes = this._attributes;
            this._servicesFolderAttribute.Attributes = this._attributes;
            this._mediaFolderAttribute.Attributes = this._attributes;
            this._dictionariesFolderAttribute.Attributes = this._attributes;
        }
        public CWebApplication(decimal Key, DataContext Context)
            : base(Key, Context)
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
