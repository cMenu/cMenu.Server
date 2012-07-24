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
    public class ModelMetaobjectComment : CMVVMCachedNotificationObject
    {
        #region PROTECTED FIELDS
        protected CMetaobjectComment _commentSource = null;
        protected ObservableCollection<ModelMetaobjectComment> _children = new ObservableCollection<ModelMetaobjectComment>();
        #endregion

        #region PUBLIC FIELDS
        public CMetaobjectComment CommentSource
        {
            get { return _commentSource; }
            set
            {
                _commentSource = value;
                _children = new ObservableCollection<ModelMetaobjectComment>(_commentSource.Children.Select(c => new ModelMetaobjectComment() { CommentSource = c}));
                _initializeCache();
                RaisePropertyChanged("CommentSource");
            }
        }
        public ObservableCollection<ModelMetaobjectComment> Children
        {
            get { return _children; }
            set 
            { 
                _children = value;
                RaisePropertyChanged("Children");
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
        public decimal ObjectKey
        {
            get { return (decimal)_cache["ObjectKey"]; }
            set
            {
                _cache["ObjectKey"] = value;
                RaisePropertyChanged("ObjectKey");
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
        public string Author
        {
            get { return (string)_cache["Author"]; }
            set
            {
                _cache["Author"] = value;
                RaisePropertyChanged("Author");
            }
        }
        public string AuthorEmail
        {
            get { return (string)_cache["AuthorEmail"]; }
            set
            {
                _cache["AuthorEmail"] = value;
                RaisePropertyChanged("AuthorEmail");
            }
        }
        public DateTime Date
        {
            get { return (DateTime)_cache["Date"]; }
            set
            {
                _cache["Date"] = value;
                RaisePropertyChanged("Date");
            }
        }
        public string Comment
        {
            get { return (string)_cache["Comment"]; }
            set
            {
                _cache["Comment"] = value;
                RaisePropertyChanged("Comment");
            }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected int _initializeCache()
        {
            this._cache.Values.Clear();

            this.Parent = this._commentSource.Parent;
            this.ObjectKey = this._commentSource.ObjectKey;
            this.Key = this._commentSource.Key;
            this.Author = this._commentSource.Author;
            this.AuthorEmail = this._commentSource.AuthorEmail;
            this.Date = this._commentSource.Date;
            this.Comment = this._commentSource.Comment;

            return -1;
        }
        protected int _saveFromCache()
        {
            this._commentSource.Parent = this.Parent;
            this._commentSource.ObjectKey = this.ObjectKey;
            this._commentSource.Author = this.Author;
            this._commentSource.AuthorEmail = this.AuthorEmail;
            this._commentSource.Date = this.Date;
            this._commentSource.Comment = this.Comment;

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
