using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.IO;

namespace cMenu.Metadata.Attributes
{
    [Serializable]
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class CMetaPropertyBindingAttribute : System.Attribute
    {
        #region PROTECTED FIELDS
        protected bool _visibleForMetadataManager = true;
        protected bool _allowBinding = true;
        protected string _classID;
        protected string _fieldName = "";
        protected int _dictionaryKey = -1;
        protected int _dictionaryAttributeKey = -1;
        #endregion

        #region PUBLIC FIELDS
        public bool VisibleForMetadataManager
        {
            get { return _visibleForMetadataManager; }
            set { _visibleForMetadataManager = value; }
        }
        public bool AllowBinding
        {
            get { return _allowBinding; }
            set { _allowBinding = value; }
        }
        public string ClassID
        {
            get { return _classID; }
            set { _classID = value; }
        }
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }
        public int DictionaryKey
        {
            get { return _dictionaryKey; }
            set { _dictionaryKey = value; }
        }
        public int DictionaryAttributeKey
        {
            get { return _dictionaryAttributeKey; }
            set { _dictionaryAttributeKey = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaPropertyBindingAttribute()
        {
        }
        public CMetaPropertyBindingAttribute(string ClassID, string FieldName, IDatabaseProvider Provider)
        {
            this._classID = ClassID;
            this._fieldName = FieldName;
            this.AttributeBindingGet(Provider);
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int AttributeBindingUpdate(IDatabaseProvider Provider)
        {
            var Stream = this.SerializeXMLStream(typeof(CMetaPropertyBindingAttribute));
            var XML = Stream.ToDataString();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_ID, this._classID);
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_FIELD, this._fieldName);
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_FIELD, XML);

            string SQL = "UPDATE " + CDBConst.CONST_TABLE_META + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_BINDING_XML + " = @p" + CDBConst.CONST_TABLE_FIELD_META_BINDING_XML;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_META_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_META_ID + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_FIELD + " = @p" + CDBConst.CONST_TABLE_FIELD_META_FIELD;

            var R = Provider.QueryExecute(SQL, false, Params);
            return (R ? -1 : -2);
        }
        public int AttributeBindingDelete(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_ID, this._classID);
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_FIELD, this._fieldName);

            string SQL = "DELETE FROM  " + CDBConst.CONST_TABLE_META;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_META_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_META_ID + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_FIELD + " = @p" + CDBConst.CONST_TABLE_FIELD_META_FIELD;

            var R = Provider.QueryExecute(SQL, false, Params);
            return (R ? -1 : -2);
        }
        public int AttributeBindingInsert(IDatabaseProvider Provider)
        {
            if (this.IsExists(Provider))
                return -2;

            var Stream = this.SerializeXMLStream(typeof(CMetaPropertyBindingAttribute));
            var XML = Stream.ToDataString();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_ID, this._classID);
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_FIELD, this._fieldName);
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_FIELD, XML);

            string SQL = "INSERT INTO " + CDBConst.CONST_TABLE_META + "(";
            SQL += CDBConst.CONST_TABLE_FIELD_META_ID + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_FIELD + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_BINDING_XML + ")";
            SQL += " VALUES (";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_META_ID + ", ";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_META_FIELD + ", ";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_META_BINDING_XML + ")";

            var R = Provider.QueryExecute(SQL, false, Params);
            return (R ? -1 : -2);
        }
        public int AttributeBindingGet(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_ID, this._classID);
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_FIELD, this._fieldName);

            string SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_META_ID + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_FIELD + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_BINDING_XML;
            SQL += " FROM " + CDBConst.CONST_TABLE_META + " WHERE " + CDBConst.CONST_TABLE_FIELD_META_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_META_ID + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_FIELD + " = @p" + CDBConst.CONST_TABLE_FIELD_META_FIELD;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            var XML = (string)T.Rows[0][0];
            var Stream = XML.ToDataStream();
            var Binding = Stream.DeserializeXMLStream<CMetaPropertyBindingAttribute>(typeof(CMetaPropertyBindingAttribute));
            this._allowBinding = Binding.AllowBinding;
            this._classID = Binding.ClassID;
            this._dictionaryAttributeKey = Binding.DictionaryAttributeKey;
            this._dictionaryKey = Binding.DictionaryKey;
            this._fieldName = Binding.FieldName;
            this._visibleForMetadataManager = Binding.VisibleForMetadataManager;

            return -1;
        }
        public bool IsExists(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_ID, this._classID);
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_FIELD, this._fieldName);

            string SQL = "SELECT Count(*) FROM " + CDBConst.CONST_TABLE_META + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_META_ID + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_FIELD + " = @p" + CDBConst.CONST_TABLE_FIELD_META_FIELD;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return true;

            return (T.Rows[0][0].CheckDBNULLValue<int>() > 0);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CMetaPropertyBindingAttribute> sGetMetaAttributesByClassID(Guid ClassID, IDatabaseProvider Provider)
        {
            List<CMetaPropertyBindingAttribute> R = new List<CMetaPropertyBindingAttribute>();
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_META_ID, ClassID.ToString());

            string SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_META_ID + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_FIELD + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_BINDING_XML;
            SQL += " FROM " + CDBConst.CONST_TABLE_META + " WHERE " + CDBConst.CONST_TABLE_FIELD_META_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_META_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                string XML = (string)T.Rows[i][2];
                var Stream = XML.ToDataStream();
                var Binding = Stream.DeserializeXMLStream<CMetaPropertyBindingAttribute>(typeof(CMetaPropertyBindingAttribute));
                R.Add(Binding);
            }

            return R;
        }
        public static List<CMetaPropertyBindingAttribute> sGetMetaAttributes(IDatabaseProvider Provider)
        {
            List<CMetaPropertyBindingAttribute> R = new List<CMetaPropertyBindingAttribute>();

            string SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_META_ID + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_FIELD + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_META_BINDING_XML;
            SQL += " FROM " + CDBConst.CONST_TABLE_META + " ORDER BY " + CDBConst.CONST_TABLE_FIELD_META_ID;

            var T = Provider.QueryGetData(SQL);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                string XML = (string)T.Rows[i][2];
                var Stream = XML.ToDataStream();
                var Binding = Stream.DeserializeXMLStream<CMetaPropertyBindingAttribute>(typeof(CMetaPropertyBindingAttribute));
                R.Add(Binding);
            }

            return R;
        }
        #endregion

    }
}
