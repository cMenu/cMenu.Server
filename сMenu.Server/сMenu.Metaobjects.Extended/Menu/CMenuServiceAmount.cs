using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.Security;

namespace cMenu.Metaobjects.Extended.Menu
{
    [Serializable]
    public class CMenuServiceAmount : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<double> _priceAttribute = new CMetaobjectAttributeLocalized<double>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_PRICE };
        protected CMetaobjectAttributeLocalized<string> _unitsAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_UNITS };
        protected CMetaobjectAttributeLocalized<double> _amountAttribute = new CMetaobjectAttributeLocalized<double>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_AMOUNT };
        #endregion

        #region PUBLIC FIELDS
        public CMetaobjectAttributeLocalized<double> PriceAttribute
        {
            get { return _priceAttribute; }
            set { _priceAttribute = value; }
        }
        public CMetaobjectAttributeLocalized<string> UnitsAttribute
        {
            get { return _unitsAttribute; }
            set { _unitsAttribute = value; }
        }
        public CMetaobjectAttributeLocalized<double> AmountAttribute
        {
            get { return _amountAttribute; }
            set { _amountAttribute = value; }
        }
        public double Price
        {
            get { return _priceAttribute.DefaultValue; }
            set { _priceAttribute.DefaultValue = value; }
        }
        public string Units
        {
            get { return _unitsAttribute.DefaultValue; }
            set { _unitsAttribute.DefaultValue = value; }
        }
        public double Amount
        {
            get { return _amountAttribute.DefaultValue; }
            set { _amountAttribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMenuServiceAmount()
            : base()
        {
            this._priceAttribute.Attributes = this._attributes;
            this._unitsAttribute.Attributes = this._attributes;
            this._amountAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMenuServiceAmount;
            this.Price = 0.0D;
            this.Units = "";
            this.Amount = 0.0D;
        }
        public CMenuServiceAmount(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._priceAttribute.Attributes = this._attributes;
            this._unitsAttribute.Attributes = this._attributes;
            this._amountAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMenuServiceAmount;
            this.Price = 0.0D;
            this.Units = "";
            this.Amount = 0.0D;
        }
        public CMenuServiceAmount(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._priceAttribute.Attributes = this._attributes;
            this._unitsAttribute.Attributes = this._attributes;
            this._amountAttribute.Attributes = this._attributes;
        }
        public CMenuServiceAmount(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._priceAttribute.Attributes = this._attributes;
            this._unitsAttribute.Attributes = this._attributes;
            this._amountAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
