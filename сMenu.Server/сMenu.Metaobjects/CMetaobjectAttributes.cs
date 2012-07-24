using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Common;
using cMenu.IO;
using cMenu.Common.Base;

namespace cMenu.Metaobjects
{
    [Serializable]
    public class CMetaobjectAttributes
    {
        #region PROTECTED FIELDS
        protected IDatabaseProvider _provider;
        protected decimal _objectKey = -1;
        protected List<CMetaobjectAttributeValue> _values = new List<CMetaobjectAttributeValue>();        
        #endregion        

        #region PUBLIC FIELDS
        public IDatabaseProvider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }
        public List<CMetaobjectAttributeValue> Values
        {
            get { return _values; }
            set { _values = value; }
        }
        public decimal ObjectKey
        {
            get { return _objectKey; }
            set 
            { 
                _objectKey = value;
                foreach (CMetaobjectAttributeValue Value in this._values)
                    Value.ObjectKey = value;
            }
        }
        public CMetaobjectAttributeValue this[string ID]
        {
            get 
            {
                return _getValue(ID);
            }
            set
            {
                _setValue(value);
            }
        }
        public object this[string ID, int Locale]
        {
            get
            {
                return _getValue(ID, Locale);
            }
            set
            {
                _setValue(ID, Locale, value);
            }
        }        
        #endregion        

        #region PROTECTED FUNCTIONS
        protected CMetaobjectAttributeValue _getValue(string ID)
        {
            var R = CMetaobjectAttributeValue.sFindValueByID(ID, this._values);
            if (R != null)
                return R;

            R = this._loadValue(ID);
            if (R != null)
            {
                this._values.Add(R);
                return R;
            }

            return null;

        }
        protected object _getValue(string ID, int Locale)
        {
            var R = this._getValue(ID);
            if (R == null)
                return null;

            var Value = R[Locale];
            if (Value != null)
                return Value;



            return R[Locale];
        }
        protected int _setValue(CMetaobjectAttributeValue Value)
        {
            var R = CMetaobjectAttributeValue.sFindValueByID(Value.AttributeID, this._values);
            if (R != null)
                this._values.Remove(R);
            this._values.Add(Value);

            return -1;
        }
        protected int _setValue(string ID, int Locale, object Value)
        {
            var R = CMetaobjectAttributeValue.sFindValueByID(ID, this._values);
            if (R == null)
            {
                R = new CMetaobjectAttributeValue(this._provider)
                {
                    ObjectKey = this._objectKey,
                    AttributeID = ID
                };
                this._values.Add(R);
            }

            R[Locale] = Value;
            return -1;
        }

        protected List<CMetaobjectAttributeValue> _loadValues()
        {
            return CMetaobjectAttributeValue.sGetValuesByObject(this._objectKey, this._provider);
        }
        protected CMetaobjectAttributeValue _loadValue(string ID)
        {
            var R = CMetaobjectAttributeValue.sGetValue(this._objectKey, ID, this._provider);
            return R;
        }
        #endregion        

        #region PUBLIC FUNCTIONS
        public int AttributeLoad(string ID, IDatabaseProvider Provider)
        {
            var R = this._getValue(ID);
            return (R != null ? -1 : -2);
        }
        public int AttributesUpdate(IDatabaseProvider Provider)
        {
            foreach (CMetaobjectAttributeValue Value in this._values)
                Value.ValueUpdate(this._provider);
            return -1;
        }
        public int AttributesInsert(IDatabaseProvider Provider)
        {
            foreach (CMetaobjectAttributeValue Value in this._values)
                Value.ValueInsert(this._provider);
            return -1;
        }
        public int AttributesDelete(IDatabaseProvider Provider)
        {
            CMetaobjectAttributeValue.sDeleteValuesByObject(this._objectKey, this._provider);
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
