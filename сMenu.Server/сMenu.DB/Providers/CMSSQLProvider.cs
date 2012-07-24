using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace cMenu.DB.Providers
{
    [Serializable]
    public class CMSSQLProvider : IDatabaseProvider
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
            SqlConnection Conn = new SqlConnection(this._config);
            SqlCommand Comm = new SqlCommand(SQL, Conn);
            return Comm;
        }
        public DataTable QueryGetData(string SQL, bool withTransaction = false, Hashtable Parameters = null)
        {
            SqlCommand Comm = (SqlCommand)this.CommandGenerate(SQL);
            DataTable R = null;
            SqlTransaction Transaction = null;

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
                SqlDataReader Reader = Comm.ExecuteReader();
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
            SqlCommand Comm = (SqlCommand)this.CommandGenerate(SQL);
            bool R = false;
            SqlTransaction Transaction = null;

            try
            {
                if (Parameters != null)
                {
                    string[] Keys = new string[Parameters.Keys.Count];
                    Parameters.Keys.CopyTo(Keys, 0);
                    foreach (string K in Keys)
                        Comm.Parameters.Add(new SqlParameter("@p" + K, (Parameters[K] == null ? DBNull.Value : Parameters[K])));
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
