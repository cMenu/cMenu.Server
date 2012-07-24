using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.UI.ManagementTools.WPF.Core;
using cMenu.UI.ManagementTools.WPF.Core.MVVM;

namespace cMenu.UI.ManagementTools.WPF.Models.Core
{
    public class ModelApplicationConnectionConfigurationModel : CMVVMCachedNotificationObject
    {
        #region PROTECTED FIELDS
        protected CMTApplicationConnectionConfiguration _configurationSource;
        #endregion

        #region PUBLIC FIELDS
        public CMTApplicationConnectionConfiguration ConfigurationSource
        {
            get { return _configurationSource; }
            set 
            { 
                _configurationSource = value;
                _initializeCache();
                RaisePropertyChanged("ConfigurationSource");
            }
        }
        public EnServerDB DatabaseType
        {
            get { return (EnServerDB)_cache["DatabaseType"]; }
            set 
            {
                _cache["DatabaseType"] = value;
                RaisePropertyChanged("DatabaseType");
            }
        }
        public string DatabaseUser
        {
            get { return (string)_cache["DatabaseUser"]; }
            set 
            {
                _cache["DatabaseUser"] = value;
                RaisePropertyChanged("DatabaseUser");
            }
        }
        public string DatabasePassword
        {
            get { return (string)_cache["DatabasePassword"]; }
            set 
            {
                _cache["DatabasePassword"] = value;
                RaisePropertyChanged("DatabasePassword");
            }
        }
        public string SystemUser
        {
            get { return (string)_cache["SystemUser"]; }
            set 
            {
                _cache["SystemUser"] = value;
                RaisePropertyChanged("SystemUser");
            }
        }
        public string SystemPassword
        {
            get { return (string)_cache["SystemPassword"]; }
            set 
            {
                _cache["SystemPassword"] = value;
                RaisePropertyChanged("SystemPassword");
            }
        }
        public string ServerAddress
        {
            get { return (string)_cache["ServerAddress"]; }
            set
            {
                _cache["ServerAddress"] = value;
                RaisePropertyChanged("ServerAddress");
            }
        }
        public string DatabaseName
        {
            get { return (string)_cache["DatabaseName"]; }
            set 
            {
                _cache["DatabaseName"] = value;
                RaisePropertyChanged("DatabaseName");
            }
        }
        public string Name
        {
            get { return (string)_cache["Name"]; }
            set
            {
                _cache["Name"] = value;
                RaisePropertyChanged("Name");
            }
        }
        public Guid ID
        {
            get { return (Guid)_cache["ID"]; }
            set
            {
                _cache["ID"] = value;
                RaisePropertyChanged("ID");
            }
        }
        public int LanguageCode
        {
            get { return (int)_cache["LanguageCode"]; }
            set
            {
                _cache["LanguageCode"] = value;
                RaisePropertyChanged("LanguageCode");
            }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected int _initializeCache()
        {
            this._cache.Values.Clear();

            this.DatabaseType = this._configurationSource.DatabaseType;
            this.DatabaseUser = this._configurationSource.DatabaseUser;
            this.DatabasePassword = this._configurationSource.DatabasePassword;
            this.SystemUser = this._configurationSource.SystemUser;
            this.SystemPassword = this._configurationSource.SystemPassword;
            this.ServerAddress = this._configurationSource.ServerAddress;
            this.DatabaseName = this._configurationSource.DatabaseName;
            this.Name = this._configurationSource.Name;
            this.ID = this._configurationSource.ID;
            this.LanguageCode = this._configurationSource.LanguageCode;

            return -1;
        }
        #endregion
    }
}
