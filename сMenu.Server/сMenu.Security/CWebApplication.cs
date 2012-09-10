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
        #endregion

        #region CONSTRUCTORS
        public CWebApplication()
            : base()
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;
        }
        public CWebApplication(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EWebApplication;
            this.Address = "";
            this.AllowOrders = false;
        }
        public CWebApplication(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;
        }
        public CWebApplication(decimal Key, IDatabaseProvider Provider) 
            : base(Key, Provider)
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
