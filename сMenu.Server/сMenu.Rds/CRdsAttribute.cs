using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Common;
using cMenu.Common.Base;

namespace cMenu.Rds
{
    [Serializable]
    public class CRdsAttribute : CBaseEntity
    {
        #region PROTECTED FIELDS
        protected List<CRdsAttributeLink> _links = new List<CRdsAttributeLink>();

        protected decimal _key;
        protected decimal _dictionaryKey;
        protected bool _isHidden;
        protected DbType _type;
        #endregion

        #region PUBLIC FIELDS
        public List<CRdsAttributeLink> Links
        {
            get { return _links; }
            set { _links = value; }
        }

        public decimal Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public decimal DictionaryKey
        {
            get { return _dictionaryKey; }
            set { _dictionaryKey = value; }
        }
        public bool IsHidden
        {
            get { return _isHidden; }
            set { _isHidden = value; }
        }
        public DbType Type
        {
            get { return _type; }
            set { _type = value; }
        }        
        #endregion

        #region CONSTRUCTORS
        public CRdsAttribute() 
            : base()
        { 

        }
        public CRdsAttribute(int Key, IDatabaseProvider Provider)
            : base()
        {
            this._key = Key;
            this.AttributeGetByKey(Provider);

        }
        public CRdsAttribute(Guid ID, IDatabaseProvider Provider)
            : base()
        {
            this._id = ID;
            this.AttributeGetByID(Provider);

        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CRdsAttributeLink> GetLinks(IDatabaseProvider Provider)
        {
            this._links = CRdsAttributeLink.sGetLinksByAttribute(this._key, Provider);
            return this._links;
        }

        public int AttributeDeleteByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ATTRIBUTES + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY;
            var R = Provider.QueryExecute(SQL, false, Params);
            if (!R)
                return -3;

            var RR = CRdsAttributeLink.sDeleteLinksByAttribute(this._key, Provider);
            if (RR != -1)
                return RR;

            RR = CRdsAttributeValue.sValuesDeleteByAttribute(this._key, Provider);
            if (RR != -1)
                return RR;

            RR = CRdsAttributeLink.sDeleteLinksByLinkedAttribute(this._key, Provider);

            return RR;
        }
        public int AttributeDeleteByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID, this._id.ToString().ToUpper());

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ATTRIBUTES + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID;
            var R = Provider.QueryExecute(SQL, false, Params);

            if (!R)
                return -3;

            var RR = CRdsAttributeLink.sDeleteLinksByAttribute(this._key, Provider);
            if (RR != -1)
                return RR;

            RR = CRdsAttributeValue.sValuesDeleteByAttribute(this._key, Provider);
            if (RR != -1)
                return RR;

            RR = CRdsAttributeLink.sDeleteLinksByLinkedAttribute(this._key, Provider);

            return RR;
        }
        public int AttributeUpdateByKey(IDatabaseProvider Provider)
        {
            var Exists = this.AttributeExists(Provider);
            if (!Exists)
                return this.AttributeInsert(Provider);               

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY, this._dictionaryKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN, (this._isHidden ? 1 : 0));
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME, this._name);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE, this._type);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._key);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_RDS_ATTRIBUTES + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE + " ";
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY;

            var R = Provider.QueryExecute(SQL, false, Params);
            return (R ? -1 : -2);
        }
        public int AttributeUpdateByID(IDatabaseProvider Provider)
        {
            var Exists = this.AttributeExists(Provider);
            if (!Exists)
                return this.AttributeInsert(Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY, this._dictionaryKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN, (this._isHidden ? 1 : 0));
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME, this._name);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE, this._type);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID, this._id.ToString().ToUpper());

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_RDS_ATTRIBUTES + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + ", ";
           
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE + " ";
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID;

            var R = Provider.QueryExecute(SQL, false, Params);
            return (R ? -1 : -2);
        }
        public int AttributeInsert(IDatabaseProvider Provider)
        {
            this._key = CDatabaseSequence.sGetRdsKey(Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY, this._dictionaryKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID, this._id.ToString());
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN, (this._isHidden ? 1 : 0));
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME, this._name);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE, this._type);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._key);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_RDS_ATTRIBUTES + " (";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE + ")";
            SQL += " VALUES (";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID + ", @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN + ", ";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + ", ";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME + ", @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE + ")";

            var R = Provider.QueryExecute(SQL, false, Params);
            return (R ? -1 : -2);
        }
        public int AttributeGetByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T != null)
            {
                this._dictionaryKey = T.Rows[0][0].CheckDBNULLValue<decimal>();
                this._id = Guid.Parse(T.Rows[0][1].CheckDBNULLValue<string>());
                this._isHidden = (T.Rows[0][2].CheckDBNULLValue<int>() == 1);
                this._key = T.Rows[0][3].CheckDBNULLValue<int>();
                this._name = T.Rows[0][4].CheckDBNULLValue<string>();
                this._type = T.Rows[0][5].CheckDBNULLValue<DbType>();
            }

            return -1;
        }
        public int AttributeGetByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID, this._id.ToString().ToUpper());

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T != null)
            {
                this._dictionaryKey = T.Rows[0][0].CheckDBNULLValue<decimal>();
                this._id = Guid.Parse(T.Rows[0][1].CheckDBNULLValue<string>());
                this._isHidden = (T.Rows[0][2].CheckDBNULLValue<int>() == 1);
                this._key = T.Rows[0][3].CheckDBNULLValue<int>();
                this._name = T.Rows[0][4].CheckDBNULLValue<string>();
                this._type = T.Rows[0][5].CheckDBNULLValue<DbType>();
            }

            return -1;
        }

        public bool AttributeExists(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, this._key);

            var SQL = "SELECT COUNT(*) FROM " + CDBConst.CONST_TABLE_RDS_ATTRIBUTES + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return false;

            return (T.Rows.Count.CheckDBNULLValue<int>() > 0);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CRdsAttribute> sAttributesGetByDictionary(decimal DictKey, IDatabaseProvider Provider)
        {
            List<CRdsAttribute> R = new List<CRdsAttribute>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY, DictKey);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME + ", " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE;
            SQL += " FROM " + CDBConst.CONST_TABLE_RDS_ATTRIBUTES;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            CRdsAttribute Attr = null;
            if (T != null)
            {
                for (int i = 0; i < T.Rows.Count; i++)
                {
                    Attr = new CRdsAttribute();
                    Attr.DictionaryKey = T.Rows[i][0].CheckDBNULLValue<decimal>();
                    Attr.ID = Guid.Parse(T.Rows[i][1].CheckDBNULLValue<string>());
                    Attr.IsHidden = ((T.Rows[i][2]).CheckDBNULLValue<int>() == 1);
                    Attr.Key = T.Rows[i][3].CheckDBNULLValue<decimal>();
                    Attr.Name = T.Rows[i][4].CheckDBNULLValue<string>();
                    Attr.Type = T.Rows[i][5].CheckDBNULLValue<DbType>();
                    R.Add(Attr);
                }                    
            }

            return R;
        }
        public static int sAttributesDeleteByDictionary(decimal DictionaryKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY, DictionaryKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_LINKS + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY + " IN (";
            SQL += "SELECT DISTINCT " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " FROM " + CDBConst.CONST_TABLE_RDS_ATTRIBUTES + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY;
            SQL += ")";
            var R = Provider.QueryExecute(SQL, false, Params);
            if (!R)
                return -3;

            SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ELEM_DATA + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " IN (";
            SQL += "SELECT " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY + " FROM " + CDBConst.CONST_TABLE_RDS_ATTRIBUTES + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY;
            SQL += ")";
            R = Provider.QueryExecute(SQL, false, Params);
            if (!R)
                return -4;
     
            SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_ATTRIBUTES + " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY;
            R = Provider.QueryExecute(SQL, false, Params);
            return (R ? -1 : -2);
        }

        public static CRdsAttribute sAttributeFindById(List<CRdsAttribute> Attributes, Guid ID)
        {
            foreach (CRdsAttribute Attr in Attributes)
                if (Attr.ID == ID)
                    return Attr;
            return null;
        }
        public static CRdsAttribute sAttributeFindByKey(List<CRdsAttribute> Attributes, decimal Key)
        {
            foreach (CRdsAttribute Attr in Attributes)
                if (Attr.Key == Key)
                    return Attr;
            return null;
        }
        public static CRdsAttribute sAttributeFindByName(List<CRdsAttribute> Attributes, string Name)
        {
            foreach (CRdsAttribute Attr in Attributes)
                if (Attr.Name == Name)
                    return Attr;
            return null;
        }
        #endregion
    }
}
