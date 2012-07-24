using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Common;

namespace cMenu.Security.DevicesManagement
{
    [Serializable]
    public class CClientDeviceSession
    {
        #region PROTECTED FIELDS
        protected EnClientDeviceSessionStatus _status = EnClientDeviceSessionStatus.EEnabled;
        protected decimal _key = -1;
        protected Guid _id = Guid.Empty;
        protected decimal _deviceKey = -1;
        protected decimal _userKey = -1;
        protected string _authToken = "";
        protected DateTime _deadLine = DateTime.MaxValue;
        #endregion        

        #region PUBLIC FIELDS
        public EnClientDeviceSessionStatus Status
        {
            get { return _status; }
            set { _status = value; }
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
        public decimal DeviceKey
        {
            get { return _deviceKey; }
            set { _deviceKey = value; }
        }
        public decimal UserKey
        {
            get { return _userKey; }
            set { _userKey = value; }
        }
        public string AuthToken
        {
            get { return _authToken; }
            set { _authToken = value; }
        }
        public DateTime DeadLine
        {
            get { return _deadLine; }
            set { _deadLine = value; }
        }
        #endregion        

        #region PUBLIC FUNCTIONS
        public int SessionGet(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_USER, this._userKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE, this._deviceKey);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS;
            SQL += " FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_USER;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            this._deadLine = (DateTime)T.Rows[0][2];
            this._authToken = (string)T.Rows[0][3];
            this._key = T.Rows[0][4].CheckDBNULLValue<decimal>();
            this._id = Guid.Parse(T.Rows[0][5].CheckDBNULLValue<string>());
            this._status = T.Rows[0][6].CheckDBNULLValue<EnClientDeviceSessionStatus>();

            return -1;
         }
        public int SessionUpdate(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE, this._deviceKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_USER, this._userKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE, this._deadLine);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN, this._authToken);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_STATUS, this._status);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_SESSION + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_STATUS + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_USER;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public int SessionDelete(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_USER, this._userKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE, this._deviceKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_USER;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public int SessionInsert(IDatabaseProvider Provider)
        {
            this._key = CDatabaseSequence.sGetObjectKey(Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE, this._deviceKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_USER, this._userKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE, this._deadLine);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN, this._authToken);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_STATUS, this._status);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_SESSION;
            SQL += "(";
            SQL += CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_USER;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS;
            SQL += ") VALUES (";
            SQL += "@p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + ", @p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + ", @p" + CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN + ", @p" + CDBConst.CONST_TABLE_FIELD_SESSION_USER;
            SQL += ", @p" + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_SESSION_ID + ", @p" + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS;
            SQL += ")";


            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CClientDeviceSession> sGetSessionsByUser(decimal UserKey, IDatabaseProvider Provider)
        {
            List<CClientDeviceSession> R = new List<CClientDeviceSession>();
            
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_USER, UserKey);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS; 
            SQL += " FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_USER;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Session = new CClientDeviceSession();
                Session.UserKey = T.Rows[i][0].CheckDBNULLValue<decimal>();
                Session.DeviceKey = T.Rows[i][1].CheckDBNULLValue<decimal>();
                Session.DeadLine = T.Rows[i][2].CheckDBNULLValue<DateTime>();
                Session.AuthToken = T.Rows[i][3].CheckDBNULLValue<string>();
                Session.Key = T.Rows[i][4].CheckDBNULLValue<decimal>();
                Session.ID = Guid.Parse(T.Rows[i][5].CheckDBNULLValue<string>());
                Session.Status = T.Rows[i][6].CheckDBNULLValue<EnClientDeviceSessionStatus>();
                R.Add(Session);
            }
            
            return R;

        }
        public static List<CClientDeviceSession> sGetSessionsByDevice(decimal DeviceKey, IDatabaseProvider Provider)
        {
            List<CClientDeviceSession> R = new List<CClientDeviceSession>();
            
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE, DeviceKey);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS; 
            SQL += " FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Session = new CClientDeviceSession();
                Session.UserKey = T.Rows[i][0].CheckDBNULLValue<decimal>();
                Session.DeviceKey = T.Rows[i][1].CheckDBNULLValue<decimal>();
                Session.DeadLine = T.Rows[i][2].CheckDBNULLValue<DateTime>();
                Session.AuthToken = T.Rows[i][3].CheckDBNULLValue<string>();
                Session.Key = T.Rows[i][4].CheckDBNULLValue<decimal>();
                Session.ID = Guid.Parse(T.Rows[i][5].CheckDBNULLValue<string>());
                Session.Status = T.Rows[i][6].CheckDBNULLValue<EnClientDeviceSessionStatus>();
                R.Add(Session);
            }
            
            return R;

        }
        public static CClientDeviceSession sGetSessionBytokenAndDevice(decimal DeviceKey, string Token, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE, DeviceKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN, Token);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_DEADLINE + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_ID + ", " + CDBConst.CONST_TABLE_FIELD_SESSION_STATUS; 
            SQL += " FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_TOKEN;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return null;

            var Session = new CClientDeviceSession();
            Session.UserKey = T.Rows[0][0].CheckDBNULLValue<decimal>();
            Session.DeviceKey = T.Rows[0][1].CheckDBNULLValue<decimal>();
            Session.DeadLine = T.Rows[0][2].CheckDBNULLValue<DateTime>();
            Session.AuthToken = T.Rows[0][3].CheckDBNULLValue<string>();
            Session.Key = T.Rows[0][4].CheckDBNULLValue<decimal>();
            Session.ID = Guid.Parse(T.Rows[0][5].CheckDBNULLValue<string>());
            Session.Status = T.Rows[0][6].CheckDBNULLValue<EnClientDeviceSessionStatus>();
            return Session;
        }
        public static int sDeleteSessionsByUser(decimal UserKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_USER, UserKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_USER + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_USER;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }
        public static int sDeleteSessionsByDevice(decimal DeviceKey, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE, DeviceKey);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_SESSION;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE + " = @p" + CDBConst.CONST_TABLE_FIELD_SESSION_DEVICE;

            var T = Provider.QueryExecute(SQL, false, Params);

            return (T ? -1 : -2);
        }        
        #endregion
    }
}
