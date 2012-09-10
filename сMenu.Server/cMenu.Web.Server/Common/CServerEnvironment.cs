using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.DB;
using cMenu.DB.Providers;
using cMenu.Metaobjects.Extended.Linq;

namespace cMenu.Web.Server.Tablet.Common
{
    public class CServerEnvironment
    {
        #region PROTECTED FIELDS
        protected static DataContext _dataContext;
        protected static IDatabaseProvider _databaseProvider;
        #endregion

        #region PUBLIC FIELDS
        public static DataContext DataContext
        {
            get { return _getDataContext(); }
            set { _dataContext = value; }
        }
        public static IDatabaseProvider DatabaseProvider
        {
            get { return _getDatabaseProvider(); }
            set { _databaseProvider = value; }
        }
        #endregion

        #region CONSTRUCTORS
        #endregion

        #region PROTECTED FUNCTIONS
        protected static DataContext _getDataContext()
        {
            if (_dataContext == null)
                _initializeServerEnvironment();

            return _dataContext;
        }
        protected static IDatabaseProvider _getDatabaseProvider()
        {
            if (_databaseProvider == null)
                _initializeServerEnvironment();

            return _databaseProvider;
        }

        protected static int _initializeServerEnvironment()
        {
            var R = _initializeDataContext();
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_SRVR_INIT_ENV;

            R = _initializeDatabaseProvider();
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_SRVR_INIT_ENV;

            return CErrors.ERR_SUC;
        }
        protected static int _initializeDataContext()
        {
            var ConnectionString = "";
            try
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringInDaWork"].ConnectionString;
            }
            catch (Exception)
            {
                /// TEST & DEBUG
                ConnectionString = "Data Source=.\\;Initial Catalog=DB_CMENU;User Id=sa;Password=Qwerty1;";
            }

            CMetaobjectExtented C = new CMetaobjectExtented();

            var ResorcesAssembly = Assembly.LoadWithPartialName("cMenu.Resources");
            var Stream = ResorcesAssembly.GetManifestResourceStream("cMenu.Resources.LinqMapping.xml");

            TextReader Reader = new StreamReader(Stream);
            var XML = Reader.ReadToEnd();

            
            XmlMappingSource mapping = XmlMappingSource.FromXml(XML);
            _dataContext = new DataContext(ConnectionString, mapping);
            _dataContext.CommandTimeout = 0;

            Reader.Close();
            Stream.Close();

            return CErrors.ERR_SUC;
        }
        protected static int _initializeDatabaseProvider()
        {
            var ConnectionString = "";
            try
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringInDaWork"].ConnectionString;
            }
            catch (Exception)
            {
                /// TEST & DEBUG
                ConnectionString = "Data Source=.\\;Initial Catalog=DB_CMENU;User Id=sa;Password=Qwerty1;";
            }

            _databaseProvider = new CMSSQLProvider() { ConnectionString = ConnectionString };

            return CErrors.ERR_SUC;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public static int Initialize()
        {
            return _initializeServerEnvironment();
        }
        #endregion

    }
}