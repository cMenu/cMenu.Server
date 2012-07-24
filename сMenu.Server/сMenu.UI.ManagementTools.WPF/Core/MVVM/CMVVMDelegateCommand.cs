using System;
using System.Windows.Input;

namespace cMenu.UI.ManagementTools.WPF.Core.MVVM
{
    public class CMVVMDelegateCommand : ICommand
    {
        #region PROTECTED FIELDS
        protected readonly Action _command;
        protected readonly Func<bool> _canExecute;
        #endregion

        #region PUBLIC FIELDS
        public event EventHandler CanExecuteChanged;
        #endregion

        #region CONSTRUCTORS
        public CMVVMDelegateCommand(Action Command, Func<bool> CanExecute = null)
        {
            if (Command == null)
                throw new ArgumentNullException();
            this._canExecute = CanExecute;
            this._command = Command;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public void Execute(object Parameter)
        {
            _command();
        }
        public bool CanExecute(object Parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute();
        }
        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
                this.CanExecuteChanged(this, EventArgs.Empty);
        }
        #endregion        

    }
}