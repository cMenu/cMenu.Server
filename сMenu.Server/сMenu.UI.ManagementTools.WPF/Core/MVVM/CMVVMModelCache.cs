using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.UI.ManagementTools.WPF.Core.MVVM
{
    public class CMVVMModelCache
    {
        #region PROTECTED FIELDS
        protected Dictionary<string, object> _values = new Dictionary<string, object>();
        #endregion

        #region PUBLIC FIELDS
        public Dictionary<string, object> Values
        {
            get { return _values;}
            set { _values = value; }
        }
        public object this[string Name]
        {
            get { return _getValue(Name); }
            set { _setValue(Name, value); }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected object _getValue(string Name)
        {
            return (this._values.ContainsKey(Name) ? this._values[Name] : null);
        }
        protected int _setValue(string Name, object Value)
        {
            if (this._values.ContainsKey(Name))
                this._values[Name] = Value;
            else
                this._values.Add(Name, Value);

            return -1;
        }
        #endregion

        #region PUBLIC FIELDS
        public int Clear()
        {
            this._values.Clear();
            return -1;
        }
        #endregion
    }
}
