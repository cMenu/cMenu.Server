using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.Common.Base;
using cMenu.DB;
using cMenu.DB.Providers;

namespace cMenu.UI.ManagementTools.Core
{
    [Serializable]
    public class CMTApplicationConnectionConfiguration : CBaseEntity
    {
        #region PROTECTED FIELDS
        protected EnServerDB _databaseType = EnServerDB.EMsSQL;
        protected string _dbUser = "";
        protected string _dbPassword = "";
        protected string _systemUser = "";
        protected string _systemPassword = "";
        protected string _serverAddress = "";
        protected string _databaseName = "";
        #endregion

        #region PUBLIC FIELDS
        public EnServerDB DatabaseType
        {
            get { return _databaseType; }
            set { _databaseType = value; }
        }
        public string DatabaseUser
        {
            get { return _dbUser; }
            set { _dbUser = value; }
        }
        public string DatabasePassword
        {
            get { return _dbPassword; }
            set { _dbPassword = value; }
        }
        public string SystemUser
        {
            get { return _systemUser; }
            set { _systemUser = value; }
        }
        public string SystemPassword
        {
            get { return _systemPassword; }
            set { _systemPassword = value; }
        }
        public string ServerAddress
        {
            get { return _serverAddress; }
            set { _serverAddress = value; }
        }
        public string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }
        public string ConnectionString
        {
            get
            {
                return CDatabaseHelper.sCompileConnectionString(this._databaseType, this._serverAddress, this._databaseName, this._dbUser, this._dbPassword);
            }
        }
        #endregion

        #region PROTECTED FUNCTIONS

        #endregion

        #region PUBLIC FUNCTIONS
        #endregion
    }
}
