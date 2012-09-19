using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Globalization;

using cMenu.Globalization;

namespace cMenu.DB.Providers
{
    [Serializable]
    public class CMSSQLProvider : IDatabaseProvider
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
            SqlConnection Connection = new SqlConnection(this._connectionString);
            return new SqlCommand(SQL, Connection);
        }
        public DataTable QueryGetData(string SQL, bool Transactional = false, Hashtable Parameters = null)
        {            
            DataTable R = null;
            SqlTransaction Transaction = null;
            SqlCommand Command = (SqlCommand)this.CommandGenerate(SQL);

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
                SqlDataReader Reader = Command.ExecuteReader();
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
            { Command.Connection.Close(); }
            return R;

        }
        public bool QueryExecute(string SQL, bool Transactional = false, Hashtable Parameters = null)
        {            
            bool R = false;
            SqlTransaction Transaction = null;
            SqlCommand Command = (SqlCommand)this.CommandGenerate(SQL);

            try
            {
                if (Parameters != null)
                {
                    string[] Keys = new string[Parameters.Keys.Count];
                    Parameters.Keys.CopyTo(Keys, 0);
                    foreach (string K in Keys)
                        Command.Parameters.Add(new SqlParameter("@p" + K, (Parameters[K] == null ? DBNull.Value : Parameters[K])));
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
            {
                T = this.QueryGetData(SQL, false, null);
            }
            catch (Exception Ex)
            {
                R = false;
                return R;
            }

            if (T == null || T.Rows.Count == 0)
                R = false;
            else
            {
                var RR = T.Rows[0][0].PostProcessDatabaseValue<int>();
                R = (RR == 1);
            }
            return R;
        }
        #endregion
    }
}
