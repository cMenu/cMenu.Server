using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;

namespace cMenu.Metaobjects
{
    [Serializable]
    public class CMetaobjectLink
    {
        #region PROTECTED FIELDS
        protected decimal _sourceObjectKey = -1;
        protected decimal _linkedObjectKey = -1;
        protected decimal _linkValue = -1;
        protected EnMetaobjectLinkType _linkType = EnMetaobjectLinkType.ESimple;
        #endregion        

        #region PUBLIC FIELDS
        public decimal SourceObjectKey
        {
            get { return _sourceObjectKey; }
            set { _sourceObjectKey = value; }
        }
        public decimal LinkedObjectKey
        {
            get { return _linkedObjectKey; }
            set { _linkedObjectKey = value; }
        }
        public decimal LinkValue
        {
            get { return _linkValue; }
            set { _linkValue = value; }
        }
        public EnMetaobjectLinkType LinkType
        {
            get { return _linkType; }
            set { _linkType = value; }
        }
        #endregion        

        #region PUBLIC FUNCTIONS
        public int LinkGet(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._sourceObjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY, this._linkedObjectKey);

            string SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_VALUE;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_TYPE;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS_LINKS + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            this._sourceObjectKey = (decimal)T.Rows[0][0];
            this._linkedObjectKey = (decimal)T.Rows[0][1];
            this._linkValue = (decimal)T.Rows[0][2];
            this._linkType = (EnMetaobjectLinkType)T.Rows[0][3];

            return -1;
        }
        public int LinkDelete(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._sourceObjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY, this._linkedObjectKey);

            string SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS_LINKS + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY;

            var R = Provider.QueryExecute(SQL, false, Params);

            return (R ? -1 : -2);
        }
        public int LinkUpdate(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._sourceObjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY, this._linkedObjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_LINK_OBJ_VALUE, this._linkValue);
            Params.Add(CDBConst.CONST_TABLE_FIELD_LINK_OBJ_TYPE, this._linkType);

            string SQL = "UPDATE " + CDBConst.CONST_TABLE_OBJECTS_LINKS + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_VALUE + " = @p" + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_VALUE + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_TYPE + " = @p" + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_TYPE;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " AND ";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY;

            var R = Provider.QueryExecute(SQL, false, Params);

            return (R ? -1 : -2);
        }
        public int LinkInsert(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._sourceObjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY, this._linkedObjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_LINK_OBJ_VALUE, this._linkValue);
            Params.Add(CDBConst.CONST_TABLE_FIELD_LINK_OBJ_TYPE, this._linkType);

            string SQL = "INSERT INTO " + CDBConst.CONST_TABLE_OBJECTS_LINKS + "(";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_VALUE + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_TYPE + ") ";
            SQL += " VALUES (@p";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", @p";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY + ", @p";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_VALUE + ", @p";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_TYPE + ") ";

            var R = Provider.QueryExecute(SQL, false, Params);
            return (R ? -1 : -2);
        }
        public CMetaobject GetSourceObject(IDatabaseProvider Provider)
        {
            CMetaobject R = new CMetaobject(Provider);
            R.Key = this.SourceObjectKey;
            R.ObjectGetByKey(Provider);
            return R;
        }
        public CMetaobject GetLinkedObject(IDatabaseProvider Provider)
        {
            CMetaobject R = new CMetaobject(Provider);
            R.Key = this.LinkedObjectKey;
            R.ObjectGetByKey(Provider);
            return R;
        }
        #endregion        

        #region STATIC FUNCTIONS
        public static List<CMetaobjectLink> sGetExternalLinks(decimal ObjectKey, IDatabaseProvider Provider)
        {
            List<CMetaobjectLink> R = new List<CMetaobjectLink>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY, ObjectKey);

            string SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_VALUE;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_TYPE;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS_LINKS + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Link = new CMetaobjectLink()
                {
                    SourceObjectKey = T.Rows[i][0].CheckDBNULLValue<decimal>(),
                    LinkedObjectKey = T.Rows[i][1].CheckDBNULLValue<decimal>(),
                    LinkValue = T.Rows[i][2].CheckDBNULLValue<decimal>(),
                    LinkType = T.Rows[i][3].CheckDBNULLValue<EnMetaobjectLinkType>()
                };
                R.Add(Link);
            }

            return R;
        }
        public static List<CMetaobjectLink> sGetInternalLinks(decimal ObjectKey, IDatabaseProvider Provider)
        {
            List<CMetaobjectLink> R = new List<CMetaobjectLink>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, ObjectKey);

            string SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_VALUE;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_TYPE;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS_LINKS + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Link = new CMetaobjectLink()
                {
                    SourceObjectKey = T.Rows[i][0].CheckDBNULLValue<decimal>(),
                    LinkedObjectKey = T.Rows[i][1].CheckDBNULLValue<decimal>(),
                    LinkValue = T.Rows[i][2].CheckDBNULLValue<decimal>(),
                    LinkType = T.Rows[i][3].CheckDBNULLValue<EnMetaobjectLinkType>()
                };
                R.Add(Link);
            }

            return R;
        }        
        public static List<CMetaobjectLink> sGetExternalLinksByClass(decimal ObjectKey, EnMetaobjectClass Class, IDatabaseProvider Provider)
        {
            List<CMetaobjectLink> R = new List<CMetaobjectLink>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY, ObjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_CLASS, Class);

            string SQL = "SELECT A." + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += ", A." + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY;
            SQL += ", A." + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_VALUE;
            SQL += ", A." + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_TYPE;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS_LINKS + " A JOIN " + CDBConst.CONST_TABLE_OBJECTS + " B ON A." + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY + " = B." + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " WHERE ";
            SQL += "A." + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY;
            SQL += " AND B." + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Link = new CMetaobjectLink()
                {
                    SourceObjectKey = T.Rows[i][0].CheckDBNULLValue<decimal>(),
                    LinkedObjectKey = T.Rows[i][1].CheckDBNULLValue<decimal>(),
                    LinkValue = T.Rows[i][2].CheckDBNULLValue<decimal>(),
                    LinkType = T.Rows[i][3].CheckDBNULLValue<EnMetaobjectLinkType>()
                };
                R.Add(Link);
            }

            return R;
        }
        public static List<CMetaobjectLink> sGetInternalLinksByClass(decimal ObjectKey, EnMetaobjectClass Class, IDatabaseProvider Provider)
        {
            List<CMetaobjectLink> R = new List<CMetaobjectLink>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, ObjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_CLASS, Class);

            string SQL = "SELECT A." + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += ", A." + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY;
            SQL += ", A." + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_VALUE;
            SQL += ", A." + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_TYPE;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS_LINKS + " A JOIN " + CDBConst.CONST_TABLE_OBJECTS + " B ON A." + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = B." + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " WHERE ";
            SQL += "A." + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            SQL += " AND B." + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Link = new CMetaobjectLink()
                {
                    SourceObjectKey = T.Rows[i][0].CheckDBNULLValue<decimal>(),
                    LinkedObjectKey = T.Rows[i][1].CheckDBNULLValue<decimal>(),
                    LinkValue = T.Rows[i][2].CheckDBNULLValue<decimal>(),
                    LinkType = T.Rows[i][3].CheckDBNULLValue<EnMetaobjectLinkType>()
                };
                R.Add(Link);
            }

            return R;
        }
        public static int sDeleteExternalLinks(decimal ObjectKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, ObjectKey);

            string SQL = "DELETE FROM " + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_LINK_OBJ_KEY;

            var R = Provider.QueryExecute(SQL, false, Params);

            return (R ? -1 : -2);
        }
        public static int sDeleteInternalLinks(decimal ObjectKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, ObjectKey);

            string SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS_LINKS + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;

            var R = Provider.QueryExecute(SQL, false, Params);

            return (R ? -1 : -2);
        }
        #endregion        
    }
}
