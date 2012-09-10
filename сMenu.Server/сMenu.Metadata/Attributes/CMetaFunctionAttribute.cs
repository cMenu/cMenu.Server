using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Metadata.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class CMetaFunctionAttribute : System.Attribute
    {
        #region PROTECTED FIELDS
        protected string _functionID = "";
        protected bool _isInternal = true;
        protected bool _needAuthentication = true;
        #endregion

        #region PUBLIC FIELDS
        public string FunctionID
        {
            get { return _functionID; }
            set { _functionID = value; }
        }
        public bool IsInternal
        {
            get { return _isInternal; }
            set { _isInternal = value; }
        }
        public bool NeedAuthentication
        {
            get { return _needAuthentication; }
            set { _needAuthentication = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaFunctionAttribute()
        { 
        }
        public CMetaFunctionAttribute(string FunctionID, bool IsInternal = true, bool NeedAuthentication = true)
        {
            this._functionID = FunctionID;
            this._isInternal = IsInternal;
            this._needAuthentication = NeedAuthentication;
        }
        #endregion
    }
}
