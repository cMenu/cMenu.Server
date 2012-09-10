using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.IO;

namespace cMenu.Rds
{
    public static class CRdsAttributeValueExt
    {
        public static CRdsAttributeValue FindByKey(this List<CRdsAttributeValue> List, decimal AttributeKey)
        {
            return CRdsAttributeValue.sFindAttributeValueByKey(AttributeKey, List);
        }
    }

    [Serializable]
    public class CRdsAttributeValue
    {
        #region PROTECTED FIELDS
        protected IDatabaseProvider _provider = null;
        protected decimal _attributeKey = -1;
        protected decimal _elementVersion = -1;
        protected Dictionary<int, object> _values = new Dictionary<int, object>();
        protected bool _immediateUpdate = false;
        #endregion

        #region PUBLIC FIELDS
        public IDatabaseProvider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }
        public decimal AttributeKey
        {
            get { return _attributeKey; }
            set { _attributeKey = value; }
        }
        public decimal ElementVersion
        {
            get { return _elementVersion; }
            set { _elementVersion = value; }
        }
        public Dictionary<int, object> Values
        {
            get { return _values; }
            set { _values = value; }
        }
        public bool ImmediateUpdate
        {
            get { return _immediateUpdate; }
            set { _immediateUpdate = value; }
        }
        public object this[int LanguageCode]
        {
            get { return _getValue(LanguageCode); }
            set { _setValue(LanguageCode, value); }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected object _getValue(int LanguageCode)
        {
            object R = null;

            R = this._values[LanguageCode];
            if (R != null)
                return R;

            this.ValueGet(LanguageCode, this._provider);
            R = this._values[LanguageCode];
            return R;
        }
        protected int _setValue(int LanguageCode, object Value)
        {
            if (this._values.ContainsKey(LanguageCode))
                this._values[LanguageCode] = Value;
            else
                this._values.Add(LanguageCode, Value);

            if (this._immediateUpdate)
                this.ValueUpdate(LanguageCode, Provider);
            return -1;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int ValueGet(int LanguageCode, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._attributeKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, this._elementVersion);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE, LanguageCode);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_VALUE;
            SQL += " FROM " + CDBConst.CONST_TABLE_RDS_ELEM_DATA;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            byte[] Data = T.Rows[0][3].PostProcessValue<byte[]>();
            var R = Data.ToDataStream().DeserializeBinaryStream<object>();
            this._values.Add(LanguageCode, R);

            return -1;
        }
        public int ValueUpdate(int LanguageCode, IDatabaseProvider Provider)
        {
            var Exists = this.ValueExists(LanguageCode, Provider);
            if (!Exists)
                this.ValueInsert(LanguageCode, Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._attributeKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, this._elementVersion);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE, LanguageCode);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_VALUE, this._values[LanguageCode].SerializeBinaryStream().ToDataByteArray());

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_RDS_ELEM_DATA;
            SQL += " SET " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_VALUE + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_VALUE;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public int ValueDelete(int LanguageCode, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._attributeKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, this._elementVersion);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE, LanguageCode);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ELEM_DATA;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public int ValueInsert(int LanguageCode, IDatabaseProvider Provider)
        {
            var Exists = this.ValueExists(LanguageCode, Provider);
            if (Exists)
                this.ValueUpdate(LanguageCode, Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._attributeKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, this._elementVersion);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE, LanguageCode);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_VALUE, this._values[LanguageCode].SerializeBinaryStream().ToDataByteArray());

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_RDS_ELEM_DATA;
            SQL += "(" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_VALUE + ")";
            SQL += " VALUES ";
            SQL += "(@p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + ", ";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + ", ";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE + ", ";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_VALUE + ")";

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? -1 : -2);
        }

        public int ValueGetAll(IDatabaseProvider Provider)
        {
            this._values.Clear();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._attributeKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, this._elementVersion);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_VALUE;
            SQL += " FROM " + CDBConst.CONST_TABLE_RDS_ELEM_DATA;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                byte[] Data = T.Rows[i][3].PostProcessValue<byte[]>();
                var R = Data.ToDataStream().DeserializeBinaryStream<object>();
                this._values.Add(T.Rows[i][2].PostProcessValue<int>(), R);
            }

            return -1;
        }
        public int ValueUpdateAll(IDatabaseProvider Provider)
        {
            int[] Keys = new int[this._values.Keys.Count];
            this._values.Keys.CopyTo(Keys, 0);

            foreach (int LanguageCode in Keys)
                this.ValueUpdate(LanguageCode, Provider);

            return -1;
        }
        public int ValueDeleteAll(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._attributeKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, this._elementVersion);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ELEM_DATA;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public int ValueInsertAll(IDatabaseProvider Provider)
        {
            int[] Keys = new int[this._values.Keys.Count];
            this._values.Keys.CopyTo(Keys, 0);

            foreach (int LanguageCode in Keys)
                this.ValueInsert(LanguageCode, Provider);

            return -1;
        }
        public bool ValueExists(int LanguageCode, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._attributeKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, this._elementVersion);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE, LanguageCode);

            var SQL = "SELECT COUNT(*) FROM " + CDBConst.CONST_TABLE_RDS_ELEM_DATA;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return false;

            return (T.Rows[0][0].PostProcessValue<int>() > 0);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static int sAttributeValuesDeleteByAttribute(decimal AttributeKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, AttributeKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ELEM_DATA;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " =@p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public static int sAttributeValuesDeleteByElement(decimal ElementVersion, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, ElementVersion);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ELEM_DATA;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " =@p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public static List<CRdsAttributeValue> sGetAttributeValuesByElement(decimal ElementVersion, IDatabaseProvider Provider)
        {
            List<CRdsAttributeValue> R = new List<CRdsAttributeValue>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, ElementVersion);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_VALUE;
            SQL += " FROM " + CDBConst.CONST_TABLE_RDS_ELEM_DATA;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            decimal PreviousKey = -1;
            decimal CurrentKey = -1;
            CRdsAttributeValue Value = null;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                CurrentKey = T.Rows[i][1].PostProcessValue<decimal>(-1);
                if (PreviousKey != CurrentKey)
                {
                    if (Value != null)
                        R.Add(Value);

                    Value = new CRdsAttributeValue();
                    Value.ElementVersion = T.Rows[i][0].PostProcessValue<decimal>(-1);
                    Value.AttributeKey = T.Rows[i][1].PostProcessValue<decimal>(-1);
                    Value.Provider = Provider;
                }

                byte[] Data = T.Rows[i][3].PostProcessValue<byte[]>(new byte[0]);
                var Stream = Data.ToDataStream();
                var Obj = Stream.DeserializeBinaryStream<object>();

                Value.Values.Add(T.Rows[i][2].PostProcessValue<int>(-1), Obj);
                PreviousKey = CurrentKey;
            }

            return R;
        }
        public static CRdsAttributeValue sGetAttributeValue(decimal ElementVersion, decimal AttributeKey, IDatabaseProvider Provider)
        {
            CRdsAttributeValue R = new CRdsAttributeValue();
            R.ElementVersion = ElementVersion;
            R.AttributeKey = AttributeKey;
            var RR = R.ValueGetAll(Provider);
            return (RR == -1 ? R : null);
        }

        public static CRdsAttributeValue sFindAttributeValueByKey(decimal AttributeKey, List<CRdsAttributeValue> Values)
        {
            foreach (CRdsAttributeValue Value in Values)
                if (Value.AttributeKey == AttributeKey)
                    return Value;

            return null;
        }
        #endregion
    }
}
