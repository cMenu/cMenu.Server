using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;

namespace cMenu.Rds
{
    [Serializable]
    public class CRdsElementAttributeValues
    {
        #region PROTECTED FIELDS
        protected IDatabaseProvider _provider;
        protected decimal _elementVersion = -1;
        protected List<CRdsAttributeValue> _values = new List<CRdsAttributeValue>();
        #endregion

        #region PUBLIC FIELDS
        public IDatabaseProvider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }
        public decimal ElementVersion
        {
            get { return _elementVersion; }
            set
            {
                _elementVersion = value;
                foreach (CRdsAttributeValue Value in this._values)
                    Value.ElementVersion = value;
            }
        }
        public List<CRdsAttributeValue> Values
        {
            get { return _values; }
            set { _values = value; }
        }
        public CRdsAttributeValue this[decimal AttributeKey]
        {
            get { return _getValue(AttributeKey); }
            set { _setValue(AttributeKey, value); }
        }
        public object this[decimal AttributeKey, int LanguageCode]
        {
            get { return _getValue(AttributeKey, LanguageCode); }
            set { _setValue(AttributeKey, LanguageCode, value); }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected CRdsAttributeValue _getValue(decimal AttributeKey)
        {
            var R = CRdsAttributeValue.sFindAttributeValueByKey(AttributeKey, this._values);
            if (R != null)
                return R;

            R = this._loadValue(AttributeKey);
            if (R != null)
            {
                this._values.Add(R);
                return R;
            }

            return null;
        }
        protected object _getValue(decimal AttributeKey, int LanguageCode)
        {
            var R = this._getValue(AttributeKey);
            if (R == null)
                return null;

            var Value = R[LanguageCode];
            if (Value != null)
                return Value;

            return null;
        }
        protected int _setValue(decimal AttributeKey, CRdsAttributeValue Value)
        {
            var R = this._getValue(Value.AttributeKey);
            if (R != null)
                this._values.Remove(R);
            this._values.Add(Value);

            return -1;
        }
        protected int _setValue(decimal AttributeKey, int LanguageCode, object Value)
        {
            var R = this._getValue(AttributeKey);
            if (R == null)
            {
                R = new CRdsAttributeValue()
                {
                    AttributeKey = AttributeKey,
                    ElementVersion = this._elementVersion,
                    Provider = this._provider
                };
                this._values.Add(R);
            }

            R[LanguageCode] = Value;

            return -1;
        }

        protected List<CRdsAttributeValue> _loadValues()
        {
            return CRdsAttributeValue.sGetAttributeValuesByElement(this._elementVersion, this._provider);
        }
        protected CRdsAttributeValue _loadValue(decimal AttributeKey)
        {
            var R = CRdsAttributeValue.sGetAttributeValue(this._elementVersion, AttributeKey, this._provider);
            return R;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int AttributeValueLoad(decimal AttributeKey, IDatabaseProvider Provider)
        {
            var R = this._getValue(AttributeKey);
            return (R != null ? -1 : -2);
        }
        public int AttributeValuesUpdate(IDatabaseProvider Provider)
        {
            foreach (CRdsAttributeValue Value in this._values)
                Value.ValueUpdateAll(Provider);
            return -1;
        }
        public int AttributeValuesInsert(IDatabaseProvider Provider)
        {
            foreach (CRdsAttributeValue Value in this._values)
                Value.ValueInsertAll(Provider);
            return -1;
        }
        public int AttributeValuesDelete(IDatabaseProvider Provider)
        {
            CRdsAttributeValue.sAttributeValuesDeleteByElement(this._elementVersion, this._provider);
            return -1;
        }
        public int AttributesGet(IDatabaseProvider Provider)
        {
            this._values = this._loadValues();
            return -1;
        }
        #endregion
    }
}
