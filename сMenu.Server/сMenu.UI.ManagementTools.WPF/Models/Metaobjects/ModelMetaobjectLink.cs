using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using cMenu.Metaobjects;
using cMenu.UI.ManagementTools.WPF.Core;
using cMenu.UI.ManagementTools.WPF.Core.MVVM;

namespace cMenu.UI.ManagementTools.WPF.Models.Metaobjects
{
    public class ModelMetaobjectLink : CMVVMCachedNotificationObject
    {
        #region PROTECTED FIELDS
        protected CMetaobjectLink _linkSource = null;
        protected ModelMetaobject _sourceObjectModel = null;
        protected ModelMetaobject _linkedObjectModel = null;
        #endregion

        #region PUBLIC FIELDS
        public CMetaobjectLink LinkSource
        {
            get { return _linkSource; }
            set
            {
                _linkSource = value;
                _initializeCache();
                RaisePropertyChanged("LinkSource");
            }
        }

        public decimal SourceObjectKey
        {
            get { return (decimal)_cache["SourceObjectKey"]; }
            set
            {
                _cache["SourceObjectKey"] = value;
                SourceObjectModel = _getMetaobjectModel(value);
            }
        }
        public decimal LinkedObjectKey
        {
            get { return (decimal)_cache["LinkedObjectKey"]; }
            set
            {
                _cache["LinkedObjectKey"] = value;
                LinkedObjectModel = _getMetaobjectModel(value);
            }
        }
        public decimal LinkValue
        {
            get { return (decimal)_cache["LinkValue"]; }
            set
            {
                _cache["LinkValue"] = value;
                RaisePropertyChanged("LinkValue");
            }
        }
        public EnMetaobjectLinkType LinkType
        {
            get { return (EnMetaobjectLinkType)_cache["LinkType"]; }
            set
            {
                _cache["LinkType"] = value;
                RaisePropertyChanged("LinkType");
            }
        }
        public ModelMetaobject SourceObjectModel
        {
            get { return _sourceObjectModel; }
            set 
            { 
                _sourceObjectModel = value;
                RaisePropertyChanged("SourceObject");
            }
        }
        public ModelMetaobject LinkedObjectModel
        {
            get { return _linkedObjectModel; }
            set
            {
                _linkedObjectModel = value;
                RaisePropertyChanged("LinkedObject");
            }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected int _initializeCache()
        {
            this._cache.Values.Clear();

            this.SourceObjectKey = this._linkSource.SourceObjectKey;
            this.LinkedObjectKey = this._linkSource.LinkedObjectKey;
            this.LinkValue = this._linkSource.LinkValue;
            this.LinkType = this._linkSource.LinkType;

            return -1;
        }
        protected ModelMetaobject _getMetaobjectModel(decimal Key)
        {
            var Metaobject = new CMetaobject(Key, CMTApplicationContext.CurrentProvider);
            return (Metaobject.ID == Guid.Empty ? null : new ModelMetaobject() { MetaobjectSource = Metaobject });
        }
        protected int _saveFromCache()
        {
            this._linkSource.SourceObjectKey = this.SourceObjectKey;
            this._linkSource.LinkedObjectKey = this.LinkedObjectKey;
            this._linkSource.LinkValue = this.LinkValue;
            this._linkSource.LinkType = this.LinkType;
            return -1;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int InitializeCache()
        {
            return this._initializeCache();
        }
        public int SaveValuesFromCache()
        {
            return this._saveFromCache();
        }
        #endregion
    }
}
