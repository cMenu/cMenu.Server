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

using cMenu.Globalization;
using cMenu.Common;
using cMenu.Common.Base;
using cMenu.DB;
using cMenu.IO;

using cMenu.UI.ManagementTools.WPF.Core;
using cMenu.UI.ManagementTools.WPF.Core.MVVM;
using cMenu.UI.ManagementTools.WPF.Views;
using cMenu.UI.ManagementTools.WPF.Models;
using cMenu.UI.ManagementTools.WPF.Models.Core;

namespace cMenu.UI.ManagementTools.WPF.ViewModels
{
    public class ViewModelConnectionManager : CMVVMCachedNotificationObject, IDataErrorInfo
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
                Configurations = new ObservableCollection<ModelApplicationConnectionConfigurationModel>(this._configurationsList.Select(c => new ModelApplicationConnectionConfigurationModel() { ConfigurationSource = c.Clone<CMTApplicationConnectionConfiguration>() }));
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
                RaisePropertyChanged("SelectedConfiguration");
                RaisePropertyChanged("");
            }
        }
        #endregion

        #region COMMAND FUNCTIONS
        protected void _onSaveAction()
        {
            this._configurationsList = this._configurations.Select(c => c.ConfigurationSource).ToList();
            if (this.OnDialogWindowsCloseQuery != null)
                this.OnDialogWindowsCloseQuery(this, true);
        }
        protected bool _onSaveActionCondition()
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
        protected void _onAddAction()
        {
            frmMtConnectionEditor frm = new frmMtConnectionEditor();
            ViewModelConnectionEditor ViewModel = new ViewModelConnectionEditor()
            {
                ConfigurationSource = new CMTApplicationConnectionConfiguration(),
                Mode = EnEditorFormMode.ECreate
            };
            frm.DataContext = ViewModel;            
            ViewModel.OnDialogWindowsCloseQuery += new OnDialogWindowsCloseQueryDelegate(
                (s, r) =>
                {
                    if (r)
                        this.Configurations.Add(new ModelApplicationConnectionConfigurationModel() { ConfigurationSource = (s as ViewModelConnectionEditor).Configuration.ConfigurationSource });                        
                    frm.Close();
                }
            );
            frm.ShowDialog();
        }
        protected bool _onAddActionCondition()
        {
            return true;
        }
        protected void _onEditAction()
        {
            frmMtConnectionEditor frm = new frmMtConnectionEditor();
            ViewModelConnectionEditor ViewModel = new ViewModelConnectionEditor()
            {
                Mode = EnEditorFormMode.EEdit,
                ConfigurationSource = this._selectedConfiguration.ConfigurationSource                
            };
            frm.DataContext = ViewModel;
            ViewModel.OnDialogWindowsCloseQuery += new OnDialogWindowsCloseQueryDelegate(
                (s, r) =>
                {
                    if (r)
                        this.SelectedConfiguration.ConfigurationSource = (s as ViewModelConnectionEditor).Configuration.ConfigurationSource;                        
                    frm.Close();
                }
            );
            frm.ShowDialog();
        }
        protected bool _onEditActionCondition()
        {
            return (this._selectedConfiguration != null);
        }
        protected void _onRemoveAction()
        {
            var R = MessageBox.Show(CGlobalizationHelper.sGetStringResource("ERROR_MT_DELETE_ITEM", CultureInfo.CurrentCulture), "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (R == MessageBoxResult.Yes)
            {
                this.Configurations.Remove(this.SelectedConfiguration);
                this.SelectedConfiguration = null;
            }
        }
        protected bool _onRemoveActionCondition()
        {
            return (this._selectedConfiguration != null);
        }
        #endregion

        #region COMMANDS
        public ICommand CmdSave
        {
            get { return new CMVVMDelegateCommand(this._onSaveAction, this._onSaveActionCondition); }
        }
        public ICommand CmdCancel
        {
            get { return new CMVVMDelegateCommand(this._onCancelAction, this._onCancelActionCondition); }
        }
        public ICommand CmdAdd
        {
            get { return new CMVVMDelegateCommand(this._onAddAction, this._onAddActionCondition); }
        }
        public ICommand CmdEdit
        {
            get { return new CMVVMDelegateCommand(this._onEditAction, this._onEditActionCondition); }
        }
        public ICommand CmdRemove
        {
            get { return new CMVVMDelegateCommand(this._onRemoveAction, this._onRemoveActionCondition); }
        }
        #endregion

        #region CONSTRUCTORS
        public ViewModelConnectionManager()
        { 
           
        }
        #endregion
    }
}
