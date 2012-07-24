using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace cMenu.UI.ManagementTools.WPF.Core.MVVM
{
    public class CMVVMNotificationObject : INotifyPropertyChanged
    {
        #region EVENTS
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region PROTECTED FUNCTIONS
        protected void RaisePropertyChanged<T>(Expression<Func<T>> Action)
        {
            var propertyName = GetPropertyName(Action);
            this.RaisePropertyChanged(propertyName);
        }
        protected static string GetPropertyName<T>(Expression<Func<T>> Action)
        {
            var expression = (MemberExpression)Action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }
        protected void RaisePropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));                
            }
        }
        #endregion      
    }
}
