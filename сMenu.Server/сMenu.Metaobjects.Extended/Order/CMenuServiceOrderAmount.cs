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
    public class CMenuServiceOrderAmount
    {
        #region PROTECTED FIELDS
        protected decimal _orderKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected decimal _amountKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected int _amount = 0;
        #endregion

        #region PUBLIC FIELDS
        public decimal OrderKey
        {
            get { return _orderKey; }
            set { _orderKey = value; }
        }
        public decimal AmountKey
        {
            get { return _amountKey; }
            set { _amountKey = value; }
        }
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMenuServiceOrderAmount()
        { 
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int AmountInsert(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY, this._orderKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY, this._amountKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT, this._amount);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_ORDERS_AMOUNTS;
            SQL += "(" + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT + ")";
            SQL += " VALUES ";
            SQL += "(@p" + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT + ")";

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_INSERT_OBJECT);
        }
        #endregion

        #region STATIS FUNCTIONS
        public static List<CMenuServiceOrderAmount> sGetAmountsByOrder(decimal Key, IDatabaseProvider Provider)
        {
            List<CMenuServiceOrderAmount> R = new List<CMenuServiceOrderAmount>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY, Key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT;
            SQL += " FROM " + CDBConst.CONST_TABLE_ORDERS_AMOUNTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Amount = new CMenuServiceOrderAmount();
                Amount.OrderKey = T.Rows[i][0].PostProcessDatabaseValue<decimal>();
                Amount.AmountKey = T.Rows[i][1].PostProcessDatabaseValue<decimal>();
                Amount.Amount = T.Rows[i][2].PostProcessDatabaseValue<int>();

                R.Add(Amount);
            }

            return R;
        }
        public static List<CMenuServiceOrderAmount> sGetAmountsByAmount(decimal Key, IDatabaseProvider Provider)
        {
            List<CMenuServiceOrderAmount> R = new List<CMenuServiceOrderAmount>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY, Key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY + ", " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT;
            SQL += " FROM " + CDBConst.CONST_TABLE_ORDERS_AMOUNTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Amount = new CMenuServiceOrderAmount();
                Amount.OrderKey = T.Rows[i][0].PostProcessDatabaseValue<decimal>();
                Amount.AmountKey = T.Rows[i][1].PostProcessDatabaseValue<decimal>();
                Amount.Amount = T.Rows[i][2].PostProcessDatabaseValue<int>();

                R.Add(Amount);
            }

            return R;
        }

        public static int sDeleteAmountsByOrder(decimal Key, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY, Key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_ORDERS_AMOUNTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_ORDER_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);
        }
        public static int sDeleteAmountsByAmount(decimal Key, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY, Key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_ORDERS_AMOUNTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_ORDERS_AMOUNTS_AMOUNT_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);
        }
        #endregion
    }
}
