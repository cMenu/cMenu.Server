using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;

namespace cMenu.DB
{
    public static class CDatabaseHelperExt
    {
        #region STATIC FUNCTIONS
        public static T PostProcessDatabaseValue<T>(this object Value, object DefaultValue = null)
        {
            return CDatabaseHelper.sPostProcessDatabaseValue<T>(Value, DefaultValue);
        }
        public static T PostProcessObjectValue<T>(this object Value, object DefaultValue = null)
        {
            return CDatabaseHelper.sPostProcessObjectValue<T>(Value, DefaultValue);
        }
        #endregion
    }

    public class CDatabaseHelper
    {
        #region STATIC FUNCTIONS
        public static T sPostProcessDatabaseValue<T>(object Value, object DefaultValue = null)
        {
            if (Value == DBNull.Value)
                return (T)DefaultValue;
            else
            {
                /// SQLite (converting long to int)
                if (Value is long)
                {
                    object ValueInteger = Convert.ToInt32(Value);
                    return (T)ValueInteger;
                }

                return (T)Value;
            }
        }
        public static string sCompileConnectionString(EnServerDB DatabaseType, string ServerAddress, string DatabaseName, string UserName, string Password)
        {
            var R = "";
            switch (DatabaseType)
            {
                case EnServerDB.EMsSQL:
                    R = string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", ServerAddress, DatabaseName, UserName, Password);
                    break;
                case EnServerDB.ESQLLite:
                    R = string.Format("Data Source={0};", DatabaseName);
                    break;
            }

            return R;
        }
        public static T sPostProcessObjectValue<T>(object Value, object DefaultValue = null)
        {
            if (Value is long)
            {
                object ValueInteger = Convert.ToInt32(Value);
                return (T)ValueInteger;
            }

            return (T)Value;
        }
        #endregion
    }
   
}
