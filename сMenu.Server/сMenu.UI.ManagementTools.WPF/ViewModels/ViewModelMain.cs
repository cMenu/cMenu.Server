using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Globalization;

using GongSolutions.Wpf.DragDrop;

using cMenu.IO;
using cMenu.Metaobjects;
using cMenu.Security;
using cMenu.Globalization;

using cMenu.UI.ManagementTools.WPF.Core;
using cMenu.UI.ManagementTools.WPF.Core.MVVM;
using cMenu.UI.ManagementTools.WPF.Core.Helpers;
using cMenu.UI.ManagementTools.WPF.Views;
using cMenu.UI.ManagementTools.WPF.Models;
using cMenu.UI.ManagementTools.WPF.Models.Core;
using cMenu.UI.ManagementTools.WPF.Models.Metaobjects;
using cMenu.Metaobjects.Extended.Helpers;

namespace cMenu.UI.ManagementTools.WPF.ViewModels
{
    public class ViewModelMain : ViewModelBase, IDropTarget
    {
        #region PROTECTED FIELDS
        protected static ViewModelMain _active;
        protected ViewModelWorkspace _workspace = new ViewModelWorkspace();
        #endregion

        #region PUBLIC FIELDS
        public static ViewModelMain Active
        {
            get { return _active; }
            set { _active = value; }
        }
        public ViewModelWorkspace Workspace
        {
            get { return _workspace; }
            set
            {
                _workspace = value;
                RaisePropertyChanged("Workspace");
                RaisePropertyChanged("");
            }
        }
        #endregion

        #region  COMMAND FUNCTIONS
        
        protected void _onConnectToServerAction()
        {
            frmMtConnectionOpen frm = new frmMtConnectionOpen();
            ViewModelConnectionOpen ViewModel = new ViewModelConnectionOpen()
            {
                ConfigurationsList = CMTApplicationContext.ApplicationConfiguration.Connections
            };
            frm.DataContext = ViewModel;
            ViewModel.OnDialogWindowsCloseQuery += new OnDialogWindowsCloseQueryDelegate
                (
                    (c, r) =>
                    {
                        frm.Close();
                        if (r)
                            this._initializeWorkspace();                        
                    }
                );
            frm.ShowDialog();
        }
        protected bool _onConnectToServerActionCondition()
        {
            return (CMTApplicationContext.CurrentConnection == null);
        }
        protected void _onConnectionManagerAction()
        {
            frmMtConnectionManager frm = new frmMtConnectionManager();
            ViewModelConnectionManager ViewModel = new ViewModelConnectionManager()
            {
                ConfigurationsList = CMTApplicationContext.ApplicationConfiguration.Connections
            };
            frm.DataContext = ViewModel;
            ViewModel.OnDialogWindowsCloseQuery +=new OnDialogWindowsCloseQueryDelegate
                (
                    (c, r) =>
                        {
                            if (r)
                            {
                                CMTApplicationContext.ApplicationConfiguration.Connections = ViewModel.ConfigurationsList;
                                CMTApplicationContext.ApplicationConfiguration.SerializeXMLFile("Configuration.xml", typeof(CMTApplicationConfiguration));
                            }
                            frm.Close();

                        }
                );
            frm.ShowDialog();
        }
        protected void _onCloseAction()
        {
            System.Windows.Application.Current.Shutdown();
        }
        protected void _onCloseActiveConnectionAction()
        {
            var R = MessageBox.Show(CGlobalizationHelper.sGetStringResource("ERROR_MT_CLOSE_CONNECTION", CultureInfo.CurrentCulture), "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (R != MessageBoxResult.Yes)
                return;

            CMTApplicationContext.CurrentConnection = null;
            this.Workspace.CloseConnection();
            RaisePropertyChanged("Workspace");
            RaisePropertyChanged("");
        }
        protected bool _onCloseActiveConnectionActionCondition()
        {
            return (CMTApplicationContext.CurrentConnection != null);
        }
        #endregion

        #region COMMANDS
        public ICommand CmdConnectToServer
        {
            get
            { return new CMVVMDelegateCommand(this._onConnectToServerAction, this._onConnectToServerActionCondition); }
        }
        public ICommand CmdConnectionManagement
        {
            get { return new CMVVMDelegateCommand(this._onConnectionManagerAction); }
        }
        public ICommand CmdClose
        {
            get { return new CMVVMDelegateCommand(this._onCloseAction); }
        }
        public ICommand CmdCloseActiveConnection
        {
            get { return new CMVVMDelegateCommand(this._onCloseActiveConnectionAction, this._onCloseActiveConnectionActionCondition); }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        void IDropTarget.DragOver(DropInfo DropInfo)
        {
            if (!(DropInfo.Data is ModelMetaobject))
            {
                DropInfo.Effects = DragDropEffects.None;
                return;
            }

            ModelMetaobject SourceModel = (ModelMetaobject)DropInfo.Data;
            ModelMetaobject TargetModel = (ModelMetaobject)DropInfo.TargetItem;
            if (TargetModel == null && DropInfo.VisualTarget is TreeView)
                TargetModel = this.Workspace.RootMetaobject;
            if (TargetModel == null && DropInfo.VisualTarget is ListView)
                TargetModel = this.Workspace.SelectedRootMetaobject;

            var AllowDrop = CMTSecurityHelper.sAllowMoveObject(SourceModel.MetaobjectSource, TargetModel.MetaobjectSource, CMTApplicationContext.CurrentUser);
            DropInfo.Effects = (AllowDrop ? DragDropEffects.Move : DragDropEffects.None);

        }
        void IDropTarget.Drop(DropInfo DropInfo)
        {
            ModelMetaobject SourceModel = (ModelMetaobject)DropInfo.Data;
            ModelMetaobject TargetModel = (ModelMetaobject)DropInfo.TargetItem;
            if (TargetModel == null && DropInfo.VisualTarget is TreeView)
                TargetModel = this.Workspace.RootMetaobject;
            if (TargetModel == null && DropInfo.VisualTarget is ListView)
                TargetModel = this.Workspace.SelectedRootMetaobject;

            var AllowDrop = CMTSecurityHelper.sAllowMoveObject(SourceModel.MetaobjectSource, TargetModel.MetaobjectSource, CMTApplicationContext.CurrentUser);
            if (!AllowDrop)
                return;

            this._workspace.MoveObject(SourceModel, TargetModel);
        }

        protected int _initializeWorkspace()
        {
            CMTApplicationContext.CurrentConnection.User.GetSecurityRecords(CMTApplicationContext.CurrentConnection.Provider);
            CMTApplicationContext.CurrentConnection.User.GetPolicies(CMTApplicationContext.CurrentConnection.Provider);

            decimal RootObjectKey = (CMTApplicationContext.CurrentConnection.IsConnectionSecurity ? CEmbeddedObjectsConsts.CONST_FOLDER_SHADOW_ROOT_KEY : CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY);
            CMetaobject MetaobjectSrc = new CMetaobject(RootObjectKey, CMTApplicationContext.CurrentProvider);
            MetaobjectSrc.GetChildren(CMTApplicationContext.CurrentProvider, true);

            ModelMetaobject Metaobject = new ModelMetaobject();
            if (!CMTApplicationContext.CurrentConnection.IsConnectionSecurity)
            {
                var MetaobjectsList = CMetaobjectHelper.sFilterObjectsByUser(new List<CMetaobject>() { MetaobjectSrc }, CMTApplicationContext.CurrentConnection.User, false);
                Metaobject.MetaobjectSource = (MetaobjectsList.Count != 0 ? MetaobjectsList[0] : null);
            }
            else
                Metaobject.MetaobjectSource = MetaobjectSrc;

            Metaobject.LanguageCode = CMTApplicationContext.CurrentLanguage;

            this.Workspace = new ViewModelWorkspace();
            this.Workspace.RootMetaobject = Metaobject;
            this.Workspace.SelectedRootMetaobject = Metaobject.Children[0];

            return -1;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        #endregion

        #region CONSTRUCTORS
        public ViewModelMain()
        {
            ViewModelMain.Active = this;
        }
        #endregion
    }
}