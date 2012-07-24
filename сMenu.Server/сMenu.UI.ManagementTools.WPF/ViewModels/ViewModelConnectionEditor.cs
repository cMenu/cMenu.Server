using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

using cMenu.Globalization;
using cMenu.Common;
using cMenu.Common.Base;
using cMenu.DB;

using cMenu.UI.ManagementTools.WPF.Core;
using cMenu.UI.ManagementTools.WPF.Core.MVVM;
using cMenu.UI.ManagementTools.WPF.Models.Core;

namespace cMenu.UI.ManagementTools.WPF.ViewModels
{
    public class ViewModelConnectionEditor : CMVVMCachedNotificationObject, IDataErrorInfo
    {
        #region ERRORS
        public string Error
        {
            get { return string.Empty; }
        }
        public string this[string Property]
        {
            get 
            {
                string ErrorMessage = string.Empty;
                switch (Property)
                {
                    case "DatabaseUser":
                        if (string.IsNullOrEmpty(DatabaseUser))
                            ErrorMessage = CGlobalizationHelper.sGetStringResource("ERROR_MT_DB_USER_NULL", CultureInfo.CurrentCulture);
                        break;
                    case "DatabasePassword":
                        if (string.IsNullOrEmpty(DatabasePassword))
                            ErrorMessage = CGlobalizationHelper.sGetStringResource("ERROR_MT_DB_PASS_NULL", CultureInfo.CurrentCulture);
                        break;
                    case "ServerAddress":
                        if (string.IsNullOrEmpty(ServerAddress))
                            ErrorMessage = CGlobalizationHelper.sGetStringResource("ERROR_MT_SERVER_ADDR_NULL", CultureInfo.CurrentCulture);
                        break;
                    case "DatabaseName":
                        if (string.IsNullOrEmpty(DatabaseName))
                            ErrorMessage = CGlobalizationHelper.sGetStringResource("ERROR_MT_DB_NAME_NULL", CultureInfo.CurrentCulture);
                        break;
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            ErrorMessage = CGlobalizationHelper.sGetStringResource("ERROR_MT_NAME_NULL", CultureInfo.CurrentCulture);
                        break;
                    case "LanguageCode":
                        /*if (LanguageCode == -1)
                            ErrorMessage = CGlobalizationHelper.sGetStringResource("ERROR_MT_LANGUAGE_CODE_NULL", CultureInfo.CurrentCulture);*/
                        break;
                }

                return ErrorMessage;
            }
        }
        #endregion

        #region PROTECTED FIELDS
        protected EnEditorFormMode _mode = EnEditorFormMode.ECreate;
        protected CMTApplicationConnectionConfiguration _configurationSource;
        protected ModelApplicationConnectionConfigurationModel _configuration;
        #endregion        

        #region PUBLIC FIELDS
        public EnEditorFormMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        public CMTApplicationConnectionConfiguration ConfigurationSource
        {
            get { return _configurationSource; }
            set
            {
                _configurationSource = value;
                Configuration = new ModelApplicationConnectionConfigurationModel() { ConfigurationSource = value };
                RaisePropertyChanged("ConfigurationSource");
            }
        }
        public ModelApplicationConnectionConfigurationModel Configuration
        {
            get { return _configuration; }
            set 
            { 
                _configuration = value;
                _initializeCache();
            }
        }

        public int SelectedDatabaseTypeIndex
        {
            get { return (int)DatabaseType; }
            set { DatabaseType = (EnServerDB)value; }
        }
        public EnServerDB DatabaseType
        {
            get { return _configuration.DatabaseType; }
            set {  _configuration.DatabaseType = value; }
        }
        public string DatabaseUser
        {
            get { return _configuration.DatabaseUser; }
            set { _configuration.DatabaseUser = value; }
        }
        public string DatabasePassword
        {
            get { return _configuration.DatabasePassword; }
            set { _configuration.DatabasePassword = value; }
        }
        public string SystemUser
        {
            get { return _configuration.SystemUser; }
            set { _configuration.SystemUser = value; }
        }
        public string SystemPassword
        {
            get { return _configuration.SystemPassword; }
            set { _configuration.SystemPassword = value; }
        }
        public string ServerAddress
        {
            get { return _configuration.ServerAddress; }
            set { _configuration.ServerAddress = value; }
        }
        public string DatabaseName
        {
            get { return _configuration.DatabaseName; }
            set { _configuration.DatabaseName = value; }
        }
        public string Name
        {
            get { return _configuration.Name; }
            set { _configuration.Name = value; }
        }
        public Guid ID
        {
            get { return _configuration.ID; }
            set { _configuration.ID = value; }
        }
        public bool SaveCredentials
        {
            get { return (bool)_cache["SaveCredentials"]; }
            set
            {
                _cache["SaveCredentials"] = value;
                RaisePropertyChanged("SaveCredentials");
            }
        }
        public int LanguageCode
        {
            get { return this._configuration.LanguageCode; }
            set { this._configuration.LanguageCode = value; }
        }
        #endregion        

        #region PROTECTED FUNCTIONS
        protected int _initializeCache()
        {
            this._cache.Values.Clear();
            this._configuration.Cache.Values.Clear();

            this.DatabaseName = this._configurationSource.DatabaseName;
            this.DatabasePassword = this._configurationSource.DatabasePassword;            
            this.DatabaseUser = this._configurationSource.DatabaseUser;
            this.ID = this._configurationSource.ID;
            this.Name = this._configurationSource.Name;
            this.ServerAddress = this._configurationSource.ServerAddress;
            this.SystemPassword = this._configurationSource.SystemPassword;
            this.SystemUser = this._configurationSource.SystemUser;
            this.SaveCredentials = (!string.IsNullOrEmpty(this._configurationSource.SystemUser) || string.IsNullOrEmpty(this._configurationSource.SystemPassword));
            this.LanguageCode = this._configurationSource.LanguageCode;
            this.SelectedDatabaseTypeIndex = (int)this._configurationSource.DatabaseType;

            if (this._mode == EnEditorFormMode.ECreate)
                this.DatabaseType = EnServerDB.EMsSQL;

            return -1;
        }
        #endregion

        #region COMMAND FUNCTIONS
        protected void _onNewGuidAction()
        {
            this.ID = Guid.NewGuid();
            RaisePropertyChanged("ID");
        }
        protected bool _onNewGuidActionCondition()
        {
            return this._mode != EnEditorFormMode.EView;
        }
        protected void _onSaveAction()
        {
            this._configurationSource.DatabaseType = this.DatabaseType;
            this._configurationSource.DatabaseUser = this.DatabaseUser;
            this._configurationSource.DatabasePassword = this.DatabasePassword;
            this._configurationSource.ServerAddress = this.ServerAddress;
            this._configurationSource.DatabaseName = this.DatabaseName;
            this._configurationSource.Name = this.Name;
            this._configurationSource.ID = this.ID;
            this._configurationSource.SystemUser = (this.SaveCredentials ? this.SystemUser : "");
            this._configurationSource.SystemPassword = (this.SaveCredentials ? this.SystemPassword : "");
            this._configurationSource.LanguageCode = this.LanguageCode;

            if (this.OnDialogWindowsCloseQuery != null)
                this.OnDialogWindowsCloseQuery(this, true);
        }
        protected void _onCancelAction()
        {            
            if (this.OnDialogWindowsCloseQuery != null)
                this.OnDialogWindowsCloseQuery(this, false);
        }
        protected bool _onCancelActionCondition()
        {
            return true;
        }
        #endregion

        #region COMMANDS
        public ICommand CmdNewGuid
        {
            get
            { return new CMVVMDelegateCommand(this._onNewGuidAction, this._onNewGuidActionCondition); }
        }
        public ICommand CmdCancel
        {
            get
            { return new CMVVMDelegateCommand(this._onCancelAction, this._onCancelActionCondition); }
        }
        public ICommand CmdSave
        {
            get
            { return new CMVVMDelegateCommand(this._onSaveAction); }
        }
        #endregion

        #region EVENTS
        public event OnDialogWindowsCloseQueryDelegate OnDialogWindowsCloseQuery;
        #endregion

        #region CONSTRUCTORS
        public ViewModelConnectionEditor()
        {
            this.ConfigurationSource = new CMTApplicationConnectionConfiguration();
        }
        #endregion  
    }
}
