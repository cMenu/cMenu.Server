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
    public class CMenuServiceAmount : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<double> _priceAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<double>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_PRICE };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _unitsAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_UNITS };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<double> _amountAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<double>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_AMOUNT };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<TimeSpan> _timeAmountAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<TimeSpan>() { AttributeID = CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_TIME_AMOUNT };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<double> PriceAttribute
        {
            get { return _priceAttribute; }
            set { _priceAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> UnitsAttribute
        {
            get { return _unitsAttribute; }
            set { _unitsAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<double> AmountAttribute
        {
            get { return _amountAttribute; }
            set { _amountAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<TimeSpan> TimeAmountAttribute
        {
            get { return _timeAmountAttribute; }
            set { _timeAmountAttribute = value; }
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
        public TimeSpan TimeAmount
        {
            get { return _timeAmountAttribute.DefaultValue; }
            set { _timeAmountAttribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMenuServiceAmount()
            : base()
        {
            this._priceAttribute.Attributes = this._attributes;
            this._unitsAttribute.Attributes = this._attributes;
            this._amountAttribute.Attributes = this._attributes;
            this._timeAmountAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMenuServiceAmount;
        }
        public CMenuServiceAmount(DataContext Context)
            : base(Context)
        {
            this._priceAttribute.Attributes = this._attributes;
            this._unitsAttribute.Attributes = this._attributes;
            this._amountAttribute.Attributes = this._attributes;
            this._timeAmountAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EMenuServiceAmount;
            this.Price = 0.0D;
            this.Units = "";
            this.Amount = 0.0D;
            this.TimeAmount = new TimeSpan();
        }
        public CMenuServiceAmount(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._priceAttribute.Attributes = this._attributes;
            this._unitsAttribute.Attributes = this._attributes;
            this._amountAttribute.Attributes = this._attributes;
            this._timeAmountAttribute.Attributes = this._attributes;
        }
        public CMenuServiceAmount(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._priceAttribute.Attributes = this._attributes;
            this._unitsAttribute.Attributes = this._attributes;
            this._amountAttribute.Attributes = this._attributes;
            this._timeAmountAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
