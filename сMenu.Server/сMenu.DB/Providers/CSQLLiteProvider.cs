using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace cMenu.DB.Providers
{
    [Serializable]
    public class CSQLLiteProvider : IDatabaseProvider
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
            SQLiteConnection Conn = new SQLiteConnection(this._config);
            SQLiteCommand Comm = new SQLiteCommand(SQL, Conn);
            return Comm;
        }
        public DataTable QueryGetData(string SQL, bool withTransaction = false, Hashtable Parameters = null)
        {
            SQLiteCommand Comm = (SQLiteCommand)this.CommandGenerate(SQL);
            DataTable R = null;
            SQLiteTransaction Transaction = null;

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
                SQLiteDataReader Reader = Comm.ExecuteReader();
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
            SQLiteCommand Comm = (SQLiteCommand)this.CommandGenerate(SQL);
            bool R = false;
            SQLiteTransaction Transaction = null;

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
                var RR = (long)T.Rows[0][0];
                R = (RR == 1);
            }
            return R;
        }
    }
}
