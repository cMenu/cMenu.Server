using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

using cMenu.Metadata;
using cMenu.Metadata.Attributes;
using cMenu.Common;
using cMenu.Communication.Server.Configuration;
using cMenu.DB;
using cMenu.DB.Providers;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Extended;
using cMenu.Security;
using cMenu.Security.UsersManagement;

namespace cMenu.Communication.Server.Environment
{
    public class CFunctionExecutionEnvironment
    {
        #region PROTECTED FIELDS
        protected static IDatabaseProvider _provider;
        protected static CApplicationServerConfiguration _appSrvrConfiguration = new CApplicationServerConfiguration();
        protected static CSystemUser _currentUser;
        #endregion        

        #region PUBLIC FIELDS
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
        public static CSystemUser CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        #endregion        

        #region STATIC FUNCTIONS
        public static IServerFunction sGetExternalFunction(string ModuleFolderPath, string ModuleName, string InterfaceName)
        {
            IServerFunction R;

            if (!File.Exists(ModuleFolderPath + ModuleName))
                return null;

            Assembly AppAssembly = Assembly.LoadFile(ModuleFolderPath + ModuleName);
            foreach (Type Type in AppAssembly.GetTypes())
                foreach (Type Interface in Type.GetInterfaces())
                    if (Interface.Name == InterfaceName)
                    {
                        R = (IServerFunction)AppAssembly.CreateInstance(Type.FullName);
                        return R;
                    }

            return null;
        }
        public static IServerFunction sGetInternalFunction(string FunctionID, string InterfaceName)
        {
            var FunctionTypes = CMetadataHelper.sGetTypesFromAssembly(System.Reflection.Assembly.GetExecutingAssembly(), typeof(IServerFunction));
            foreach (Type T in FunctionTypes)
            {
                var Meta = CMetadataHelper.sGetFunctionMetadata(T);
                if (Meta == null)
                    continue;
                if (Meta.FunctionID == FunctionID)
                {
                    return (IServerFunction)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(T.FullName);
                }
            }
            return null;
        }
        public static IDatabaseProvider sGetCurrentProvider()
        {
            if (_provider != null)
                return _provider;

            switch (_appSrvrConfiguration.DatabaseType)
            {
                case EnServerDB.EMsSQL: _provider = new CMSSQLProvider() { ConnectionString = _appSrvrConfiguration.ConnectionString }; break;
                case EnServerDB.EMySQL: _provider = new CMySQLProvider() { ConnectionString = _appSrvrConfiguration.ConnectionString }; break;
                case EnServerDB.EOralce: _provider = new COracleProvider() { ConnectionString = _appSrvrConfiguration.ConnectionString }; break;
                case EnServerDB.ESQLLite: _provider = new CSQLLiteProvider() { ConnectionString = _appSrvrConfiguration.ConnectionString }; break;
            }

            return _provider;
        }        
        #endregion
    }
}