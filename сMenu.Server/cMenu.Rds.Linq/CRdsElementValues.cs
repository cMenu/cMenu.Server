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
    public class CRdsElementValues
    {
        #region PROTECTED FIELDS
        protected decimal _elementVersion = -1;
        protected Dictionary<decimal, CRdsAttributeValues> _values = new Dictionary<decimal, CRdsAttributeValues>();
        protected DataContext _context = null;
        #endregion

        #region PUBLIC FIELDS
        public decimal ElementVersion
        {
            get { return _elementVersion; }
            set { _elementVersion = value; }
        }
        public Dictionary<decimal, CRdsAttributeValues> Values
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
        public CRdsAttributeValues this[decimal AttributeKey]
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

        #region CONSTRUCTORS
        #endregion

        #region PROTECTED FUNCTIONS
        protected CRdsAttributeValues _getValue(decimal AttributeKey)
        {
            var R = CRdsAttributeValues.sFindValuesByAttributeKey(AttributeKey, this._values.Values.ToList());
            if (R != null)
                return R;

            R = this._loadValues(AttributeKey);
            if (R != null)
            {
                this._values.Add(AttributeKey, R);
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
        protected int _setValue(decimal AttributeKey, CRdsAttributeValues Value)
        {
            var R = this._getValue(Value.AttributeKey);
            if (R != null)
                this._values.Remove(AttributeKey);
            this._values.Add(AttributeKey, Value);

            return -1;
        }
        protected int _setValue(decimal AttributeKey, int LanguageCode, object Value)
        {
            var R = this._getValue(AttributeKey);
            if (R == null)
            {
                R = new CRdsAttributeValues()
                {
                    AttributeKey = AttributeKey,
                    ElementVersion = this._elementVersion,
                    Context = this._context
                };                
                this._values.Add(AttributeKey, R);
            }

            R[LanguageCode].AttributeValue = Value;

            return -1;
        }

        protected List<CRdsAttributeValues> _loadValues()
        {
            return CRdsAttributeValues.sGetValuesByElementVersion(this._elementVersion, this._context);
        }
        protected CRdsAttributeValues _loadValues(decimal AttributeKey)
        {
            var R = CRdsAttributeValues.sGetValues(this._elementVersion, AttributeKey, this._context);
            return R;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int AttributeValueLoad(decimal AttributeKey, DataContext Context)
        {
            var R = this._getValue(AttributeKey);
            return (R != null ? -1 : -2);
        }
        public int AttributeValuesUpdate(DataContext Context)
        {
            foreach (CRdsAttributeValues Value in this._values.Values)
                Value.AttributeValuesUpdate(Context);
            return -1;
        }
        public int AttributeValuesInsert(DataContext Context)
        {
            foreach (CRdsAttributeValues Value in this._values.Values)
                Value.AttributeValuesInsert(Context);
            return -1;
        }
        public int AttributeValuesDelete(DataContext Context)
        {
            CRdsAttributeValue.sAttributeValuesDeleteByElement(this._elementVersion, Context);
            return -1;
        }
        public int AttributesGet(DataContext Context)
        {
            this._values.Clear();
            var R = this._loadValues();
            foreach (CRdsAttributeValues Value in R)
                this._values.Add(Value.AttributeKey, Value);
            return -1;
        }
        #endregion

    }
}
