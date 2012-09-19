using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using cMenu.Common;
using cMenu.DB;
using cMenu.DB.Providers;

namespace cMenu.DB
{
    [Serializable]
    public static class CDatabaseSequence
    {
        #region STATIC FUNCTIONS
        public static decimal sGetObjectKey(IDatabaseProvider Provider)
        {
            decimal Key = -1;
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SEQUENCE_VALUE, "1");
            string SQL = "begin INSERT INTO " + CDBConst.CONST_TABLE_SEQUENCE_OBJECTS + " (" + CDBConst.CONST_TABLE_FIELD_SEQUENCE_VALUE + ") VALUES (@p" + CDBConst.CONST_TABLE_FIELD_SEQUENCE_VALUE + "); SELECT MAX(" + CDBConst.CONST_TABLE_FIELD_SEQUENCE_ID + ") FROM " + CDBConst.CONST_TABLE_SEQUENCE_OBJECTS + "; end;";
            DataTable T = Provider.QueryGetData(SQL, true, Params);
            Key = (T != null ? (decimal)T.Rows[0][0] : -1);

            return Key;
        }
        public static decimal sGetRdsKey(IDatabaseProvider Provider)
        {
            decimal Key = -1;
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SEQUENCE_VALUE, "1");
            string SQL = "begin INSERT INTO " + CDBConst.CONST_TABLE_SEQUENCE_RDS + " (" + CDBConst.CONST_TABLE_FIELD_SEQUENCE_VALUE + ") VALUES (@p" + CDBConst.CONST_TABLE_FIELD_SEQUENCE_VALUE + "); SELECT MAX(" + CDBConst.CONST_TABLE_FIELD_SEQUENCE_ID + ") FROM " + CDBConst.CONST_TABLE_SEQUENCE_RDS + "; end;";
            DataTable T = Provider.QueryGetData(SQL, true, Params);
            Key = (T != null ? (decimal)T.Rows[0][0] : -1);

            return Key;
        }
        public static decimal sGetVersionKey(IDatabaseProvider Provider)
        {
            decimal Key = -1;
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_SEQUENCE_VALUE, "1");
            string SQL = "begin INSERT INTO " + CDBConst.CONST_TABLE_SEQUENCE_VERSIONS + " (" + CDBConst.CONST_TABLE_FIELD_SEQUENCE_VALUE + ") VALUES (@p" + CDBConst.CONST_TABLE_FIELD_SEQUENCE_VALUE + "); SELECT MAX(" + CDBConst.CONST_TABLE_FIELD_SEQUENCE_ID + ") FROM " + CDBConst.CONST_TABLE_SEQUENCE_VERSIONS + "; end;";
            DataTable T = Provider.QueryGetData(SQL, true, Params);
            Key = (T != null ? (decimal)T.Rows[0][0] : -1);

            return Key;
        }
        #endregion
    }
}
