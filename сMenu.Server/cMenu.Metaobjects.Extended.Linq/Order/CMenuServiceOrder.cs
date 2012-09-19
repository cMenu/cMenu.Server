using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects.Extended;
using cMenu.Metaobjects.Linq;
using cMenu.Security.Linq;

namespace cMenu.Metaobjects.Extended.Linq.Order
{
    public static class CMenuServiceOrderEx
    {
        public static List<CMenuServiceOrder> GetAllOrders(this Table<CMenuServiceOrder> Orders)
        {
            return CMenuServiceOrder.sGetAllOrders(Orders.Context);
        }
        public static List<CMenuServiceOrder> GetOrdersByDates(this Table<CMenuServiceOrder> Orders, DateTime StartDate, DateTime EndDate)
        {
            return CMenuServiceOrder.sGetOrdersByDates(StartDate, EndDate, Orders.Context);
        }
    }

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
            get { return _date; }
            set { _date = value; }
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
        public int OrderGetByKey(DataContext Context)
        {
            var Orders = Context.GetTable<CMenuServiceOrder>();
            var Query = from Order in Orders
                        where Order.Key == this._key
                        select Order;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_GET_OBJECT;
            var Result = Query.ToList()[0];

            this._key = Result.Key;
            this._id = Result.ID;
            this._sessionKey = Result.SessionKey;
            this._date = Result.Date;
            this._deadline = Result.Deadline;

            return CErrors.ERR_SUC;
        }
        public int OrderGetByID(DataContext Context)
        {
            var Orders = Context.GetTable<CMenuServiceOrder>();
            var Query = from Order in Orders
                        where Order.ID == this._id
                        select Order;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_GET_OBJECT;
            var Result = Query.ToList()[0];

            this._key = Result.Key;
            this._id = Result.ID;
            this._sessionKey = Result.SessionKey;
            this._date = Result.Date;
            this._deadline = Result.Deadline;

            return CErrors.ERR_SUC;
        }
        public int OrderDeleteByKey(DataContext Context)
        {
            var Orders = Context.GetTable<CMenuServiceOrder>();
            var Query = from Order in Orders
                        where Order.Key == this._key
                        select Order;

            Orders.DeleteAllOnSubmit(Query);

            var RR = CMenuServiceOrderAmount.sDeleteAmountsByOrder(this._key, Context);
            if (RR != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return CErrors.ERR_SUC;
        }
        public int OrderDeleteByID(DataContext Context)
        {
            var Orders = Context.GetTable<CMenuServiceOrder>();
            var Query = from Order in Orders
                        where Order.ID == this._id
                        select Order;

            Orders.DeleteAllOnSubmit(Query);

            var RR = CMenuServiceOrderAmount.sDeleteAmountsByOrder(this._key, Context);
            if (RR != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return CErrors.ERR_SUC;
        }
        public int OrderUpdate(DataContext Context)
        {
            this.Key = this._key;

            return CErrors.ERR_SUC;
        }
        public int OrderInsert(DataContext Context)
        {
            var Orders = Context.GetTable<CMenuServiceOrder>();
            Orders.InsertOnSubmit(this);

            foreach (CMenuServiceOrderAmount Amount in this._amounts)
            {
                var RR = Amount.AmountInsert(Context);
                if (RR != CErrors.ERR_SUC)
                    return CErrors.ERR_DB_INSERT_OBJECT;
            }

            return CErrors.ERR_SUC;
        }

        public List<CMenuServiceOrderAmount> GetAmounts(DataContext Context)
        {
            this._amounts = CMenuServiceOrderAmount.sGetAmountsByOrder(this.Key, Context);
            return this._amounts;
        }
        #endregion

        #region STATIS FUNCTIONS
        public static List<CMenuServiceOrder> sGetAllOrders(DataContext Context)
        {
            var Orders = Context.GetTable<CMenuServiceOrder>();
            var Query = from Order in Orders
                        select Order;

            return Query.ToList();
        }
        public static List<CMenuServiceOrder> sGetOrdersByDates(DateTime StartDate, DateTime EndDate, DataContext Context)
        {
            var Orders = Context.GetTable<CMenuServiceOrder>();
            var Query = from Order in Orders
                        where Order.Date >= StartDate && Order.Date <= EndDate
                        select Order;

            return Query.ToList();
        }
        #endregion
    }
}
