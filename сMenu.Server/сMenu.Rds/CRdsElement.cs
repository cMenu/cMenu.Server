using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using cMenu.IO;
using cMenu.DB;
using cMenu.Common;
using cMenu.Common.Base;

namespace cMenu.Rds
{
    [Serializable]
    public class CRdsElement
    {
        #region PROTECTED FIELDS
        protected IDatabaseProvider _provider = null;
        protected List<CRdsElement> _children = new List<CRdsElement>();

        protected Hashtable _values = new Hashtable();
        protected decimal _parent = CRDSConst.CONST_RDS_ROOT_PARENT;
        protected decimal _key = -1;
        protected decimal _version = -1;
        protected decimal _dictionaryKey = -1;        
        protected bool _immediateUpdate = false;        
        #endregion

        #region PUBLIC FIELDS
        public IDatabaseProvider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }
        public List<CRdsElement> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public Hashtable Values
        {
            get { return _values; }
            set { _values = value; }
        }
        public decimal Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        public decimal DictionaryKey
        {
            get { return _dictionaryKey; }
            set { _dictionaryKey = value; }
        }
        public decimal Version
        {
            get { return _version; }
            set { _version = value; }
        }
        public decimal Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public bool ImmediateUpdate
        {
            get { return _immediateUpdate; }
            set { _immediateUpdate = value; }
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
            object R;
            R = (this._values.ContainsKey(AttributeKey) ? this._values[AttributeKey] : null);
            if (R == null)
            {
                CRdsAttributeValue Value = new CRdsAttributeValue() { AttributeKey = AttributeKey, Provider = this._provider, ElementVersion = this._version };
                var RR = Value.ValueGetAll(this._provider);
                if (RR == -1)
                    R = null;
                else
                {
                    R = Value;
                    this._values.Add(AttributeKey, Value);
                }
            }
            return (CRdsAttributeValue)R;
        }
        protected object _getValue(decimal AttributeKey, int LanguageCode)
        {
            var AttributeValue = this[AttributeKey];
            if (AttributeValue == null)
                return null;

            var Value = AttributeValue[LanguageCode];
            return Value;
        }
        protected int _setValue(decimal AttributeKey, CRdsAttributeValue Value)
        {
            if (this._values.ContainsKey(AttributeKey))
                this._values[AttributeKey] = Value;
            else
                this._values.Add(AttributeKey, Value);

            if (this._immediateUpdate)
                Value.ValueUpdateAll(this._provider);
            return -1;
        }
        protected int _setValue(decimal AttributeKey, int LanguageCode, object Value)
        {
            var AttributeValue = this[AttributeKey];
            if (AttributeValue == null)
            {
                AttributeValue = new CRdsAttributeValue()
                {
                    AttributeKey = AttributeKey,
                    ElementVersion = this._version,
                    ImmediateUpdate = this._immediateUpdate,
                    Provider = this._provider
                };
                this._values.Add(AttributeKey, AttributeValue);
            }

            AttributeValue[LanguageCode] = Value;
            if (this._immediateUpdate)
                AttributeValue.ValueUpdate(LanguageCode, this._provider);

            return -1;
        }
        protected int _refreshElementVersion()
        {
            decimal[] Keys = new decimal[this._values.Keys.Count];
            this._values.Keys.CopyTo(Keys, 0);

            foreach (decimal Key in Keys)
            {
                CRdsAttributeValue Value = (CRdsAttributeValue)this._values[Key];
                Value.ElementVersion = this._version;
            }

            return -1;
        }
        #endregion

        #region CONSTRUCTORS
        public CRdsElement(IDatabaseProvider Provider) 
            : base()
        {
            this._provider = Provider;
        }
        public CRdsElement(decimal ElementVersion, IDatabaseProvider Provider)
            : base()
        {
            this._version = ElementVersion;
            this._provider = Provider;

            this.ElementGet(Provider);
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int ElementInsertValues(IDatabaseProvider Provider)
        {
            decimal[] Keys = new decimal[this._values.Keys.Count];
            this._values.Keys.CopyTo(Keys, 0);

            foreach (decimal Key in Keys)
            {
                CRdsAttributeValue Value = (CRdsAttributeValue)this._values[Key];
                Value.ValueInsertAll(Provider);
            }

            return -1;
        }
        public int ElementUpdateValues(IDatabaseProvider Provider)
        {
            decimal[] Keys = new decimal[this._values.Keys.Count];
            this._values.Keys.CopyTo(Keys, 0);

            foreach (decimal Key in Keys)
            {
                CRdsAttributeValue Value = (CRdsAttributeValue)this._values[Key];
                Value.ValueUpdateAll(Provider);
            }

            return -1;
        }
        public int ElementDeleteValues(IDatabaseProvider Provider)
        {
            decimal[] Keys = new decimal[this._values.Keys.Count];
            this._values.Keys.CopyTo(Keys, 0);

            foreach (decimal Key in Keys)
            {
                CRdsAttributeValue Value = (CRdsAttributeValue)this._values[Key];
                Value.ValueDeleteAll(Provider);
            }

            return -1;
        }

        public int ElementGet(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();

            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, this._version);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT;
            SQL += " FROM " + CDBConst.CONST_TABLE_RDS_ELEM + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            this._key = T.Rows[0][0].CheckDBNULLValue<decimal>();
            this._version = T.Rows[0][1].CheckDBNULLValue<decimal>();
            this._dictionaryKey = T.Rows[0][2].CheckDBNULLValue<decimal>();
            this._parent = T.Rows[0][3].CheckDBNULLValue<decimal>();

            this._refreshElementVersion();

            return -1;
        }
        public int ElementInsert(IDatabaseProvider Provider)
        {
            this._key = CDatabaseSequence.sGetRdsKey(Provider);
            this._version = CDatabaseSequence.sGetVersionKey(Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY, this._dictionaryKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, this._version);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT, this._parent);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_RDS_ELEM + " (";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT + ")";
            SQL += " VALUES (";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY + ", ";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + ", @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT + ")";

            var R = Provider.QueryExecute(SQL, false, Params);
            if (R)
                this._refreshElementVersion();
            return (R ? -1 : -2);
        }
        public int ElementUpdate(IDatabaseProvider Provider)
        {
            var Exists = this.ElementExists(Provider);
            if (!Exists)
                return this.ElementInsert(Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, this._version);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY, this._dictionaryKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT, this._parent);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_RDS_ELEM + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY;

            var R = Provider.QueryExecute(SQL, false, Params);
            return (R ? -1 : -2);
        }
        public int ElementDelete(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY, this._key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ELEM + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY;
            var R = Provider.QueryExecute(SQL, false, Params);
            
            var RR = CRdsElement.sElementsChildrenDelete(this._key, this._dictionaryKey, Provider);
            if (RR != -1)
                return RR;

            RR = CRdsAttributeValue.sValuesDeleteByElement(this._version, Provider);
            if (RR != -1)
                return RR;

            return (R ? -1 : -2);
        }
        public bool ElementExists(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, this._version);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY, this._key);

            var SQL = "SELECT Count(*) FROM " + CDBConst.CONST_TABLE_RDS_ELEM + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY;
            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null)
                return false;
            var RecordsCount = (int)T.Rows[0][0];
            return (RecordsCount > 0);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CRdsElement> sElementsGetByDictionary(decimal DictionaryKey, IDatabaseProvider Provider)
        {
            Hashtable DelayedElements = new Hashtable();
            List<CRdsElement> R = new List<CRdsElement>();
            Hashtable Params = new Hashtable();
            CRdsElement TempElement = null;

            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY, DictionaryKey);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT;
            SQL += " FROM " + CDBConst.CONST_TABLE_RDS_ELEM + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null)
                return R;
            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Element = new CRdsElement(Provider);
                Element.Key = T.Rows[i][0].CheckDBNULLValue<decimal>();
                Element.Version = T.Rows[i][1].CheckDBNULLValue<decimal>();
                Element.DictionaryKey = T.Rows[i][2].CheckDBNULLValue<decimal>();
                Element.Parent = T.Rows[i][3].CheckDBNULLValue<decimal>();

                if (DelayedElements.ContainsKey(Element.Key))
                {
                    List<CRdsElement> ChildElements = (List<CRdsElement>)DelayedElements[Element.Parent];
                    Element.Children.AddRange(ChildElements);
                    DelayedElements.Remove(Element.Parent);
                }

                if (Element.Parent == CRDSConst.CONST_RDS_ROOT_PARENT)
                {
                    R.Add(Element);
                    continue;
                }

                TempElement = CRdsElement.sFindElementByKey(R, Element.Parent);
                if (TempElement == null)
                {
                    List<CRdsElement> Elements = null;
                    if (DelayedElements.ContainsKey(Element.Parent))
                    {
                        Elements = (List<CRdsElement>)DelayedElements[Element.Parent];
                        Elements.Add(Element);
                        DelayedElements[Element.Parent] = Elements;
                    }
                    else
                    {
                        Elements = new List<CRdsElement>();
                        Elements.Add(Element);
                        DelayedElements.Add(Element.Parent, Element);
                    }
                }
                else
                    TempElement.Children.Add(Element);

            }

            return R;
        }
        public static int sElementsDeleteByDictionary(decimal DictionaryKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY, DictionaryKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ELEM_DATA + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " IN (";
            SQL += "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " FROM " + CDBConst.CONST_TABLE_RDS_ELEM + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY;
            SQL += ")";
            var R = Provider.QueryExecute(SQL, false, Params);
            if (!R)
                return -3;

            SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ELEM + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY;
            R = Provider.QueryExecute(SQL, false, Params);

            return (R ? -1 : -3);
        }
        public static int sElementsChildrenDelete(decimal ElementKey, decimal DictionaryKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT, ElementKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY, DictionaryKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ELEM_DATA + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " IN (";
            SQL += "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " FROM " + CDBConst.CONST_TABLE_RDS_ELEM + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY;
            SQL += ")";
            var R = Provider.QueryExecute(SQL, false, Params);
            if (!R)
                return -3;

            SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY + " FROM " + CDBConst.CONST_TABLE_RDS_ELEM + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null)
                return -2;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Key = (decimal)T.Rows[i][0];
                CRdsElement.sElementsChildrenDelete(Key, DictionaryKey, Provider);
            }
            SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ELEM + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY;
            R = Provider.QueryExecute(SQL, false, Params);

            return (R ? -1 : -4);
        }

        public static CRdsElement sFindElementByKey(List<CRdsElement> Elements, decimal Key, bool Recursive = true)
        {
            CRdsElement TempElement = null;
            foreach(CRdsElement Element in Elements)
            {
                if (Element.Key == Key)
                    return Element;
                if (Element.Children.Count > 0 && Recursive)
                {
                    TempElement = CRdsElement.sFindElementByKey(Element.Children, Key, Recursive);
                    if (TempElement != null)
                        return TempElement;
                }
            }
            return null;
        }
        #endregion
    }
}
