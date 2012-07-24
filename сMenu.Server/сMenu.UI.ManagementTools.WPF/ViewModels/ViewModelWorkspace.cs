using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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
using cMenu.IO;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Extended;
using cMenu.Metaobjects.Extended.Helpers;
using cMenu.Security;
using cMenu.Security.MetaobjectsManagement;
using cMenu.Security.UsersManagement;

using cMenu.UI.ManagementTools.WPF.Core;
using cMenu.UI.ManagementTools.WPF.Core.MVVM;
using cMenu.UI.ManagementTools.WPF.Core.Helpers;
using cMenu.UI.ManagementTools.WPF.Models;
using cMenu.UI.ManagementTools.WPF.Models.Core;
using cMenu.UI.ManagementTools.WPF.Models.Metaobjects;
using cMenu.UI.ManagementTools.WPF.Views;
using cMenu.UI.ManagementTools.WPF.ViewModels;

namespace cMenu.UI.ManagementTools.WPF.ViewModels
{
    public class ViewModelWorkspace : CMVVMCachedNotificationObject, IDataErrorInfo
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

                return ErrorMessage;
            }
        }
        #endregion

        #region PROTECTED FIELDS
        protected EnClipboardOperation _clipboardOperation = EnClipboardOperation.ENone;
        protected EnActiveBrowserBlock _activeBlock = EnActiveBrowserBlock.EList;

        protected ModelMetaobject _rootMetaobject;
        protected ModelMetaobject _previousSelectedRootMetaobject;
        protected ModelMetaobject _selectedRootMetaobject;
        protected ModelMetaobject _selectedMetaobject;
        #endregion

        #region PUBLIC FIELDS
        public ModelMetaobject RootMetaobject
        {
            get { return _rootMetaobject; }
            set
            {
                _rootMetaobject = value;
                RaisePropertyChanged("RootMetaobject");
            }
        }
        public ModelMetaobject SelectedRootMetaobject
        {
            get { return _selectedRootMetaobject; }
            set
            {
                _selectedRootMetaobject = value;
                RaisePropertyChanged("SelectedRootMetaobject");
                SelectedMetaobject = (_selectedRootMetaobject == null || _selectedRootMetaobject.Children.Count == 0 ? null : _selectedRootMetaobject.Children[0]);
            }
        }
        public ModelMetaobject SelectedMetaobject
        {
            get { return _selectedMetaobject; }
            set
            {
                _selectedMetaobject = value;
                RaisePropertyChanged("SelectedMetaobject");
                RaisePropertyChanged("");
            }
        }

        public bool IsVisible
        {
            get { return CMTApplicationContext.CurrentConnection != null; }
        }

        public bool AllowCreateObject
        {
            get { return _onCreateObjectActionCondition(); }
        }
        public bool AllowEditObject
        {
            get { return _onEditObjectActionCondition(); }
        }
        public bool AllowRemoveObject
        {
            get { return _onRemoveObjectActionCondition(); }
        }
        public bool AllowCutObject
        {
            get { return _onCutObjectActionCondition(); }
        }
        public bool AllowCopyObject
        {
            get { return _onCopyObjectActionCondition(); }
        }
        public bool AllowPasteObject
        {
            get { return _onPasteObjectActionCondition(); }
        }
        public bool AllowLinkObject
        {
            get { return _onLinkObjectActionCondition(); }
        }
        public bool AllowViewProperties
        {
            get { return _onViewPropertiesActionCondition(); }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected ModelMetaobject _getActiveObject()
        {
            ModelMetaobject ObjectModel = null;
            switch (this._activeBlock)
            {
                case EnActiveBrowserBlock.EList: ObjectModel = this._selectedMetaobject; break;
                case EnActiveBrowserBlock.ETree: ObjectModel = this._selectedRootMetaobject; break;
            }

            return ObjectModel;
        }
        protected int _pasteChildObjects(CMetaobject Root)
        {
            foreach (CMetaobject Child in Root.Children)
            {
                Child.Attributes.AttributesGet(CMTApplicationContext.CurrentProvider);
                Child.Key = decimal.MaxValue;
                Child.ID = Guid.NewGuid();
                Child.Parent = Root.Key;
                Child.Name = CGlobalizationHelper.sGetStringResource("MSG_COPY", CultureInfo.CurrentCulture) + " " + Child.Name;                

                var R = Child.ObjectInsert(CMTApplicationContext.CurrentProvider);
                if (R != -1)
                    return R;

                R = this._setRightsForObject(Child);
                if (R != -1)
                    return R;

                this._pasteChildObjects(Child);
            }

            return -1;
        }
        protected int _setRightsForObject(CMetaobject Metaobject)
        {
            CSystemUserGroup Administrators = new CSystemUserGroup(CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY, CMTApplicationContext.CurrentProvider);
            CMetaobjectSecurityRecord Record = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = Metaobject.Key,
                Rights = 3,
                UserKey = Administrators.Key
            };

            var R = Record.RecordInsert(CMTApplicationContext.CurrentProvider);
            if (R != -1)
            {
                Metaobject.ObjectDeleteByKey(CMTApplicationContext.CurrentProvider);
                return -7;
            }

            Record = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = Metaobject.Key,
                Rights = 3,
                UserKey = CMTApplicationContext.CurrentUser.Key
            };
            R = Record.RecordInsert(CMTApplicationContext.CurrentProvider);
            if (R != -1)
            {
                Metaobject.ObjectDeleteByKey(CMTApplicationContext.CurrentProvider);
                return -7;
            }

            var Groups = CSystemUserGroup.sGetAllGroups(CMTApplicationContext.CurrentProvider);
            foreach (CSystemUserGroup Group in Groups)
            {
                if (Group.ID == Guid.Parse(CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_ID))
                    continue;

                Record = new CMetaobjectSecurityRecord()
                {
                    MetaobjectKey = Metaobject.Key,
                    Rights = 1,
                    UserKey = Group.Key
                };
                R = Record.RecordInsert(CMTApplicationContext.CurrentProvider);
                if (R != -1)
                {
                    Metaobject.ObjectDeleteByKey(CMTApplicationContext.CurrentProvider);
                    return -7;
                }
            }

            return -1;
        }
        protected int _closeConnection()
        {
            this.SelectedMetaobject = null;
            this.SelectedRootMetaobject = null;
            this.RootMetaobject = null;            

            return -1;
        }
        protected int _refresh()
        {
            decimal RootObjectKey = (CMTApplicationContext.CurrentConnection.IsConnectionSecurity ? CEmbeddedObjectsConsts.CONST_FOLDER_SHADOW_ROOT_KEY : CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY);
            CMetaobject MetaobjectSrc = new CMetaobject(RootObjectKey, CMTApplicationContext.CurrentProvider);
            MetaobjectSrc.GetChildren(CMTApplicationContext.CurrentProvider, true);

            ModelMetaobject Metaobject = new ModelMetaobject();
            if (!CMTApplicationContext.CurrentConnection.IsConnectionSecurity)
            {
                CMTApplicationContext.CurrentConnection.User.GetSecurityRecords(CMTApplicationContext.CurrentConnection.Provider);
                var MetaobjectsList = CMetaobjectHelper.sFilterObjectsByUser(new List<CMetaobject>() { MetaobjectSrc }, CMTApplicationContext.CurrentConnection.User, false);
                Metaobject.MetaobjectSource = (MetaobjectsList.Count != 0 ? MetaobjectsList[0] : null);
            }
            else
                Metaobject.MetaobjectSource = MetaobjectSrc;

            Metaobject.LanguageCode = CMTApplicationContext.CurrentLanguage;

            this.RootMetaobject = Metaobject;
            this.SelectedRootMetaobject = Metaobject.Children[0];

            CMTApplicationContext.CurrentConnection.User.GetSecurityRecords(CMTApplicationContext.CurrentConnection.Provider);
            CMTApplicationContext.CurrentConnection.User.GetPolicies(CMTApplicationContext.CurrentConnection.Provider);

            return -1;
        }
        protected int _refreshUser()
        {
            CMTApplicationContext.CurrentUser.GetPolicies(CMTApplicationContext.CurrentProvider);
            CMTApplicationContext.CurrentUser.GetSecurityRecords(CMTApplicationContext.CurrentProvider);
            return -1;
        }
        protected int _moveObject(ModelMetaobject Source, ModelMetaobject Target)
        {            
            ModelMetaobject Parent = this._rootMetaobject.FindObjectByKey(Source.Parent);
            ModelMetaobject Model = Source.FindObjectByKey(Target.Key);
            if (Model != null)
                return -5;            
            
            if (Source.MetaobjectSource.System)
                return -4;

            if (!CMTApplicationContext.CurrentConnection.User.PolicyAllowEditObjectsList())
                return -2;

            if (Target.Class == EnMetaobjectClass.ECategory && Source.Class == EnMetaobjectClass.EMenuService)
            {
                CMetaobjectShortcut Shortcut = new CMetaobjectShortcut(CMTApplicationContext.CurrentProvider)
                {
                    Description = Source.Description,
                    ID = Guid.NewGuid(),
                    ModificatonDate = DateTime.Now,
                    Name = CGlobalizationHelper.sGetStringResource("MSG_SHORTCUT", CultureInfo.CurrentCulture) + " " + Source.Name,
                    Parent = Target.Key,
                    Status = EnMetaobjectStatus.EEnabled,
                    SourceObjectKey = Source.Key
                };
                var R = Shortcut.ObjectInsert(CMTApplicationContext.CurrentProvider);
                if (R != -1)
                    return -6;

                R = this._setRightsForObject(Shortcut);
                if (R != -1)
                    return R;

                var ShortcutModel = new ModelMetaobject(){ MetaobjectSource = Shortcut };
                Target.Children.Add(ShortcutModel);

                this._refreshUser();
                return -1;
            }

            Source.MetaobjectSource.Parent = Target.MetaobjectSource.Key;
            var RR = Source.MetaobjectSource.ObjectUpdate(CMTApplicationContext.CurrentConnection.Provider);
            if (RR != -1)
                return -3;
            
            Target.Children.Add(Source);
            Parent.Children.Remove(Source);
            Source.InitializeCache();
            return -1;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int CloseConnection()
        {
            return this._closeConnection();
        }
        public int Refresh()
        {
            return this._refresh();
        }
        public int MoveObject(ModelMetaobject Source, ModelMetaobject Target)
        {
            return this._moveObject(Source, Target);
        }
        #endregion        

        #region COMMAND FUNCTIONS

        #region OBJECT CREATING
        protected void _onCreateFolderAction()
        {
 
        }
        protected bool _onCreateFolderActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateFolder(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreateAdverAction()
        {

        }
        protected bool _onCreateAdverActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateAdvertisement(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreateMediaAction()
        {

        }
        protected bool _onCreateMediaActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateMediaResource(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreateShortcutAction()
        {

        }
        protected bool _onCreateShortcutActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateShortcut(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreateOrganizationAction()
        {

        }
        protected bool _onCreateOrganizationActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateOrganization(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreateMenuAction()
        {

        }
        protected bool _onCreateMenuActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateMenu(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreateCategoryAction()
        {

        }
        protected bool _onCreateCategoryActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateCategory(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreateServiceAction()
        {

        }
        protected bool _onCreateServiceActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateService(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreateServiceAmountAction()
        {

        }
        protected bool _onCreateServiceAmountActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateServiceAmount(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreateUserAction()
        {

        }
        protected bool _onCreateUserActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateUser(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreateUserGroupAction()
        {

        }
        protected bool _onCreateUserGroupActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateUserGroup(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreatePolicyAction()
        {

        }
        protected bool _onCreatePolicyActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreatePolicy(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCreateDeviceAction()
        {

        }
        protected bool _onCreateDeviceActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateDevice(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        #endregion

        protected void _onTreeFocusedAction()
        {
            this._activeBlock = EnActiveBrowserBlock.ETree;
            RaisePropertyChanged("");
        }
        protected void _onListFocusedAction()
        {
            this._activeBlock = EnActiveBrowserBlock.EList;
            RaisePropertyChanged("");
        }
        protected void _onRefreshAction()
        {
            this._refresh();
        }
        protected bool _onRefreshActionCondition()
        {
            return (CMTApplicationContext.CurrentConnection != null);
        }
        protected void _onCreateObjectAction()
        {
            
        }
        protected bool _onCreateObjectActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (this._selectedRootMetaobject == null)
                return false;

            return CMTSecurityHelper.sAllowCreateObject(this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onEditObjectAction()
        {
 
        }
        protected bool _onEditObjectActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;
            var SelectedObject = this._getActiveObject();

            if (SelectedObject == null)
                return false;

            return CMTSecurityHelper.sAllowEditObject(SelectedObject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onRemoveObjectAction()
        {
            var SelectedObject = this._getActiveObject();
            var ParentObject = this._rootMetaobject.FindObjectByKey(SelectedObject.Parent);

            var R = MessageBox.Show(string.Format(CGlobalizationHelper.sGetStringResource("ERROR_MT_DELETE_ITEM_2", CultureInfo.CurrentCulture) + " '{0}'?", SelectedObject.Name), "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (R != MessageBoxResult.Yes)
                return;

            var RR = SelectedObject.MetaobjectSource.ObjectDeleteByKey(CMTApplicationContext.CurrentProvider);
            if (RR != -1)
                return;

            ParentObject.Children.Remove(SelectedObject);
            this.SelectedRootMetaobject = ParentObject;
            this._refreshUser();
        }
        protected bool _onRemoveObjectActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            var SelectedObject = this._getActiveObject();

            if (SelectedObject == null)
                return false;

            return CMTSecurityHelper.sAllowRemoveObject(SelectedObject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCutObjectAction()
        {
            if (CMTApplicationContext.Clipboard != null)
                ((ModelMetaobject)CMTApplicationContext.Clipboard).IsCuted = false;

            var SelectedObject = this._getActiveObject();

            CMTApplicationContext.Clipboard = SelectedObject;
            Clipboard.SetDataObject(SelectedObject.MetaobjectSource.SerializeJSONStream().ToDataString());
            this._clipboardOperation = EnClipboardOperation.ECut;
            SelectedObject.IsCuted = true;
            this.RaisePropertyChanged("");
        }
        protected bool _onCutObjectActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            var SelectedObject = this._getActiveObject();

            if (SelectedObject == null)
                return false;

            return CMTSecurityHelper.sAllowCutObject(SelectedObject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onCopyObjectAction()
        {
            if (CMTApplicationContext.Clipboard != null)
                ((ModelMetaobject)CMTApplicationContext.Clipboard).IsCuted = false;

            var SelectedObject = this._getActiveObject();

            CMTApplicationContext.Clipboard = SelectedObject;
            Clipboard.SetDataObject(SelectedObject.MetaobjectSource.SerializeJSONStream().ToDataString());
            this._clipboardOperation = EnClipboardOperation.ECopy;
            this.RaisePropertyChanged("");
        }
        protected bool _onCopyObjectActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            var SelectedObject = this._getActiveObject();

            if (SelectedObject == null)
                return false;

            return CMTSecurityHelper.sAllowCopyObject(SelectedObject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onPasteObjectAction()
        {
            var SelectedObject = this._getActiveObject();
            var ParentSelectedObject = this._rootMetaobject.FindObjectByKey(SelectedObject.Parent);
            var PastedObjectModel = (ModelMetaobject)CMTApplicationContext.Clipboard;

            if (PastedObjectModel == null)
                return;

            PastedObjectModel.MetaobjectSource.Attributes.AttributesGet(CMTApplicationContext.CurrentProvider);
            var OldParent = PastedObjectModel.Parent;

            if (this._clipboardOperation == EnClipboardOperation.ECut)
            {
                var DeleteR = PastedObjectModel.MetaobjectSource.ObjectDeleteByKey(CMTApplicationContext.CurrentProvider);
                if (DeleteR != -1)
                    return;
            }

            #region INSERT SHORTCUT
            if (PastedObjectModel.Class == EnMetaobjectClass.EMenuService && this._selectedRootMetaobject.Class == EnMetaobjectClass.ECategory && this._clipboardOperation == EnClipboardOperation.ECopy)
            {
                CMetaobjectShortcut Shortcut = new CMetaobjectShortcut(CMTApplicationContext.CurrentProvider)
                {
                    Description = PastedObjectModel.Description,
                    ID = Guid.NewGuid(),
                    ModificatonDate = DateTime.Now,
                    Name = CGlobalizationHelper.sGetStringResource("MSG_SHORTCUT", CultureInfo.CurrentCulture) + " " + PastedObjectModel.Name,
                    Parent = this._selectedRootMetaobject.Key,
                    Status = EnMetaobjectStatus.EEnabled,
                    SourceObjectKey = PastedObjectModel.Key
                };
                var R = Shortcut.ObjectInsert(CMTApplicationContext.CurrentProvider);
                if (R != -1)
                    return;

                R = this._setRightsForObject(Shortcut);
                if (R != -1)
                    return;


                var ShortcutModel = new ModelMetaobject() { MetaobjectSource = Shortcut };
                this._selectedRootMetaobject.Children.Add(ShortcutModel);
                this._refreshUser();

                return;
            }
            #endregion            
                        
            var NewObject = PastedObjectModel.MetaobjectSource.Clone<CMetaobject>();

            NewObject.Key = decimal.MaxValue;
            NewObject.ID = Guid.NewGuid();
            NewObject.ModificatonDate = DateTime.Now;
            NewObject.Parent = this._selectedRootMetaobject.Key;
            NewObject.Name = (this._clipboardOperation == EnClipboardOperation.ECopy ? CGlobalizationHelper.sGetStringResource("MSG_COPY", CultureInfo.CurrentCulture) : "") + " " + PastedObjectModel.Name;

            var RR = NewObject.ObjectInsert(CMTApplicationContext.CurrentProvider);
            if (RR != -1)
                return;

            RR = this._setRightsForObject(NewObject);
            if (RR != -1)
                return;

            RR = this._pasteChildObjects(NewObject);
            if (RR != -1)
                return;

            if (this._clipboardOperation == EnClipboardOperation.ECut)
            {
                var ParentModel = this._rootMetaobject.FindObjectByKey(OldParent);
                ParentModel.Children.Remove(PastedObjectModel);
                this._clipboardOperation = EnClipboardOperation.ENone;
                CMTApplicationContext.Clipboard = null;
            }

            var Model = new ModelMetaobject() { MetaobjectSource = NewObject };
            switch (this._activeBlock)
            {
                case EnActiveBrowserBlock.ETree: SelectedObject.Children.Add(Model); break;
                case EnActiveBrowserBlock.EList: ParentSelectedObject.Children.Add(Model); break;
            }
            this._refreshUser();            
        }
        protected bool _onPasteObjectActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            if (CMTApplicationContext.Clipboard == null)
                return false;

            return CMTSecurityHelper.sAllowPasteObject(((ModelMetaobject)CMTApplicationContext.Clipboard).MetaobjectSource, this._selectedRootMetaobject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onLinkObjectAction()
        {

        }
        protected bool _onLinkObjectActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            var SelectedObject = this._getActiveObject();

            if (SelectedObject == null)
                return false;

            return CMTSecurityHelper.sAllowLinkObject(SelectedObject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        protected void _onViewPropertiesAction()
        {
            var SelectedObject = this._getActiveObject();
            ModelMetaobject Model = new ModelMetaobject() { MetaobjectSource = SelectedObject.MetaobjectSource, LanguageCode = CMTApplicationContext.CurrentLanguage };

            ViewModelProperties ViewModel = new ViewModelProperties() { MetaobjectModel = Model, Mode = EnEditorFormMode.EEdit };
            ViewProperties View = new ViewProperties();
            View.DataContext = ViewModel;
            ViewModel.OnDialogWindowsCloseQuery += new OnDialogWindowsCloseQueryDelegate
                (
                    (c, r) =>
                    {
                        View.Close();
                        if (r)
                        {
                            SelectedObject.MetaobjectSource = (View.DataContext as ViewModelProperties).MetaobjectModel.MetaobjectSource;
                            SelectedObject.MetaobjectSource.ObjectUpdate(CMTApplicationContext.CurrentProvider);
                        }
                    }
                );
            View.ShowDialog();
        }
        protected bool _onViewPropertiesActionCondition()
        {
            if (CMTApplicationContext.CurrentConnection == null || CMTApplicationContext.CurrentProvider == null || CMTApplicationContext.CurrentUser == null)
                return false;

            var SelectedObject = this._getActiveObject();
            if (SelectedObject == null)
                return false;

            return CMTSecurityHelper.sAllowViewProperties(SelectedObject.MetaobjectSource, CMTApplicationContext.CurrentUser);
        }
        #endregion

        #region COMMANDS

        #region OBJECT COMMANDS
        public ICommand CmdCreateFolder
        {
            get { return new CMVVMDelegateCommand(this._onCreateFolderAction, this._onCreateFolderActionCondition); }
        }
        public ICommand CmdCreateAdver
        {
            get { return new CMVVMDelegateCommand(this._onCreateAdverAction, this._onCreateAdverActionCondition); }
        }
        public ICommand CmdCreateMedia
        {
            get { return new CMVVMDelegateCommand(this._onCreateMediaAction, this._onCreateMediaActionCondition); }
        }
        public ICommand CmdCreateShortcut
        {
            get { return new CMVVMDelegateCommand(this._onCreateShortcutAction, this._onCreateShortcutActionCondition); }
        }
        public ICommand CmdCreateOrganization
        {
            get { return new CMVVMDelegateCommand(this._onCreateOrganizationAction, this._onCreateOrganizationActionCondition); }
        }
        public ICommand CmdCreateMenu
        {
            get { return new CMVVMDelegateCommand(this._onCreateMenuAction, this._onCreateMenuActionCondition); }
        }
        public ICommand CmdCreateCategory
        {
            get { return new CMVVMDelegateCommand(this._onCreateCategoryAction, this._onCreateCategoryActionCondition); }
        }
        public ICommand CmdCreateService
        {
            get { return new CMVVMDelegateCommand(this._onCreateServiceAction, this._onCreateServiceActionCondition); }
        }
        public ICommand CmdCreateServiceAmount
        {
            get { return new CMVVMDelegateCommand(this._onCreateServiceAmountAction, this._onCreateServiceAmountActionCondition); }
        }
        public ICommand CmdCreateUser
        {
            get { return new CMVVMDelegateCommand(this._onCreateUserAction, this._onCreateUserActionCondition); }
        }
        public ICommand CmdCreateUserGroup
        {
            get { return new CMVVMDelegateCommand(this._onCreateUserGroupAction, this._onCreateUserGroupActionCondition); }
        }
        public ICommand CmdCreatePolicy
        {
            get { return new CMVVMDelegateCommand(this._onCreatePolicyAction, this._onCreatePolicyActionCondition); }
        }
        public ICommand CmdCreateDevice
        {
            get { return new CMVVMDelegateCommand(this._onCreateDeviceAction, this._onCreateDeviceActionCondition); }
        }
        #endregion

        public ICommand CmdFocusTree
        {
            get { return new CMVVMDelegateCommand(this._onTreeFocusedAction); }
        }
        public ICommand CmdFocusList
        {
            get { return new CMVVMDelegateCommand(this._onListFocusedAction); }
        }
        public ICommand CmdRefresh
        {
            get { return new CMVVMDelegateCommand(this._onRefreshAction, this._onRefreshActionCondition); }
        }
        public ICommand CmdCreateObject
        {
            get { return new CMVVMDelegateCommand(this._onCreateObjectAction, this._onCreateObjectActionCondition); }
        }
        public ICommand CmdEditObject
        {
            get { return new CMVVMDelegateCommand(this._onEditObjectAction, this._onEditObjectActionCondition); }
        }
        public ICommand CmdRemoveObject
        {
            get { return new CMVVMDelegateCommand(this._onRemoveObjectAction, this._onRemoveObjectActionCondition); }
        }
        public ICommand CmdCutObject
        {
            get { return new CMVVMDelegateCommand(this._onCutObjectAction, this._onCutObjectActionCondition); }
        }
        public ICommand CmdCopyObject
        {
            get { return new CMVVMDelegateCommand(this._onCopyObjectAction, this._onCopyObjectActionCondition); }
        }
        public ICommand CmdPasteObject
        {
            get { return new CMVVMDelegateCommand(this._onPasteObjectAction, this._onPasteObjectActionCondition); }
        }
        public ICommand CmdLinkObject
        {
            get { return new CMVVMDelegateCommand(this._onLinkObjectAction, this._onLinkObjectActionCondition); }
        }
        public ICommand CmdViewProperties
        {
            get { return new CMVVMDelegateCommand(this._onViewPropertiesAction, this._onViewPropertiesActionCondition); }
        }
        #endregion
    }
}
