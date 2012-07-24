using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.DB.Providers;
using cMenu.Common;
using cMenu.Common.Base;
using cMenu.Security;
using cMenu.Security.UsersManagement;
using cMenu.UI.ManagementTools.Forms.Common;

namespace cMenu.UI.ManagementTools.Core
{
    public class CMTApplicationContext
    {
        #region PROTECTED FIELDS
        protected static CMTApplicationConfiguration _configuration;
        protected static List<frmMtServer> _connectionForms = new List<frmMtServer>();
        protected static Hashtable _activeConnections = new Hashtable();        
        #endregion

        #region PUBLIC FIELDS
        public static CMTApplicationConfiguration Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }
        public static List<frmMtServer> ConnectionForms
        {
            get { return _connectionForms; }
            set { _connectionForms = value; }
        }
        public static Hashtable ActiveConnections
        {
            get { return _activeConnections; }
            set { _activeConnections = value; }
        }        
        #endregion

        #region STATIC FUNCTIONS
        public static int sRemoveConnection(Guid ID)
        {
            _activeConnections.Remove(ID);
            return -1;
        }
        public static CMTApplicationConnection sGetConnection(Guid ID)
        {
            if (_activeConnections.ContainsKey(ID))
                return (CMTApplicationConnection)_activeConnections[ID];
            return null;
        }
        public static int sSetActiveConnection(Guid ID, CMTApplicationConnection Connection)
        {
            if (_activeConnections.ContainsKey(ID))
                _activeConnections[ID] = Connection;
            else
                _activeConnections.Add(ID, Connection);
            return -1;
        }
        public static IDatabaseProvider sGetActiveProvider(Guid ID)
        {
            var Connection = sGetConnection(ID);
            if (Connection == null)
                return null;
            if (Connection.Provider != null)
                return Connection.Provider;

            switch (Connection.Configuration.DatabaseType)
            {
                case EnServerDB.EMsSQL: Connection.Provider = new CMSSQLProvider() { Configuration = Connection.Configuration.ConnectionString }; break;
                case EnServerDB.EMySQL: Connection.Provider = new CMySQLProvider() { Configuration = Connection.Configuration.ConnectionString }; break;
                case EnServerDB.EOralce: Connection.Provider = new COracleProvider() { Configuration = Connection.Configuration.ConnectionString }; break;
                case EnServerDB.ESQLLite: Connection.Provider = new CSQLLiteProvider() { Configuration = Connection.Configuration.ConnectionString }; break;
            }

            return Connection.Provider;
        }
        public static IDatabaseProvider sGetProvider(CMTApplicationConnection Connection)
        {
            switch (Connection.Configuration.DatabaseType)
            {
                case EnServerDB.EMsSQL: Connection.Provider = new CMSSQLProvider() { Configuration = Connection.Configuration.ConnectionString }; break;
                case EnServerDB.EMySQL: Connection.Provider = new CMySQLProvider() { Configuration = Connection.Configuration.ConnectionString }; break;
                case EnServerDB.EOralce: Connection.Provider = new COracleProvider() { Configuration = Connection.Configuration.ConnectionString }; break;
                case EnServerDB.ESQLLite: Connection.Provider = new CSQLLiteProvider() { Configuration = Connection.Configuration.ConnectionString }; break;
            }

            return Connection.Provider;
        }
        #endregion
    }
}
