using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

using cMenu.Common;
using cMenu.Common.Base;
using cMenu.DB;

namespace cMenu.Rds.Linq
{
    public class CRdsAttributeValues
    {
        #region PROTECTED FIELDS
        protected decimal _elementVersion = -1;
        protected decimal _attributeKey = -1;
        protected Dictionary<int, CRdsAttributeValue> _values = new Dictionary<int, CRdsAttributeValue>();
        protected DataContext _context = null;
        #endregion

        #region PUBLIC FIELDS
        public decimal ElementVersion
        {
            get { return _elementVersion; }
            set { _elementVersion = value; }
        }
        public decimal AttributeKey
        {
            get { return _attributeKey; }
            set { _attributeKey = value; }
        }
        public Dictionary<int, CRdsAttributeValue> Values
        {
            get { return _values; }
            set { _values = value; }
        }
        public DataContext Context
        {
            get { return _context; }
            set
            {
                _context = value;
            }
        }

        public CRdsAttributeValue this[int AttributeLocale]
        {
            get { return _getValue(AttributeLocale); }
            set { _setValue(AttributeLocale, value); }
        }
        #endregion

        #region CONSTRUCTORS
        #endregion

        #region PROTECTED FUNCTIONS
        protected CRdsAttributeValue _getValue(int AttributeLocale)
        {
            CRdsAttributeValue R = (this._values.ContainsKey(AttributeLocale) ? this._values[AttributeLocale] : null);
            if (R != null)
                return R;

            R = this.GetValue(this._elementVersion, this._attributeKey, AttributeLocale, this._context);
            if (R == null)
            {
                R = new CRdsAttributeValue()
                {
                    AttributeKey = this._attributeKey,
                    ElementVersion = this._elementVersion,
                    AttributeLocale = AttributeLocale
                };
            }

            this._values.Add(AttributeLocale, R);
            return R;
        }
        protected int _setValue(int AttributeLocale, CRdsAttributeValue Value)
        {
            if (this._values.ContainsKey(AttributeLocale))
                this._values[AttributeLocale] = Value;
            else
                this._values.Add(AttributeLocale, Value);

            return -1;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public CRdsAttributeValue GetValue(decimal ElementVersion, decimal AttributeKey, int AttributeLocale, DataContext Context)
        {
            return CRdsAttributeValue.sGetAttributeValue(ElementVersion, AttributeKey, AttributeLocale, Context);
        }

        public int AttributeValuesInsert(DataContext Context)
        {
            foreach (CRdsAttributeValue Value in this._values.Values)
                Value.AttributeValueInsert(Context);
            return -1;
        }
        public int AttributeValuesUpdate(DataContext Context)
        {
            foreach (CRdsAttributeValue Value in this._values.Values)
                Value.AttributeValueUpdate(Context);
            return -1;
        }
        public int AttributeValuesDelete(DataContext Context)
        {
            this._values.Clear();
            return CRdsAttributeValue.sAttributeValuesDelete(this._elementVersion, this._attributeKey, Context);
        }
        public int AttributeValuesGet(DataContext Context)
        {
            this._values.Clear();

            var Values = CRdsAttributeValue.sGetAttributeValues(this._elementVersion, this._attributeKey, Context);
            foreach (CRdsAttributeValue Value in Values)
                this._values.Add(Value.AttributeLocale, Value);
            return -1;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static CRdsAttributeValues sFindValuesByAttributeKey(decimal AttributeKey, List<CRdsAttributeValues> Values)
        {
            var Query = from Value in Values
                        where Value.AttributeKey == AttributeKey
                        select Value;
            return (Query.Count() == 0 ? null : Query.ToList()[0]);
        }
        public static CRdsAttributeValues sFindValuesByElementVersion(decimal ElementVersion, List<CRdsAttributeValues> Values)
        {
            var Query = from Value in Values
                        where Value.ElementVersion == ElementVersion
                        select Value;
            return (Query.Count() == 0 ? null : Query.ToList()[0]);
        }

        public static List<CRdsAttributeValues> sGetValuesByElementVersion(decimal ElementVersion, DataContext Context)
        {
            List<CRdsAttributeValues> R = new List<CRdsAttributeValues>();

            var Values = CRdsAttributeValue.sGetAttributeValuesByElement(ElementVersion, Context);
            decimal AttrKey, PrevAttrKey;
            PrevAttrKey = -1;
            AttrKey = -1;

            CRdsAttributeValues CurrentValue = null;

            foreach (CRdsAttributeValue Value in Values)
            {
                AttrKey = Value.AttributeKey;
                if (AttrKey != PrevAttrKey)
                {
                    if (CurrentValue != null)
                        R.Add(CurrentValue);
                    CurrentValue = new CRdsAttributeValues()
                    {
                        AttributeKey = AttrKey,
                        Context = Context,
                        ElementVersion = Value.ElementVersion
                    };
                }
                else
                    CurrentValue.Values.Add(Value.AttributeLocale, Value);
            }

            return R;
        }
        public static CRdsAttributeValues sGetValues(decimal ElementVersion, decimal AttributeKey, DataContext Context)
        {
            var Values = CRdsAttributeValue.sGetAttributeValues(ElementVersion, AttributeKey, Context);
            CRdsAttributeValues R = new CRdsAttributeValues()
            {
                AttributeKey = AttributeKey,
                ElementVersion = ElementVersion,
                Context = Context
            };
            foreach (CRdsAttributeValue Value in Values)
                R.Values.Add(Value.AttributeLocale, Value);

            return R;
        }
        #endregion

    }
}
