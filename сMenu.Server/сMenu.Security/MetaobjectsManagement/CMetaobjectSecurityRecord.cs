using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.DB;

namespace cMenu.Security.MetaobjectsManagement
{
    [Serializable]
    public class CMetaobjectSecurityRecord
    {
        #region PROTECTED FIELDS
        protected decimal _userKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected decimal _metaobjectKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected decimal _rights = 3;
        #endregion

        #region PUBLIC FIELDS
        public decimal UserKey
        {
            get { return _userKey; }
            set { _userKey = value; }
        }
        public decimal MetaobjectKey
        {
            get { return _metaobjectKey; }
            set { _metaobjectKey = value; }
        }
        public decimal Rights
        {
            get { return _rights; }
            set { _rights = value; }
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int RecordGet(IDatabaseProvider Provider, decimal UserKey, decimal MetaobjectKey)
        {
            CMetaobjectSecurityRecord R = new CMetaobjectSecurityRecord();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY, UserKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY, MetaobjectKey);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_RIGHTS + " FROM " + CDBConst.CONST_TABLE_METAOBJECT_SECURITY;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY + "= @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY + "= @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            this.MetaobjectKey = MetaobjectKey;
            this.UserKey = UserKey;
            this.Rights = T.Rows[0][0].PostProcessValue<int>(-3);

            return CErrors.ERR_SUC;

        }
        public int RecordDelete(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY, this.UserKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY, this.MetaobjectKey);

            var SQL = "DELETE FROM "+ CDBConst.CONST_TABLE_METAOBJECT_SECURITY;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY + "= @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY + "= @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);

        }
        public int RecordUpdate(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY, this.UserKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY, this.MetaobjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_RIGHTS, this.Rights);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_METAOBJECT_SECURITY + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_METAOBJSEC_RIGHTS + " = @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_RIGHTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY + "= @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY + "= @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_UPDATE_OBJECT);

        }
        public int RecordInsert(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY, this.UserKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY, this.MetaobjectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_RIGHTS, this.Rights);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_METAOBJECT_SECURITY;
            SQL += "(";
            SQL += CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY + ", " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_RIGHTS;
            SQL += ") VALUES (";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_RIGHTS;
            SQL += ")";

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_INSERT_OBJECT);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CMetaobjectSecurityRecord> sGetRecordsByMetaobject(decimal MetaobjectKey, IDatabaseProvider Provider)
        {
            List<CMetaobjectSecurityRecord> R = new List<CMetaobjectSecurityRecord>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY, MetaobjectKey);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY + ", " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_RIGHTS;
            SQL += " FROM " + CDBConst.CONST_TABLE_METAOBJECT_SECURITY + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T != null)
                for (int i = 0; i < T.Rows.Count; i++)
                {
                    var Record = new CMetaobjectSecurityRecord();
                    Record.MetaobjectKey = T.Rows[i][0].PostProcessValue<decimal>();
                    Record.UserKey = T.Rows[i][1].PostProcessValue<decimal>();
                    Record.Rights = T.Rows[i][2].PostProcessValue<int>();
                    R.Add(Record);
                }
           
            return R;
        }
        public static List<CMetaobjectSecurityRecord> sGetRecordsByUser(decimal UserKey, IDatabaseProvider Provider)
        {
            List<CMetaobjectSecurityRecord> R = new List<CMetaobjectSecurityRecord>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY, UserKey);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY + ", " + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_RIGHTS;
            SQL += " FROM " + CDBConst.CONST_TABLE_METAOBJECT_SECURITY + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T != null)
                for (int i = 0; i < T.Rows.Count; i++)
                {
                    var Record = new CMetaobjectSecurityRecord();
                    Record.MetaobjectKey = T.Rows[i][0].PostProcessValue<decimal>();
                    Record.UserKey = T.Rows[i][1].PostProcessValue<decimal>();
                    Record.Rights = T.Rows[i][2].PostProcessValue<int>();
                    R.Add(Record);
                }

            return R;
        }

        public static int sDeleteRecordsByMetaobject(decimal MetaobjectKey, IDatabaseProvider Provider)
        {

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY, MetaobjectKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_METAOBJECT_SECURITY + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_METAOBJECT_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);
        }
        public static int sDeleteRecordsByUser(decimal UserKey, IDatabaseProvider Provider)
        {

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY, UserKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_METAOBJECT_SECURITY + " WHERE ";
            SQL += CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_METAOBJSEC_USER_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);
        }
        #endregion
    }
}
