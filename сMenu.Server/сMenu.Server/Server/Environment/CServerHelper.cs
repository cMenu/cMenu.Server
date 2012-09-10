using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

using iMenu.Common;
using iMenu.Communication.Server.Configuration;
using iMenu.DB;
using iMenu.DB.Providers;

namespace iMenu.Communication.Server.Environment
{
    public class CFunctionExecutionEnvironment
    {
        #region PROTECTED FIELDS
        protected static IDatabaseProvider _provider;
        protected static CCommunicationServerConfiguration _commSrvrConfiguration = new CCommunicationServerConfiguration();
        protected static CApplicationServerConfiguration _appSrvrConfiguration = new CApplicationServerConfiguration();
        #endregion        

        #region PUBLIC FIELDS
        public static CCommunicationServerConfiguration CommunicationServerConfiguration
        {
            get
            {
                return _commSrvrConfiguration;
            }
            set
            {
                _commSrvrConfiguration = value;
            }
        }
        public static CApplicationServerConfiguration ApplicationServerConfiguration
        {
            get
            {
                return _appSrvrConfiguration;
            }
            set
            {
                _appSrvrConfiguration = value;
            }
        }
        #endregion        

        #region STATIC FUNCTIONS
        public static IServerFunction sGetFunction(string ModuleFolderPath, string ModuleName, string InterfaceName)
        {
            IServerFunction R;

            if (!File.Exists(ModuleFolderPath + ModuleName))
                return null;

            Assembly AppAssembly = Assembly.LoadFile(ModuleFolderPath + ModuleName);
            foreach (Type Type in AppAssembly.GetTypes())
                foreach (Type Interface in Type.GetInterfaces())
                    if (Interface.Name == InterfaceName)
                    {
                        R = (IServerFunction)AppAssembly.CreateInstance(Type.Name);
                        return R;
                    }

            return null;
        }
        public static IDatabaseProvider sGetCurrentProvider()
        {
            if (_provider != null)
                return _provider;

            switch (_commSrvrConfiguration.DatabaseType)
            {
                case EnServerDB.EMsSQL: _provider = new CMSSQLProvider() { Configuration = _commSrvrConfiguration.ConnectionString }; break;
                case EnServerDB.EMySQL: _provider = new CMySQLProvider() { Configuration = _commSrvrConfiguration.ConnectionString }; break;
                case EnServerDB.EOralce: _provider = new COracleProvider() { Configuration = _commSrvrConfiguration.ConnectionString }; break;
                case EnServerDB.ESQLLite: _provider = new CSQLLiteProvider() { Configuration = _commSrvrConfiguration.ConnectionString }; break;
            }

            return _provider;
        }
        #endregion        
    }
}
