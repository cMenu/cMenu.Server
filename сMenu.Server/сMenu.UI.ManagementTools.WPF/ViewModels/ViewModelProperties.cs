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
using cMenu.UI.ManagementTools.WPF.Models.Metaobjects;

namespace cMenu.UI.ManagementTools.WPF.ViewModels
{
    public class ViewModelProperties : CMVVMCachedNotificationObject, IDataErrorInfo
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
        protected EnEditorFormMode _mode = EnEditorFormMode.ECreate;
        protected ModelMetaobject _metaobjectModel = null;
        #endregion

        #region PUBLIC FIELDS
        public ModelMetaobject MetaobjectModel
        {
            get { return _metaobjectModel; }
            set 
            { 
                _metaobjectModel = value;
                RaisePropertyChanged("MetaobjectModel");
            }
        }
        public EnEditorFormMode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                RaisePropertyChanged("Mode");
            }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        #endregion

        #region COMMAND FUNCTIONS
        protected void _onNewGuidAction()
        {
            this.MetaobjectModel.ID = Guid.NewGuid();
        }
        protected bool _onNewGuidActionCondition()
        {
            return (this._mode != EnEditorFormMode.EView);
        }
        protected void _onSaveAction()
        {
            this._metaobjectModel.SaveValuesFromCache();

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
        #endregion

        #region COMMANDS
        public ICommand CmdNewGuid
        {
            get { return new CMVVMDelegateCommand(this._onNewGuidAction, this._onNewGuidActionCondition); }
        }
        public ICommand CmdSave
        {
            get { return new CMVVMDelegateCommand(this._onSaveAction, this._onSaveActionCondition); }
        }
        public ICommand CmdCancel
        {
            get { return new CMVVMDelegateCommand(this._onCancelAction, this._onCancelActionCondition); }
        }
        #endregion

        #region EVENTS
        public event OnDialogWindowsCloseQueryDelegate OnDialogWindowsCloseQuery;
        #endregion

        #region CONSTRUCTORS
        public ViewModelProperties()
        {
            this.MetaobjectModel = new ModelMetaobject();
            this.MetaobjectModel.Name = "111";
            this.MetaobjectModel.ID = Guid.NewGuid();
            this.MetaobjectModel.Description = "1112313123";
            this.MetaobjectModel.Class = Metaobjects.EnMetaobjectClass.ECategory;
            this.MetaobjectModel.ModificatonDate = DateTime.Now;
        }
        #endregion  
    }
}
