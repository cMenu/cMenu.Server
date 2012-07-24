﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;

namespace cMenu.DB
{
    public static class CDatabaseHelperExt
    {
        public static T CheckDBNULLValue<T>(this object Value, object DefaultValue = null)
        {
            return CDatabaseHelper.sCheckDBNULLValue<T>(Value, DefaultValue);
        }
    }

    public class CDatabaseHelper
    {
        public static T sCheckDBNULLValue<T>(object Value, object DefaultValue = null)
        {
            if (Value == DBNull.Value)
                return (T)DefaultValue;
            else
            {
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
                    R = string.Format("Data Source={0};", ServerAddress);
                    break;
            }

            return R;
        }
    }
   
}
