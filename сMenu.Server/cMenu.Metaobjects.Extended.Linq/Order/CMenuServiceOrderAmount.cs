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
    public static class CMenuServiceOrderAmountEx
    {
        public static List<CMenuServiceOrderAmount> GetAmountsByOrder(this Table<CMenuServiceOrderAmount> Amounts, decimal Key)
        {
            return CMenuServiceOrderAmount.sGetAmountsByOrder(Key, Amounts.Context);
        }
        public static List<CMenuServiceOrderAmount> GetAmountsByAmount(this Table<CMenuServiceOrderAmount> Amounts, decimal Key)
        {
            return CMenuServiceOrderAmount.sGetAmountsByAmount(Key, Amounts.Context);
        }

        public static int DeleteAmountsByOrder(this Table<CMenuServiceOrderAmount> Amounts, decimal Key)
        {
            return CMenuServiceOrderAmount.sDeleteAmountsByOrder(Key, Amounts.Context);
        }
        public static int DeleteAmountsByAmount(this Table<CMenuServiceOrderAmount> Amounts, decimal Key)
        {
            return CMenuServiceOrderAmount.sDeleteAmountsByAmount(Key, Amounts.Context);
        }
    }

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
        public int AmountInsert(DataContext Context)
        {
            var Amounts = Context.GetTable<CMenuServiceOrderAmount>();
            Amounts.InsertOnSubmit(this);

            return CErrors.ERR_SUC;
        }
        #endregion

        #region STATIS FUNCTIONS
        public static List<CMenuServiceOrderAmount> sGetAmountsByOrder(decimal Key, DataContext Context)
        {
            var Amounts = Context.GetTable<CMenuServiceOrderAmount>();
            var Query = from Amount in Amounts
                        where Amount.OrderKey == Key
                        select Amount;

            return Query.ToList();
        }
        public static List<CMenuServiceOrderAmount> sGetAmountsByAmount(decimal Key, DataContext Context)
        {
            var Amounts = Context.GetTable<CMenuServiceOrderAmount>();
            var Query = from Amount in Amounts
                        where Amount.AmountKey == Key
                        select Amount;

            return Query.ToList();
        }

        public static int sDeleteAmountsByOrder(decimal Key, DataContext Context)
        {
            var Amounts = Context.GetTable<CMenuServiceOrderAmount>();
            var Query = from Amount in Amounts
                        where Amount.OrderKey == Key
                        select Amount;

            Amounts.DeleteAllOnSubmit(Query);
            return CErrors.ERR_SUC;
        }
        public static int sDeleteAmountsByAmount(decimal Key, DataContext Context)
        {
            var Amounts = Context.GetTable<CMenuServiceOrderAmount>();
            var Query = from Amount in Amounts
                        where Amount.AmountKey == Key
                        select Amount;

            Amounts.DeleteAllOnSubmit(Query);
            return CErrors.ERR_SUC;
        }
        #endregion
    }
}
