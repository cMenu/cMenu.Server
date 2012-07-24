using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;

namespace cMenu.DB.Providers
{
    [ObsoleteAttribute("OracleConnection has been deprecated. http://go.microsoft.com/fwlink/?LinkID=144260", false)]
    [Serializable]
    public class COracleProvider : IDatabaseProvider
    {
        private string _config;

        public string Configuration
        {
            get
            {
                return this._config;
            }
            set
            {
                this._config = value;
            }
        }

        public DbCommand CommandGenerate(string SQL)
        {
            OracleConnection Conn = new OracleConnection(this._config);
            OracleCommand Comm = new OracleCommand(SQL, Conn);
            return Comm;
        }
        public DataTable QueryGetData(string SQL, bool withTransaction = false, Hashtable Parameters = null)
        {
            OracleCommand Comm = (OracleCommand)this.CommandGenerate(SQL);
            DataTable R = null;
            OracleTransaction Transaction = null;

            try
            {
                if (Parameters != null)
                {
                    string[] Keys = new string[Parameters.Keys.Count];
                    Parameters.Keys.CopyTo(Keys, 0);
                    foreach (string K in Keys)
                        Comm.Parameters.AddWithValue("@p" + K, Parameters[K]);
                }

                Comm.Connection.Open();

                if (withTransaction)
                {
                    Transaction = Comm.Connection.BeginTransaction();
                    Comm.Transaction = Transaction;
                }
                OracleDataReader Reader = Comm.ExecuteReader();
                R = new DataTable();
                R.Load(Reader);
                Reader.Close();

                if (withTransaction)
                    Transaction.Commit();
            }
            catch (Exception Ex)
            {
                if (withTransaction)
                    Transaction.Rollback();
            }
            finally
            {
                Comm.Connection.Close();
            }
            return R;

        }
        public bool QueryExecute(string SQL, bool withTransaction = false, Hashtable Parameters = null)
        {
            OracleCommand Comm = (OracleCommand)this.CommandGenerate(SQL);
            bool R = false;
            OracleTransaction Transaction = null;

            try
            {
                if (Parameters != null)
                {
                    string[] Keys = new string[Parameters.Keys.Count];
                    Parameters.Keys.CopyTo(Keys, 0);
                    foreach (string K in Keys)
                        Comm.Parameters.AddWithValue("@p" + K, Parameters[K]);
                }

                Comm.Connection.Open();

                if (withTransaction)
                {
                    Transaction = Comm.Connection.BeginTransaction();
                    Comm.Transaction = Transaction;
                }
                Comm.ExecuteNonQuery();
                R = true;

                if (withTransaction)
                    Transaction.Commit();
            }
            catch (Exception Ex)
            {
                R = false;
                if (withTransaction)
                    Transaction.Rollback();
            }
            finally
            {
                Comm.Connection.Close();
            }
            return R;
        }
        public bool TestConnection()
        {
            var R = false;

            var SQL = "SELECT 1";
            var T = this.QueryGetData(SQL, false, null);
            if (T == null || T.Rows.Count == 0)
                R = false;
            else
            {
                var RR = T.Rows[0][0].CheckDBNULLValue<int>();
                R = (RR == 1);
            }
            return R;
        }
    }
}
