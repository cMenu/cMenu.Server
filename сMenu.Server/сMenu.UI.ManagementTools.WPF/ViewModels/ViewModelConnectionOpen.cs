using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

using cMenu.IO;
using cMenu.Metaobjects;
using cMenu.Security;
using cMenu.Security.UsersManagement;
using cMenu.Globalization;

using cMenu.UI.ManagementTools.WPF.Core;
using cMenu.UI.ManagementTools.WPF.Core.MVVM;
using cMenu.UI.ManagementTools.WPF.Models;
using cMenu.UI.ManagementTools.WPF.Models.Core;

namespace cMenu.UI.ManagementTools.WPF.ViewModels
{
    public class ViewModelConnectionOpen : CMVVMCachedNotificationObject, IDataErrorInfo
    {
        #region EVENTS
        public event OnDialogWindowsCloseQueryDelegate OnDialogWindowsCloseQuery;
        #endregion

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
                    case "Login":
                        if (string.IsNullOrEmpty(this.Login))
                            ErrorMessage = CGlobalizationHelper.sGetStringResource("ERROR_MT_LOGIN_NULL", CultureInfo.CurrentCulture);
                        break;
                    case "Password":
                        if (string.IsNullOrEmpty(this.Password))
                            ErrorMessage = CGlobalizationHelper.sGetStringResource("ERROR_MT_PASS_NULL", CultureInfo.CurrentCulture);
                        break;
                }
                return ErrorMessage;
            }
        }
        #endregion

        #region PROTECTED FIELDS
        protected List<CMTApplicationConnectionConfiguration> _configurationsList = new List<CMTApplicationConnectionConfiguration>();
        protected ObservableCollection<ModelApplicationConnectionConfigurationModel> _configurations = new ObservableCollection<ModelApplicationConnectionConfigurationModel>();
        protected ModelApplicationConnectionConfigurationModel _selectedConfiguration;
        #endregion

        #region PUBLIC FIELDS
        public List<CMTApplicationConnectionConfiguration> ConfigurationsList
        {
            get { return _configurationsList; }
            set
            {
                _configurationsList = value;
                Configurations = new ObservableCollection<ModelApplicationConnectionConfigurationModel>( this._configurationsList.Select(c => new ModelApplicationConnectionConfigurationModel() { ConfigurationSource = c }));
                RaisePropertyChanged("ConfigurationsList");
            }
        }
        public ObservableCollection<ModelApplicationConnectionConfigurationModel> Configurations
        {
            get { return _configurations; }
            set
            {
                _configurations = value;
                RaisePropertyChanged("Configurations");
            }
        }
        public ModelApplicationConnectionConfigurationModel SelectedConfiguration
        {
            get { return _selectedConfiguration; }
            set
            {
                _selectedConfiguration = value;
                _initializeCache();
                RaisePropertyChanged("SelectedConfiguration");
                RaisePropertyChanged("");
            }
        }
        public string Login
        {
            get { return (string)_cache["Login"]; }
            set 
            {
                _cache["Login"] = value;
                RaisePropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return (string)_cache["Password"]; }
            set
            {
                _cache["Password"] = value;
                RaisePropertyChanged("Password");
            }
        }
        public bool SecurityConnection
        {
            get { return (_cache["SecurityConnection"] == null ? false : (bool)_cache["SecurityConnection"]); }
            set
            {
                _cache["SecurityConnection"] = value;
                RaisePropertyChanged("SecurityConnection");
            }
        }
        #endregion

        #region COMMAND FUNCTIONS
        protected void _onConnectAction()
        {
            var Login = this.Login;
            var Password = this.Password;
            var Connection = new CMTApplicationConnection() { Configuration = this.SelectedConfiguration.ConfigurationSource };

            Connection.Provider = CMTApplicationContext.sGetProvider(Connection);
            Connection.IsConnectionSecurity = this.SecurityConnection;

            if (!Connection.Provider.TestConnection())
            {
                MessageBox.Show(CGlobalizationHelper.sGetStringResource("ERROR_MT_SERVER_NOT_RESPOND", CultureInfo.CurrentCulture), "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var User = CSystemUser.sGetUserByLoginPasshash(Connection.Provider, Login, Password, CSecurityHelper.sGeneratePasshash(Login, Password));
            if (User == null)
            {
                MessageBox.Show(CGlobalizationHelper.sGetStringResource("ERROR_MT_AUTH_DATA_NULL", CultureInfo.CurrentCulture), "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (this.SecurityConnection && User.ID.ToString().ToUpper() != CEmbeddedSecurityConsts.CONST_USER_ADMINISTRATOR_ID)
            {
                MessageBox.Show(CGlobalizationHelper.sGetStringResource("ERROR_MT_USER_NOT_ENOUGH_RIGHTS_SEC_REPO", CultureInfo.CurrentCulture), "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Connection.User = User;
            CMTApplicationContext.CurrentConnection = Connection;

            if (this.OnDialogWindowsCloseQuery != null)
                this.OnDialogWindowsCloseQuery(this, true);
        }
        protected bool _onConnectActionCondition()
        {
            return true;
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
        public ICommand CmdConnect
        {
            get { return new CMVVMDelegateCommand(this._onConnectAction, this._onConnectActionCondition); }
        }
        public ICommand CmdCancel
        {
            get { return new CMVVMDelegateCommand(this._onCancelAction, this._onCancelActionCondition); }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected int _initializeCache()
        {
            this._cache.Values.Clear();

            this.Login = this._selectedConfiguration.SystemUser;
            this.Password = this._selectedConfiguration.SystemPassword;
            this.SecurityConnection = false;

            return -1;
        }
        #endregion

        #region CONSTRUCTORS
        public ViewModelConnectionOpen()
        {
        }
        #endregion
    }
}
