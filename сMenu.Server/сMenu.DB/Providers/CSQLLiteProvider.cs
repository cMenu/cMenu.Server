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
        #region PROTECTED FIELDS
        protected string _connectionString;
        #endregion

        #region PUBLIC FIELDS
        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
            }
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public DbCommand CommandGenerate(string SQL)
        {
            SQLiteConnection Connection = new SQLiteConnection(this._connectionString);
            return new SQLiteCommand(SQL, Connection);
        }
        public DataTable QueryGetData(string SQL, bool Transactional = false, Hashtable Parameters = null)
        {
            SQLiteCommand Command = (SQLiteCommand)this.CommandGenerate(SQL);
            DataTable R = null;
            SQLiteTransaction Transaction = null;

            try
            {
                if (Parameters != null)
                {
                    string[] Keys = new string[Parameters.Keys.Count];
                    Parameters.Keys.CopyTo(Keys, 0);
                    foreach (string K in Keys)
                        Command.Parameters.AddWithValue("@p" + K, Parameters[K]);
                }

                Command.Connection.Open();

                if (Transactional)
                {
                    Transaction = Command.Connection.BeginTransaction();
                    Command.Transaction = Transaction;
                }
                SQLiteDataReader Reader = Command.ExecuteReader();
                R = new DataTable();
                R.Load(Reader);
                Reader.Close();

                if (Transactional)
                    Transaction.Commit();
            }
            catch (Exception Ex)
            {
                if (Transactional)
                    Transaction.Rollback();
                throw (Ex);
            }
            finally
            {
                Command.Connection.Close();
            }
            return R;

        }
        public bool QueryExecute(string SQL, bool Transactional = false, Hashtable Parameters = null)
        {
            SQLiteCommand Command = (SQLiteCommand)this.CommandGenerate(SQL);
            bool R = false;
            SQLiteTransaction Transaction = null;

            try
            {
                if (Parameters != null)
                {
                    string[] Keys = new string[Parameters.Keys.Count];
                    Parameters.Keys.CopyTo(Keys, 0);
                    foreach (string K in Keys)
                        Command.Parameters.AddWithValue("@p" + K, Parameters[K]);
                }

                Command.Connection.Open();

                if (Transactional)
                {
                    Transaction = Command.Connection.BeginTransaction();
                    Command.Transaction = Transaction;
                }
                Command.ExecuteNonQuery();
                R = true;

                if (Transactional)
                    Transaction.Commit();
            }
            catch (Exception Ex)
            {
                R = false;
                if (Transactional)
                    Transaction.Rollback();
                throw (Ex);
            }
            finally
            {
                Command.Connection.Close();
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
        #endregion
    }
}
