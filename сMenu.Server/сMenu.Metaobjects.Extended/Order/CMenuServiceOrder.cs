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
        protected decimal _key = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected Guid _id = Guid.Empty;
        protected decimal _sessionKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected DateTime _date = DateTime.Now;
        protected DateTime _deadline = DateTime.Now;
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
        public DateTime Date
        {
            get {return _date;}
            set {_date = value;}
        }
        public DateTime Deadline
        {
            get { return _deadline; }
            set { _deadline = value; }
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
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE;
            SQL += " FROM " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            this._key = T.Rows[0][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._id = Guid.Parse(T.Rows[0][1].PostProcessDatabaseValue<string>(""));
            this._sessionKey = T.Rows[0][2].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._date = T.Rows[0][3].PostProcessDatabaseValue<DateTime>(DateTime.Now);
            this._deadline = T.Rows[0][4].PostProcessDatabaseValue<DateTime>(DateTime.Now);
            this._amounts = CMenuServiceOrderAmount.sGetAmountsByOrder(this._key, Provider);

            return CErrors.ERR_SUC;
        }
        public int OrderGetByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_ID, this._id.ToString().ToUpper());

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE;
            SQL += " FROM " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            this._key = T.Rows[0][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._id = Guid.Parse(T.Rows[0][1].PostProcessDatabaseValue<string>(""));
            this._sessionKey = T.Rows[0][2].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._date = T.Rows[0][3].PostProcessDatabaseValue<DateTime>(DateTime.Now);
            this._deadline = T.Rows[0][4].PostProcessDatabaseValue<DateTime>(DateTime.Now);
            this._amounts = CMenuServiceOrderAmount.sGetAmountsByOrder(this._key, Provider);

            return CErrors.ERR_SUC;
        }
        public int OrderDeleteByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_KEY, this._key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);
            if (!T)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return CMenuServiceOrderAmount.sDeleteAmountsByOrder(this._key, Provider);
        }
        public int OrderDeleteByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_ID, this._id.ToString().ToUpper());

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_ID;

            var T = Provider.QueryExecute(SQL, false, Params);
            if (!T)
                return CErrors.ERR_DB_DELETE_OBJECT;
            return CMenuServiceOrderAmount.sDeleteAmountsByOrder(this._key, Provider);
        }
        public int OrderUpdateByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_DT, this._date);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE, this._deadline);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " SET " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_DT;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_UPDATE_OBJECT);
         }
        public int OrderUpdateByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_DT, this._date);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE, this._deadline);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " SET " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_DT;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_ID;

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_UPDATE_OBJECT);
        }
        public int OrderInsert(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY, this._sessionKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_DT, this._date);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE, this._deadline);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_ORDERS;
            SQL += "(" + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE + ")";
            SQL += " VALUES ";
            SQL += "(@p" + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + ", @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_DT;
            SQL += ", @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + ", @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE + ")";

            var T = Provider.QueryExecute(SQL, false, Params);
            if (!T)
                return CErrors.ERR_DB_INSERT_OBJECT;

            foreach (CMenuServiceOrderAmount Amount in this._amounts)
            {
                Amount.OrderKey = this._key;
                var R = Amount.AmountInsert(Provider);
                if (R != CErrors.ERR_SUC)
                    return CErrors.ERR_DB_INSERT_OBJECT;
            }

            return CErrors.ERR_SUC;
        }

        public List<CMenuServiceOrderAmount> GetAmounts(IDatabaseProvider Provider)
        {
            this._amounts = CMenuServiceOrderAmount.sGetAmountsByOrder(this.Key, Provider);
            return this._amounts;
        }
        #endregion

        #region STATIS FUNCTIONS
        public static List<CMenuServiceOrder> sGetAllOrders(IDatabaseProvider Provider)
        {
            List<CMenuServiceOrder> R = new List<CMenuServiceOrder>();

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE;
            SQL += " FROM " + CDBConst.CONST_TABLE_ORDERS;

            var T = Provider.QueryGetData(SQL, false, null);
            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Order = new CMenuServiceOrder();
                Order.Key = T.Rows[i][0].PostProcessDatabaseValue<decimal>();
                Order.ID = Guid.Parse(T.Rows[i][1].PostProcessDatabaseValue<string>());
                Order.SessionKey = T.Rows[i][2].PostProcessDatabaseValue<decimal>();
                Order.Date = T.Rows[i][3].PostProcessDatabaseValue<DateTime>();
                Order.Deadline = T.Rows[i][4].PostProcessDatabaseValue<DateTime>();
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

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_ORDERS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_ID + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_SESSION_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_DEADLINE;
            SQL += " FROM " + CDBConst.CONST_TABLE_ORDERS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + " >= " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + "_START";
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + " <= " + CDBConst.CONST_TABLE_FIELD_ORDERS_DT + "_END";

            var T = Provider.QueryGetData(SQL, false, null);
            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Order = new CMenuServiceOrder();
                Order.Key = T.Rows[i][0].PostProcessDatabaseValue<decimal>();
                Order.ID = Guid.Parse(T.Rows[i][1].PostProcessDatabaseValue<string>());
                Order.SessionKey = T.Rows[i][2].PostProcessDatabaseValue<decimal>();
                Order.Date = T.Rows[i][3].PostProcessDatabaseValue<DateTime>();
                Order.Deadline = T.Rows[i][4].PostProcessDatabaseValue<DateTime>();
                R.Add(Order);
            }

            return R;
        }
        #endregion
    }
}
