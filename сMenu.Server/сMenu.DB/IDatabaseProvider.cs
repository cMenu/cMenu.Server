using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;


namespace cMenu.DB
{
    public interface IDatabaseProvider
    {
        string ConnectionString
        { get; set; }

        DbCommand CommandGenerate(string SQL);
        DataTable QueryGetData(string SQL, bool Transactional = false, Hashtable Parameters = null);
        bool QueryExecute(string SQL, bool Transactional = false, Hashtable Parameters = null);
        bool TestConnection();
    }
}
