using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.Common.Base;
using cMenu.DB;

namespace cMenu.Rds
{
    public class CRdsAttributeLink : CBaseEntity
    {
        #region PROTECTED FIELDS
        protected decimal _key;
        protected decimal _attributeKey;
        protected decimal _linkedAttributeKey;
        #endregion

        #region PUBLIC FIELDS
        public decimal Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public decimal AttributeKey
        {
            get { return _attributeKey; }
            set { _attributeKey = value; }
        }
        public decimal LinkedAttributeKey
        {
            get { return _linkedAttributeKey; }
            set { _linkedAttributeKey = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CRdsAttributeLink()
            : base()
        {
 
        }
        public CRdsAttributeLink(decimal Key, IDatabaseProvider Provider)
            : base()
        {
            this._key = Key;
            this.LinkGetByKey(Provider);
        }
        public CRdsAttributeLink(Guid ID, IDatabaseProvider Provider)
            : base()
        {
            this._id = ID;
            this.LinkGetByID(Provider);
        }    
        #endregion

        #region PUBLIC FUNCTIONS
        public int LinkInsert(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME, this._name);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY, this._attributeKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY, this._linkedAttributeKey);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_RDS_LINKS;
            SQL += " (" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY + ")";
            SQL += " VALUES ";
            SQL += " (@p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY;
            SQL += ", @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID;
            SQL += ", @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME;
            SQL += ", @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY;
            SQL += ", @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY + ")";

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public int LinkUpdateByKey(IDatabaseProvider Provider)
        {
            bool Exists = this.LinkExists(Provider);
            if (!Exists)
                return this.LinkInsert(Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME, this._name);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY, this._attributeKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY, this._linkedAttributeKey);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_RDS_LINKS + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY + ", ";
            SQL += " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public int LinkUpdateByID(IDatabaseProvider Provider)
        {
            bool Exists = this.LinkExists(Provider);
            if (!Exists)
                return this.LinkInsert(Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME, this._name);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY, this._attributeKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY, this._linkedAttributeKey);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_RDS_LINKS + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY + ", ";
            SQL += " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public int LinkDeleteByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY, this._key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_LINKS;
            SQL += " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public int LinkDeleteByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY, this._key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_LINKS;
            SQL += " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public int LinkGetByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY, this._key);

            var SQL = "SELECT ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY;
            SQL += " FROM " + CDBConst.CONST_TABLE_RDS_LINKS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            this._key = T.Rows[0][0].PostProcessValue<decimal>();
            this._id = Guid.Parse(T.Rows[0][1].PostProcessValue<string>());
            this._name = T.Rows[0][2].PostProcessValue<string>();
            this._attributeKey = T.Rows[0][3].PostProcessValue<decimal>();
            this._linkedAttributeKey = T.Rows[0][4].PostProcessValue<decimal>();

            return -1;
        }
        public int LinkGetByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID, this._key);

            var SQL = "SELECT ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY;
            SQL += " FROM " + CDBConst.CONST_TABLE_RDS_LINKS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            this._key = T.Rows[0][0].PostProcessValue<decimal>();
            this._id = Guid.Parse(T.Rows[0][1].PostProcessValue<string>());
            this._name = T.Rows[0][2].PostProcessValue<string>();
            this._attributeKey = T.Rows[0][3].PostProcessValue<decimal>();
            this._linkedAttributeKey = T.Rows[0][4].PostProcessValue<decimal>();

            return -1;
        }

        public bool LinkExists(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY, this._attributeKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY, this._linkedAttributeKey);

            var SQL = "SELECT COUNT(*) FROM " + CDBConst.CONST_TABLE_RDS_LINKS + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY + " AND ";

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return false;

            return (T.Rows[0][0].PostProcessValue<int>() > 0);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CRdsAttributeLink> sGetLinksByAttribute(decimal AttributeKey, IDatabaseProvider Provider)
        {
            List<CRdsAttributeLink> R = new List<CRdsAttributeLink>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY, AttributeKey);

            var SQL = "SELECT ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY;
            SQL += " FROM " + CDBConst.CONST_TABLE_RDS_LINKS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                CRdsAttributeLink Link = new CRdsAttributeLink()
                {
                    Key = T.Rows[i][0].PostProcessValue<decimal>(),
                    ID = Guid.Parse(T.Rows[i][1].PostProcessValue<string>()),
                    Name = T.Rows[i][2].PostProcessValue<string>(),
                    AttributeKey = T.Rows[i][3].PostProcessValue<decimal>(),
                    LinkedAttributeKey = T.Rows[i][4].PostProcessValue<decimal>()
                };
                R.Add(Link);
            }

            return R;
        }

        public static int sDeleteLinksByAttribute(decimal AttributeKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY, AttributeKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_LINKS;
            SQL += " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public static int sDeleteLinksByLinkedAttribute(decimal LinkedAttributeKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY, LinkedAttributeKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_RDS_LINKS;
            SQL += " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        #endregion

    }
}