using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

using cMenu.DB;
using cMenu.IO;
using cMenu.Common;
using Newtonsoft.Json;

namespace cMenu.Metaobjects.Linq
{
    [Serializable]
    public class CMetaobjectAttributes
    {
        #region PROTECTED FIELDS
        protected decimal _objectKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected Dictionary<string, CMetaobjectAttributeValues> _values = new Dictionary<string, CMetaobjectAttributeValues>();

        protected DataContext _context = null;
        #endregion

        #region PUBLIC FIELDS
        public decimal ObjectKey
        {
            get { return _objectKey; }
            set 
            { 
                _objectKey = value;
                foreach (CMetaobjectAttributeValues Values in _values.Values)
                    Values.ObjectKey = value;
            }
        }
        public Dictionary<string, CMetaobjectAttributeValues> Values
        {
            get { return _values; }
            set { _values = value; }
        }

        [JsonIgnore]
        public CMetaobjectAttributeValues this[string AttributeID]
        {
            get { return _getValue(AttributeID); }
            set { _setValue(AttributeID, value); }
        }
        [JsonIgnore]
        public DataContext Context
        {
            get { return _context; }
            set 
            { 
                _context = value;
                foreach (CMetaobjectAttributeValues Values in this.Values.Values)
                    Values.Context = value;
            }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected CMetaobjectAttributeValues _getValue(string AttributeID)
        {
            CMetaobjectAttributeValues R = (this._values.ContainsKey(AttributeID) ? this._values[AttributeID] : null);
            if (R != null)
                return R;

            R = this.GetValuesByAttribute(this._objectKey, AttributeID, this._context);
            if (R == null)
            {
                R = new CMetaobjectAttributeValues()
                {
                    AttributeID = AttributeID,
                    ObjectKey = this._objectKey,
                    Context = this._context
                };
            }
            this._values.Add(AttributeID, R);
            return R;
        }
        protected int _setValue(string AttributeID, CMetaobjectAttributeValues Value)
        {
            Value.Context = this._context;
            if (this._values.ContainsKey(AttributeID))
                this._values[AttributeID] = Value;
            else
                this._values.Add(AttributeID, Value);

            return CErrors.ERR_SUC;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public CMetaobjectAttributeValues GetValuesByAttribute(decimal ObjectKey, string AttributeID, DataContext Context)
        {
            CMetaobjectAttributeValues R = new CMetaobjectAttributeValues();
            var Attributes = Context.GetTable<CMetaobjectAttributeValue>();
            var Query = from Attr in Attributes
                        where 
                            Attr.ObjectKey == ObjectKey && 
                            Attr.AttributeID == AttributeID
                        select Attr;

            if (Query.Count() == 0)
                return null;

            R.Context = Context;
            R.ObjectKey = ObjectKey;
            R.AttributeID = AttributeID;

            foreach (CMetaobjectAttributeValue Value in Query)
                R.Values.Add(Value.AttributeLocale, Value);

            return R;
        }

        public int AttributesInsert(DataContext Context)
        {
            foreach (CMetaobjectAttributeValues Values in this._values.Values)
            {
                var R = Values.AttributeValuesInsert(Context);
                if (R != CErrors.ERR_SUC)
                    return CErrors.ERR_DB_INSERT_OBJECT;
            }

            return CErrors.ERR_SUC;
        }
        public int AttributesUpdate(DataContext Context)
        {
            foreach (CMetaobjectAttributeValues Values in this._values.Values)
            {
                var R = Values.AttributeValuesUpdate(Context);
                if (R != CErrors.ERR_SUC)
                    return CErrors.ERR_DB_UPDATE_OBJECT;
            }

            return CErrors.ERR_SUC;
        }
        public int AttributesDelete(DataContext Context)
        {
            this._values.Clear();
            return CMetaobjectAttributeValue.sDeleteAttributeValues(ObjectKey, Context);
        }
        public int AttributesGet(DataContext Context)
        {
            this._values.Clear();

            var Values = CMetaobjectAttributeValues.sGetAttributeValues(this.ObjectKey, Context);
            foreach (CMetaobjectAttributeValues Value in Values)
            {
                Value.Context = Context;
                this._values.Add(Value.AttributeID, Value);
            }
           
            return CErrors.ERR_SUC;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static CMetaobjectAttributes sGetAttributes(decimal ObjectKey, DataContext Context)
        {
            CMetaobjectAttributes R = new CMetaobjectAttributes()
            {
                ObjectKey = ObjectKey,
                Context = Context
            };
            R.AttributesGet(Context);

            return R;
        }
        public static int sDeleteAttributes(decimal ObjectKey, DataContext Context)
        {
            return CMetaobjectAttributeValue.sDeleteAttributeValues(ObjectKey, Context);
        }
        #endregion
    }
}
