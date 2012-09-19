using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.IO;
using cMenu.Common;

namespace cMenu.Security.UsersManagement
{
    [Serializable]
    public class CSystemUserSession
    {
        #region PROTECTED FIELDS
        protected EnSessionStatus _status = EnSessionStatus.EEnabled;
        protected EnSessionType _type = EnSessionType.EWeb;
        protected decimal _key = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected Guid _id = Guid.Empty;
        protected decimal _userKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected DateTime _deadLine = DateTime.MaxValue;
        protected Hashtable _variables = new Hashtable();
        #endregion        

        #region PUBLIC FIELDS
        public EnSessionStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public EnSessionType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public decimal Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public decimal UserKey
        {
            get { return _userKey; }
            set { _userKey = value; }
        }
        public DateTime DeadLine
        {
            get { return _deadLine; }
            set { _deadLine = value; }
        }
        public Hashtable Variables
        {
            get { return _variables; }
            set { _variables = value; }
        }
        #endregion        

        #region PUBLIC FUNCTIONS
        public int SessionGetByKey(decimal Key, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_KEY, Key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_TYPE;
            SQL += " FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            this._userKey = T.Rows[0][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._deadLine = T.Rows[0][2].PostProcessDatabaseValue<DateTime>(DateTime.Now);
            this._key = T.Rows[0][3].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._id = Guid.Parse(T.Rows[0][4].PostProcessDatabaseValue<string>(""));
            this._status = T.Rows[0][5].PostProcessDatabaseValue<EnSessionStatus>(EnSessionStatus.EEnabled);
            this._type = T.Rows[0][7].PostProcessDatabaseValue<EnSessionType>(EnSessionType.EWeb);

            byte[] Data = T.Rows[0][6].PostProcessDatabaseValue<byte[]>(new byte[0]);
            this._variables = Data.ToDataStream().DeserializeBinaryStream<Hashtable>();

            return CErrors.ERR_SUC;
         }
        public int SessionGetByID(Guid ID, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_ID, ID.ToString().ToUpper());

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_TYPE;
            SQL += " FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            this._userKey = T.Rows[0][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._deadLine = T.Rows[0][2].PostProcessDatabaseValue<DateTime>(DateTime.Now);
            this._key = T.Rows[0][3].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._id = Guid.Parse(T.Rows[0][4].PostProcessDatabaseValue<string>(""));
            this._status = T.Rows[0][5].PostProcessDatabaseValue<EnSessionStatus>(EnSessionStatus.EEnabled);
            this._type = T.Rows[0][7].PostProcessDatabaseValue<EnSessionType>(EnSessionType.EWeb);

            byte[] Data = T.Rows[0][6].PostProcessDatabaseValue<byte[]>(new byte[0]);
            this._variables = Data.ToDataStream().DeserializeBinaryStream<Hashtable>();

            return CErrors.ERR_SUC;
         }
        public int SessionUpdateByKey(IDatabaseProvider Provider)
        {
            byte[] Data = this._variables.SerializeBinaryStream().ToDataByteArray();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_USER, this._userKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE, this._deadLine);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_STATUS, this._status);            
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES, Data);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_TYPE, this._type);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_SESSION + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_STATUS + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_TYPE + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_TYPE;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_UPDATE_OBJECT);
        }
        public int SessionUpdateByID(IDatabaseProvider Provider)
        {
            byte[] Data = this._variables.SerializeBinaryStream().ToDataByteArray();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_USER, this._userKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE, this._deadLine);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_STATUS, this._status);            
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES, Data);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_ID, this.ID.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_TYPE, this._type);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_SESSION + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_STATUS + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_TYPE + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_TYPE;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_ID;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_UPDATE_OBJECT);
        }
        public int SessionDeleteByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_KEY, this._key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_USER;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);
        }
        public int SessionDeleteByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_ID, this._id.ToString().ToUpper());

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_USER;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_ID;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);
        }
        public int SessionInsert(IDatabaseProvider Provider)
        {
            this._key = CDatabaseSequence.sGetObjectKey(Provider);
            byte[] Data = this._variables.SerializeBinaryStream().ToDataByteArray();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_USER, this._userKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE, this._deadLine);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_STATUS, this._status);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES, Data);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_SESSION;
            SQL += "(";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_USER;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS;
            SQL += ") VALUES (";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + ", @p" + CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES + ", @p" + CDBConst.CONST_TABLE_FIELD_SESSION_USER;
            SQL += ", @p" + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_SESSION_ID + ", @p" + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS;
            SQL += ")";


            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_INSERT_OBJECT);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CSystemUserSession> sGetSessionsByUser(decimal UserKey, IDatabaseProvider Provider)
        {
            List<CSystemUserSession> R = new List<CSystemUserSession>();
            
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_USER, UserKey);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_VARIABLES;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_TYPE;
            SQL += " FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_USER;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Session = new CSystemUserSession();
                Session.UserKey = T.Rows[i][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
                Session.DeadLine = T.Rows[i][1].PostProcessDatabaseValue<DateTime>(DateTime.Now);
                Session.Key = T.Rows[i][3].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
                Session.ID = Guid.Parse(T.Rows[i][4].PostProcessDatabaseValue<string>(""));
                Session.Status = T.Rows[i][5].PostProcessDatabaseValue<EnSessionStatus>(EnSessionStatus.EEnabled);
                Session.Type = T.Rows[i][7].PostProcessDatabaseValue<EnSessionType>(EnSessionType.EWeb);

                byte[] Data = T.Rows[0][6].PostProcessDatabaseValue<byte[]>(new byte[0]);
                Session.Variables = Data.ToDataStream().DeserializeBinaryStream<Hashtable>();
                R.Add(Session);
            }
            
            return R;

        }
        public static int sDeleteSessionsByUser(decimal UserKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_USER, UserKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_USER;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);
        }
        #endregion
    }
}
