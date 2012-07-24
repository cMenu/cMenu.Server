using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Input;
using System.Windows.Media;

using cMenu.Metaobjects;
using cMenu.UI.ManagementTools.WPF.Core;
using cMenu.UI.ManagementTools.WPF.Core.Helpers;
using cMenu.UI.ManagementTools.WPF.Core.MVVM;
using cMenu.UI.ManagementTools.WPF.ViewModels;

namespace cMenu.UI.ManagementTools.WPF.Models.Metaobjects
{
    public class ModelMetaobject : CMVVMCachedNotificationObject
    {
        #region PROTECTED FIELDS
        protected bool _isCuted = false;
        protected bool _isSelectedInObjectsTree = false;

        protected CMetaobject _metaobjectSource = null;
        protected int _languageCode = -1;
        protected ObservableCollection<ModelMetaobject> _children = new ObservableCollection<ModelMetaobject>();
        protected ObservableCollection<ModelMetaobjectComment> _comments = new ObservableCollection<ModelMetaobjectComment>();
        protected ObservableCollection<ModelMetaobjectLink> _sourceLinks = new ObservableCollection<ModelMetaobjectLink>();
        protected ObservableCollection<ModelMetaobjectLink> _destinationLinks = new ObservableCollection<ModelMetaobjectLink>();
        #endregion

        #region PUBLIC FIELDS
        public ImageSource Icon
        {
            get 
            {
                string ResourceKey = "";
                switch (this.MetaobjectSource.Class)
                {
                    case EnMetaobjectClass.EAdvertisement: ResourceKey = "icoPolicySrc"; break;
                    case EnMetaobjectClass.ECategory: ResourceKey = "icoCategorySrc"; break;
                    case EnMetaobjectClass.EClientDevice: ResourceKey = "icoDeviceSrc"; break;
                    case EnMetaobjectClass.EFolder: ResourceKey = "icoFolderSrc"; break;
                    case EnMetaobjectClass.EMediaResource: ResourceKey = "icoMediaSrc"; break;
                    case EnMetaobjectClass.EMenu: ResourceKey = "icoMenuItemSrc"; break;
                    case EnMetaobjectClass.EMenuService: ResourceKey = "icoMenuServiceSrc"; break;
                    case EnMetaobjectClass.EMenuServiceAmount: ResourceKey = "icoMenuServiceAmountSrc"; break;
                    case EnMetaobjectClass.EOrganization: ResourceKey = "icoOrgSrc"; break;
                    case EnMetaobjectClass.EShortcut: ResourceKey = "icoShortcutSrc"; break;
                    case EnMetaobjectClass.ESystemPolicy: ResourceKey = "icoPolicySrc"; break;
                    case EnMetaobjectClass.ESystemUser: ResourceKey = "icoUserSrc"; break;
                    case EnMetaobjectClass.ESystemUserGroup: ResourceKey = "icoGroupSrc"; break;
                }
                return (ImageSource)App.Current.FindResource(ResourceKey);
            }
        }
        public ImageSource Icon32
        {
            get
            {
                string ResourceKey = "";
                switch (this.MetaobjectSource.Class)
                {
                    case EnMetaobjectClass.EAdvertisement: ResourceKey = "icoFolder32Src"; break;
                    case EnMetaobjectClass.ECategory: ResourceKey = "icoCategory32Src"; break;
                    case EnMetaobjectClass.EClientDevice: ResourceKey = "icoDevice32Src"; break;
                    case EnMetaobjectClass.EFolder: ResourceKey = "icoFolder32Src"; break;
                    case EnMetaobjectClass.EMediaResource: ResourceKey = "icoMedia32Src"; break;
                    case EnMetaobjectClass.EMenu: ResourceKey = "icoMenuItem32Src"; break;
                    case EnMetaobjectClass.EMenuService: ResourceKey = "icoMenuService32Src"; break;
                    case EnMetaobjectClass.EMenuServiceAmount: ResourceKey = "icoMenuServiceAmount32Src"; break;
                    case EnMetaobjectClass.EOrganization: ResourceKey = "icoOrg32Src"; break;
                    case EnMetaobjectClass.EShortcut: ResourceKey = "icoShortcut32Src"; break;
                    case EnMetaobjectClass.ESystemPolicy: ResourceKey = "icoPolicy32Src"; break;
                    case EnMetaobjectClass.ESystemUser: ResourceKey = "icoUser32Src"; break;
                    case EnMetaobjectClass.ESystemUserGroup: ResourceKey = "icoGroup32Src"; break;
                }
                return (ImageSource)App.Current.FindResource(ResourceKey);
            }
        }
        public bool IsSelectedInObjectsTree
        {
            get { return _isSelectedInObjectsTree; }
            set
            {
                _isSelectedInObjectsTree = value;
                RaisePropertyChanged("IsSelectedInObjectsTree");
                ViewModelMain.Active.Workspace.SelectedRootMetaobject = this;
            }
        }
        public bool IsCuted
        {
            get { return _isCuted; }
            set 
            { 
                _isCuted = value;
                RaisePropertyChanged("IsCuted");
            }
        }
        public bool AllowEdit
        {
            get { return CMTSecurityHelper.sAllowEditObject(this._metaobjectSource, CMTApplicationContext.CurrentUser); }
        }

        public CMetaobject MetaobjectSource
        {
            get { return _metaobjectSource; }
            set 
            { 
                _metaobjectSource = value;
                _initializeCache();
                _children = new ObservableCollection<ModelMetaobject>(_metaobjectSource.Children.Select(m => new ModelMetaobject() { MetaobjectSource = m}));
                _comments = new ObservableCollection<ModelMetaobjectComment>(_metaobjectSource.Comments.Select(c => new ModelMetaobjectComment() { CommentSource = c }));
                _sourceLinks = new ObservableCollection<ModelMetaobjectLink>(_metaobjectSource.ExternalLinks.Select(l => new ModelMetaobjectLink() { LinkSource = l}));
                _destinationLinks = new ObservableCollection<ModelMetaobjectLink>(_metaobjectSource.InternalLinks.Select(l => new ModelMetaobjectLink() { LinkSource = l }));

                RaisePropertyChanged("MetaobjectSource");
            }
        }
        public int LanguageCode
        {
            get { return _languageCode; }
            set
            {
                _languageCode = value;
                _initializeLocalizedCache();
                RaisePropertyChanged("LanguageCode");
                _changeCultureForChildObjects();
            }
        }
        public ObservableCollection<ModelMetaobject> Children
        {
            get { return _children; }
            set 
            {
                _children = value;
                RaisePropertyChanged("Children");
            }
        }
        public ObservableCollection<ModelMetaobjectComment> Comments
        {
            get { return _comments; }
            set 
            {
                _comments = value;
                RaisePropertyChanged("Comments");
            }
        }
        public ObservableCollection<ModelMetaobjectLink> SourceLinks
        {
            get { return _sourceLinks; }
            set
            {
                _sourceLinks = value;
                RaisePropertyChanged("SourceLinks");
            }
        }
        public ObservableCollection<ModelMetaobjectLink> DestinationLinks
        {
            get { return _destinationLinks; }
            set
            {
                _destinationLinks = value;
                RaisePropertyChanged("DestinationLinks");
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
        public string Description
        {
            get { return (string)_cache["Description"]; }
            set
            {
                _cache["Description"] = value;
                RaisePropertyChanged("Description");
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
        public decimal Key
        {
            get { return (decimal)_cache["Key"]; }
            set
            {
                _cache["Key"] = value;
                RaisePropertyChanged("Key");
            }
        }
        public decimal Parent
        {
            get { return (decimal)_cache["Parent"]; }
            set
            {
                _cache["Parent"] = value;
                RaisePropertyChanged("Parent");
            }
        }
        public bool System
        {
            get { return (bool)_cache["System"]; }
            set
            {
                _cache["System"] = value;
                RaisePropertyChanged("System");
            }
        }
        public DateTime ModificatonDate
        {
            get { return (DateTime)_cache["ModificatonDate"]; }
            set
            {
                _cache["ModificatonDate"] = value;
                RaisePropertyChanged("ModificatonDate");
            }
        }
        public EnMetaobjectClass Class
        {
            get { return (EnMetaobjectClass)_cache["Class"]; }
            set
            {
                _cache["Class"] = value;
                RaisePropertyChanged("Class");
            }
        }
        public EnMetaobjectStatus Status
        {
            get { return (EnMetaobjectStatus)_cache["Status"]; }
            set
            {
                _cache["Status"] = value;
                RaisePropertyChanged("Status");
            }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected ModelMetaobject _findObjectByKey(decimal Key, ModelMetaobject Root)
        {
            if (Root.Key == Key)
                return Root;
            foreach (ModelMetaobject Model in Root.Children)
            {
                var R = this._findObjectByKey(Key, Model);
                if (R != null)
                    return R;
            }
            return null;
        }
        protected int _initializeCache()
        {
            this._cache.Clear();
            
            this.ID = this._metaobjectSource.ID;
            this.Key = this._metaobjectSource.Key;
            this.Parent = this._metaobjectSource.Parent;
            this.System = this._metaobjectSource.System;
            this.ModificatonDate = this._metaobjectSource.ModificatonDate;
            this.Class = this._metaobjectSource.Class;
            this.Status = this._metaobjectSource.Status;

            this._initializeLocalizedCache();

            return -1;
        }
        protected int _initializeLocalizedCache()
        {
            this.Name = this._metaobjectSource.NameAttribute[this._languageCode];
            this.Description = this._metaobjectSource.DescriptionAttribute[this._languageCode];

            return -1;
        }
        protected int _saveFromCache()
        {
            this._metaobjectSource.NameAttribute[this._languageCode] = this.Name;
            this._metaobjectSource.DescriptionAttribute[this._languageCode] = this.Description;

            this._metaobjectSource.ID = this.ID;
            this._metaobjectSource.Parent = this.Parent;
            this._metaobjectSource.System = this.System;
            this._metaobjectSource.ModificatonDate = this.ModificatonDate;
            this._metaobjectSource.Class = this.Class;
            this._metaobjectSource.Status = this.Status;

            return -1;
        }
        protected int _changeCultureForChildObjects()
        {
            foreach (ModelMetaobject Model in this._children)
                Model.LanguageCode = this.LanguageCode;

            return -1;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int InitializeCache()
        {
            return this._initializeCache();
        }
        public int InititalizeLocalizedCache()
        {
            return this._initializeLocalizedCache();
        }
        public int SaveValuesFromCache()
        {
            return this._saveFromCache();
        }
        public ModelMetaobject FindObjectByKey(decimal Key)
        {
            return this._findObjectByKey(Key, this);
        }
        #endregion

        #region COMMAND FUNCTIONS
        protected void _onOpenInBrowserAction()
        {
            this.IsSelectedInObjectsTree = true;
        }
        #endregion

        #region COMMANDS
        public ICommand OpenInBrowser
        {
            get { return new CMVVMDelegateCommand(this._onOpenInBrowserAction); }
        }
        #endregion
    }
}