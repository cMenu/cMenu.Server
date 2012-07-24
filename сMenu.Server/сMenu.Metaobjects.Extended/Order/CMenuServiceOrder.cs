using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.DB;

namespace cMenu.Metaobjects.Extended.Order
{
    [Serializable]
    public class CMenuServiceOrder
    {
        #region PROTECTED FIELDS
        protected decimal _key = -1;
        protected Guid _id = Guid.Empty;
        protected decimal _sessionKey = -1;
        protected decimal _userKey = -1;
        protected DateTime _date = DateTime.Now;
        protected List<CMenuServiceOrderAmount> _amounts = new List<CMenuServiceOrderAmount>();
        #endregion

        #region PUBLIC FIELDS
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
        public decimal SessionKey
        {
            get { return _sessionKey; }
            set { _sessionKey = value; }
        }
        public decimal UserKey
        {
            get { return _userKey; }
            set { _userKey = value; }
        }
        public DateTime Date
        {
            get {return _date;}
            set {_date = value;}
        }
        public List<CMenuServiceOrderAmount> Amounts
        {
            get { return _amounts; }
            set { _amounts = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMenuServiceOrder()
        { }
        #endregion

        #region PUBLIC FUNCTIONS
        public int OrderGetByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_KEY, this._key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY;
            SQL += " FROM " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            this._key = T.Rows[0][0].CheckDBNULLValue<decimal>();
            this._id = Guid.Parse(T.Rows[0][1].CheckDBNULLValue<string>());
            this._sessionKey = T.Rows[0][2].CheckDBNULLValue<decimal>();
            this._date = T.Rows[0][3].CheckDBNULLValue<DateTime>();
            this._userKey = T.Rows[0][4].CheckDBNULLValue<decimal>(-1);
            this._amounts = CMenuServiceOrderAmount.sGetAmountsByOrder(this._key, Provider);

            return -1;
        }
        public int OrderGetByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_ID, this._id.ToString().ToUpper());

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY;
            SQL += " FROM " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -2;

            this._key = T.Rows[0][0].CheckDBNULLValue<decimal>();
            this._id = Guid.Parse(T.Rows[0][1].CheckDBNULLValue<string>());
            this._sessionKey = T.Rows[0][2].CheckDBNULLValue<decimal>();
            this._date = T.Rows[0][3].CheckDBNULLValue<DateTime>();
            this._userKey = T.Rows[0][4].CheckDBNULLValue<decimal>(-1);
            this._amounts = CMenuServiceOrderAmount.sGetAmountsByOrder(this._key, Provider);

            return -1;
        }
        public int OrderDeleteByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_KEY, this._key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);
            if (T) CMenuServiceOrderAmount.sDeleteAmountsByOrder(this._key, Provider);
            return (T ? -1 : -2);
        }
        public int OrderDeleteByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_ID, this._id.ToString().ToUpper());

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_ID;

            var T = Provider.QueryExecute(SQL, false, Params);
            if (T) CMenuServiceOrderAmount.sDeleteAmountsByOrder(this._key, Provider);
            return (T ? -1 : -2);
        }
        public int OrderUpdateByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_DT, this._date);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY, this._userKey);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " SET " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_DT;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? -1 : -2);
         }
        public int OrderUpdateByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_DT, this._date);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY, this._userKey);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " SET " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_DT;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_ID;

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? -1 : -2);
        }
        public int OrderInsert(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY, this._sessionKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY, this._userKey);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_ORDERS;
            SQL += "(" + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY + ")";
            SQL += " VALUES ";
            SQL += "(@p" + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + ", @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + ", @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY + ")";

            var T = Provider.QueryExecute(SQL, false, Params);
            if (T) 
                foreach (CMenuServiceOrderAmount Amount in this._amounts)
                {
                    Amount.OrderKey = this._key;
                    Amount.AmountInsert(Provider);
                }

            return (T ? -1 : -2);
        }
        #endregion

        #region STATIS FUNCTIONS
        public static List<CMenuServiceOrder> sGetAllOrders(IDatabaseProvider Provider)
        {
            List<CMenuServiceOrder> R = new List<CMenuServiceOrder>();

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY;
            SQL += " FROM " + CDBConst.CONST_TABLE_ORDERS;

            var T = Provider.QueryGetData(SQL, false, null);
            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Order = new CMenuServiceOrder();
                Order.Key = T.Rows[i][0].CheckDBNULLValue<decimal>();
                Order.ID = Guid.Parse(T.Rows[i][1].CheckDBNULLValue<string>());
                Order.SessionKey = T.Rows[i][2].CheckDBNULLValue<decimal>();
                Order.Date = T.Rows[i][3].CheckDBNULLValue<DateTime>();
                Order.UserKey= T.Rows[i][4].CheckDBNULLValue<decimal>(-1);
                Order.Amounts = CMenuServiceOrderAmount.sGetAmountsByOrder(Order.Key, Provider);
                R.Add(Order);
            }
            
            return R;
        }
        public static List<CMenuServiceOrder> sGetOrdersByDates(DateTime StartDate, DateTime EndDate, IDatabaseProvider Provider)
        {
            List<CMenuServiceOrder> R = new List<CMenuServiceOrder>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_DT + "_START", StartDate);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_DT + "_END", EndDate);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_USER_KEY;
            SQL += " FROM " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + " >= " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + "_START";
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + " <= " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + "_END";

            var T = Provider.QueryGetData(SQL, false, null);
            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Order = new CMenuServiceOrder();
                Order.Key = T.Rows[i][0].CheckDBNULLValue<decimal>();
                Order.ID = Guid.Parse(T.Rows[i][1].CheckDBNULLValue<string>());
                Order.SessionKey = T.Rows[i][2].CheckDBNULLValue<decimal>();
                Order.Date = T.Rows[i][3].CheckDBNULLValue<DateTime>();
                Order.UserKey = T.Rows[i][4].CheckDBNULLValue<decimal>(-1);
                Order.Amounts = CMenuServiceOrderAmount.sGetAmountsByOrder(Order.Key, Provider);
                R.Add(Order);
            }

            return R;
        }
        #endregion
    }
}
