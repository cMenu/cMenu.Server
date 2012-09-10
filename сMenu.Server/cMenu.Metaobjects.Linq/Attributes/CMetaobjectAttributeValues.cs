using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

using cMenu.DB;
using cMenu.Common;
using Newtonsoft.Json;

namespace cMenu.Metaobjects.Linq
{
    [Serializable]
    public class CMetaobjectAttributeValues
    {
        #region PROTECTED FIELDS
        protected decimal _objectKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected string _attributeID = "";
        protected Dictionary<int, CMetaobjectAttributeValue> _values = new Dictionary<int, CMetaobjectAttributeValue>();

        protected DataContext _context = null;
        #endregion

        #region PUBLIC FIELDS
        public decimal ObjectKey
        {
            get { return _objectKey; }
            set 
            { 
                _objectKey = value;
                foreach (CMetaobjectAttributeValue Value in _values.Values)
                    Value.ObjectKey = value;
            }
        }
        public string AttributeID
        {
            get { return _attributeID; }
            set { _attributeID = value; }
        }
        public Dictionary<int, CMetaobjectAttributeValue> Values
        {
            get { return _values; }
            set { _values = value; }
        }

        [JsonIgnore]
        public DataContext Context
        {
            get { return _context; }
            set 
            { 
                _context = value;
            }
        }
        [JsonIgnore]
        public CMetaobjectAttributeValue this[int AttributeLocale]
        {
            get { return _getValue(AttributeLocale); }
            set { _setValue(AttributeLocale, value); }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected CMetaobjectAttributeValue _getValue(int AttributeLocale)
        {
            CMetaobjectAttributeValue R = (this._values.ContainsKey(AttributeLocale) ? this._values[AttributeLocale] : null);
            if (R != null)
                return R;

            R = CMetaobjectAttributeValue.sGetAttributeValue(this._objectKey, this._attributeID, AttributeLocale, this._context);
            if (R == null)
            {
                R = new CMetaobjectAttributeValue()
                {
                    AttributeID = this._attributeID,
                    AttributeLocale = AttributeLocale,
                    ObjectKey = this._objectKey
                };
            }

            this._values.Add(AttributeLocale, R);
            return R;
        }
        protected int _setValue(int AttributeLocale, CMetaobjectAttributeValue Value)
        {
            if (this._values.ContainsKey(AttributeLocale))
                this._values[AttributeLocale] = Value;
            else
                this._values.Add(AttributeLocale, Value);

            return -1;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int AttributeValuesInsert(DataContext Context)
        {
            foreach (CMetaobjectAttributeValue Value in this._values.Values)
                Value.AttributeValueInsert(Context);
            return CErrors.ERR_SUC;
        }
        public int AttributeValuesUpdate(DataContext Context)
        {
            foreach (CMetaobjectAttributeValue Value in this._values.Values)
                Value.AttributeValueUpdate(Context);

            return CErrors.ERR_SUC;
        }
        public int AttributeValuesDelete(DataContext Context)
        {
            this._values.Clear();
            return CMetaobjectAttributeValue.sDeleteAttributeValues(this.ObjectKey, this.AttributeID, Context);
        }
        public int AttributeValuesGet(DataContext Context)
        {
            this._values.Clear();

            var Values = CMetaobjectAttributeValue.sGetAttributeValues(this.ObjectKey, this.AttributeID, Context);
            foreach (CMetaobjectAttributeValue Value in Values)
                this._values.Add(Value.AttributeLocale, Value);

            return CErrors.ERR_SUC;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static CMetaobjectAttributeValues sGetAttributeValues(decimal ObjectKey, string AttributeID, DataContext Context)
        {
            CMetaobjectAttributeValues R = new CMetaobjectAttributeValues()
            {
                ObjectKey = ObjectKey,
                AttributeID = AttributeID,
                Context = Context
            };
            R.AttributeValuesGet(Context);

            return R;
        }
        public static List<CMetaobjectAttributeValues> sGetAttributeValues(decimal ObjectKey, DataContext Context)
        {
            Dictionary<string, CMetaobjectAttributeValues> R = new Dictionary<string,CMetaobjectAttributeValues>();

            var Values = CMetaobjectAttributeValue.sGetAttributeValues(ObjectKey, Context);
            foreach (CMetaobjectAttributeValue Value in Values)
            {
                if (R.ContainsKey(Value.AttributeID))
                {
                    var AttrValues = R[Value.AttributeID];
                    AttrValues[Value.AttributeLocale] = Value;
                }
                else
                {
                    CMetaobjectAttributeValues AttrValues = new CMetaobjectAttributeValues()
                    {
                        ObjectKey = ObjectKey,
                        AttributeID = Value.AttributeID,
                        Context = Context
                    };
                    AttrValues[Value.AttributeLocale] = Value;
                    R.Add(Value.AttributeID, AttrValues);
                }
            }

            return R.Values.ToList();
        }

        public static int sDeleteAttributeValues(decimal ObjectKey, DataContext Context)
        {
            return CMetaobjectAttributeValue.sDeleteAttributeValues(ObjectKey, Context);
        }
        public static int sDeleteAttributeValues(decimal ObjectKey, string AttributeID, DataContext Context)
        {
            return CMetaobjectAttributeValue.sDeleteAttributeValues(ObjectKey, AttributeID, Context);
        }
        #endregion
    }
}
