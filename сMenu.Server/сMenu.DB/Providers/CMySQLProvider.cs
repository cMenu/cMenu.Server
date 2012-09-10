using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MySql.Data;

namespace cMenu.DB.Providers
{
    [Serializable]
    public class CMySQLProvider : IDatabaseProvider
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
            MySql.Data.MySqlClient.MySqlConnection Connection = new MySql.Data.MySqlClient.MySqlConnection(this._connectionString);
            return new MySql.Data.MySqlClient.MySqlCommand(SQL, Connection);
        }
        public DataTable QueryGetData(string SQL, bool Transactional = false, Hashtable Parameters = null)
        {
            DataTable R = null;
            MySql.Data.MySqlClient.MySqlTransaction Transaction = null;
            MySql.Data.MySqlClient.MySqlCommand Command = (MySql.Data.MySqlClient.MySqlCommand)this.CommandGenerate(SQL);            

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
                MySql.Data.MySqlClient.MySqlDataReader Reader = Command.ExecuteReader();
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
            bool R = false;
            MySql.Data.MySqlClient.MySqlTransaction Transaction = null;
            MySql.Data.MySqlClient.MySqlCommand Command = (MySql.Data.MySqlClient.MySqlCommand)this.CommandGenerate(SQL);

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
            DataTable T = null;
            try
            { T = this.QueryGetData(SQL, false, null); }
            catch (Exception Ex)
            {
                R = false;
                return R;
            }

            if (T == null || T.Rows.Count == 0)
                R = false;
            else
            {
                var RR = T.Rows[0][0].PostProcessValue<int>();
                R = (RR == 1);
            }
            return R;
        }
        #endregion
    }
}
