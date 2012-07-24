using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.IO;

namespace cMenu.Metaobjects
{
    [Serializable]
    public class CMetaobjectAttributeValue
    {
        #region PROTECTED FIELDS
        protected decimal _objectKey = -1;
        protected string _attributeID = "";
        protected Dictionary<int, object> _values = new Dictionary<int, object>();
        protected IDatabaseProvider _provider;
        #endregion

        #region PUBLIC FIELDS
        public decimal ObjectKey
        {
            get { return _objectKey; }
            set { _objectKey = value; }
        }
        public string AttributeID
        {
            get { return _attributeID; }
            set { _attributeID = value; }
        }
        public Dictionary<int, object> Values
        {
            get { return _values; }
            set { _values = value; }
        }
        public object this[int Locale]
        {
            get
            {
                return _getValue(Locale);
            }
            set
            {
                _setValue(Locale, value);
            }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected object _getValue(int Locale)
        {
            object R = (this._values.ContainsKey(Locale) ? this._values[Locale] : this._values[-1]);
            if (R != null)
                return R;

            R = CMetaobjectAttributeValue.sGetValue(this._objectKey, this._attributeID, Locale, this._provider);
            if (R == null)
                return null;

            this._values.Add(Locale, R);
            return R;
        }
        protected int _setValue(int Locale, object Value)
        {
            if (this._values.ContainsKey(Locale))
                this._values[Locale] = Value;
            else
                this._values.Add(Locale, Value);
            return -1;
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaobjectAttributeValue(IDatabaseProvider Provider)
        {
            this._provider = Provider;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int ValueGet(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._objectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID, this._attributeID);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS_ATTR;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            this._values.Clear();

            for (int i = 0; i < T.Rows.Count; i++)
            {    
                byte[] Data = T.Rows[i][3].CheckDBNULLValue<byte[]>(new byte[0]);
                var Stream = Data.ToDataStream();
                var Obj = Stream.DeserializeBinaryStream<object>();

                this._values.Add(T.Rows[i][2].CheckDBNULLValue<int>(-1), Obj);
            }
            
            return -1;
        }
        public int ValueDelete(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._objectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID, this._attributeID);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS_ATTR;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID;

            var T = Provider.QueryExecute(SQL, false, Params);               

            return (T ? -1 : -2);
        }
        public int ValueUpdate(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            var SQL = "";

            var Keys = new int[this._values.Keys.Count];
            this._values.Keys.CopyTo(Keys, 0);

            foreach (int Key in Keys)
            {
                var Stream = this._values[Key].SerializeBinaryStream();
                var Data = Stream.ToDataByteArray();

                var ExistingValue = CMetaobjectAttributeValue.sGetValue(this._objectKey, this._attributeID, Key, Provider);
                if (ExistingValue == null)
                {
                    Params.Clear();
                    Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._objectKey);
                    Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID, this._attributeID);
                    Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE, Key);
                    Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE, Data);

                    SQL = "INSERT INTO " + CDBConst.CONST_TABLE_OBJECTS_ATTR + " (";
                    SQL += CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + ", ";
                    SQL += CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE;
                    SQL += ") VALUES (";
                    SQL += "@p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + ", @p";
                    SQL += CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE + ", @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE + ")";

                    Provider.QueryExecute(SQL, false, Params);
                    continue;
                }

                Params.Clear();
                Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._objectKey);
                Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID, this._attributeID);
                Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE, Key);
                Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE, Data);

                SQL = "UPDATE " + CDBConst.CONST_TABLE_OBJECTS_ATTR + " SET ";
                SQL += CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE;
                SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
                SQL += " AND "+ CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID;
                SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE;

                Provider.QueryExecute(SQL, false, Params);
            }

            return -1;
        }
        public int ValueInsert(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            var SQL = "";
            
            var Keys = new int[this._values.Keys.Count];
            this._values.Keys.CopyTo(Keys, 0);

            foreach (int Key in Keys)
            {
                var Stream = this._values[Key].SerializeBinaryStream();
                var Data = Stream.ToDataByteArray();
                var ExistingValue = CMetaobjectAttributeValue.sGetValue(this._objectKey, this._attributeID, Key, Provider);

                if (ExistingValue != null)
                {
                    Params.Clear();
                    Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._objectKey);
                    Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID, this._attributeID);
                    Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE, Key);
                    Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE, Data);

                    SQL = "UPDATE " + CDBConst.CONST_TABLE_OBJECTS_ATTR + " SET ";
                    SQL += CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE;
                    SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
                    SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID;
                    SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE;

                    Provider.QueryExecute(SQL, false, Params);
                    continue;
                }

                Params.Clear();
                Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._objectKey);
                Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID, this._attributeID);
                Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE, Key);
                Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE, Data);

                SQL = "INSERT INTO " + CDBConst.CONST_TABLE_OBJECTS_ATTR + " (";
                SQL += CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + ", ";
                SQL += CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE;
                SQL += ") VALUES (";
                SQL += "@p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + ", @p";
                SQL += CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE + ", @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE + ")";

                Provider.QueryExecute(SQL, false, Params);
            }

            return -1;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static CMetaobjectAttributeValue sGetValue(decimal ObjectKey, string AttributeID, IDatabaseProvider Provider)
        {
            CMetaobjectAttributeValue R = null;

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, ObjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID, AttributeID);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS_ATTR;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return null;

            R = new CMetaobjectAttributeValue(Provider);
            R.ObjectKey = T.Rows[0][0].CheckDBNULLValue<decimal>(-1);
            R.AttributeID = T.Rows[0][1].CheckDBNULLValue<string>("");

            for (int i = 0; i < T.Rows.Count; i++)
            {
                byte[] Data = T.Rows[i][3].CheckDBNULLValue<byte[]>(new byte[0]);
                var Stream = Data.ToDataStream();
                var Obj = Stream.DeserializeBinaryStream<object>();

                if (R.Values.ContainsKey(T.Rows[i][2].CheckDBNULLValue<int>(-1)))
                    continue;
                R.Values.Add(T.Rows[i][2].CheckDBNULLValue<int>(-1), Obj);
            }

            return R;
        }
        public static List<CMetaobjectAttributeValue> sGetValuesByObject(decimal ObjectKey, IDatabaseProvider Provider)
        {
            List<CMetaobjectAttributeValue> R = new List<CMetaobjectAttributeValue>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, ObjectKey);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS_ATTR;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += " ORDER BY " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            string PreviousID = "";
            string CurrentID = "";
            CMetaobjectAttributeValue Value = null;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                CurrentID = T.Rows[i][1].CheckDBNULLValue<string>("");
                if (PreviousID != CurrentID)
                {
                    if (Value != null)
                        R.Add(Value);

                    Value = new CMetaobjectAttributeValue(Provider);
                    Value.ObjectKey = T.Rows[i][0].CheckDBNULLValue<decimal>(-1);
                    Value.AttributeID = T.Rows[i][1].CheckDBNULLValue<string>("");
                }

                byte[] Data = T.Rows[i][3].CheckDBNULLValue<byte[]>(new byte[0]);
                var Stream = Data.ToDataStream();
                var Obj = Stream.DeserializeBinaryStream<object>();

                Value.Values.Add(T.Rows[i][2].CheckDBNULLValue<int>(-1), Obj);
                PreviousID = CurrentID;
            }

            return R;
        }
        public static object sGetValue(decimal ObjectKey, string AttributeID, int Locale, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, ObjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID, AttributeID);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE, Locale);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS_ATTR;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_LOCALE;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return null;

            byte[] Data = T.Rows[0][3].CheckDBNULLValue<byte[]>(new byte[0]);
            var Stream = Data.ToDataStream();
            var Obj = Stream.DeserializeBinaryStream<object>();

            return Obj;
        }
        public static int sDeleteValuesByObject(decimal ObjectKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, ObjectKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS_ATTR;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public static int sDeleteValues(decimal ObjectKey, string AttributeID, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, ObjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID, AttributeID);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS_ATTR;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_ID;

            var T = Provider.QueryExecute(SQL, false, Params);               

            return (T ? -1 : -2);
        }
        public static CMetaobjectAttributeValue sFindValueByID(string AttributeID, List<CMetaobjectAttributeValue> Values)
        {
            foreach (CMetaobjectAttributeValue Value in Values)
                if (Value.AttributeID == AttributeID)
                    return Value;

            return null;
        }
        #endregion
    }
}

