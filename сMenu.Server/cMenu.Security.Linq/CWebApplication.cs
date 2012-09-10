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
        public CWebApplication(DataContext Context)
            : base(Context)
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EWebApplication;
            this.Address = "";
            this.AllowOrders = false;
        }
        public CWebApplication(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;
        }
        public CWebApplication(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._addrAttribute.Attributes = this._attributes;
            this._allowOrdersAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
