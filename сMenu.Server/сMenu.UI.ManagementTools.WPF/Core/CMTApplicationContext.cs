using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using cMenu.DB;
using cMenu.DB.Providers;
using cMenu.Common;
using cMenu.Common.Base;
using cMenu.Metaobjects;
using cMenu.Security;
using cMenu.Security.UsersManagement;

using cMenu.UI.ManagementTools.WPF.Models.Metaobjects;

namespace cMenu.UI.ManagementTools.WPF.Core
{
    class CMTApplicationContext
    {
        #region PROTECTED FIELDS
        protected static object _clipboard;
        protected static CMTApplicationConfiguration _applicationConfiguration;
        protected static CMTApplicationConnection _currentConnection;
        #endregion

        #region PUBLIC FIELDS
        public static object Clipboard
        {
            get { return _clipboard; }
            set { _clipboard = value; }
        }
        public static CMTApplicationConfiguration ApplicationConfiguration
        {
            get { return _applicationConfiguration; }
            set { _applicationConfiguration = value; }
        }
        public static IDatabaseProvider CurrentProvider
        {
            get 
            {
                return (_currentConnection == null ? null : _currentConnection.Provider);
            }
            set 
            { 
                if (_currentConnection != null)
                    _currentConnection.Provider = value; 
            }
        }
        public static CMTApplicationConnection CurrentConnection
        {
            get { return _currentConnection; }
            set { _currentConnection = value; }
        }
        public static CSystemUser CurrentUser
        {
            get 
            { 
                return (_currentConnection == null ? null : _currentConnection.User); 
            }
            set 
            {
                if (_currentConnection != null)
                    _currentConnection.User = value;
            }
        }
        public static int CurrentLanguage
        {
            get { return _currentConnection.Configuration.LanguageCode; }
        }
        #endregion

        #region STATIC FUNCTIONS
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
