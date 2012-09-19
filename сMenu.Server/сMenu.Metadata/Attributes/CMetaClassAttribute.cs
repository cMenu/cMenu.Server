using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Metadata.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class CMetaClassAttribute : System.Attribute
    {
        #region PROTECTED FIELDS
        protected string _classID;        
        protected bool _visibleForMetadataManager = false;
        protected List<CMetaPropertyBindingAttribute> _bindings = new List<CMetaPropertyBindingAttribute>();
        #endregion

        #region PUBLIC FIELDS
        public string ClassID
        {
            get { return _classID; }
            set { _classID = value; }
        }        
        public bool VisibleForMetadataManager
        {
            get { return _visibleForMetadataManager; }
            set { _visibleForMetadataManager = value; }
        }
        public List<CMetaPropertyBindingAttribute> Bindings
        {
            get { return _bindings; }
            set { _bindings = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaClassAttribute()
        {

        }
        public CMetaClassAttribute(string ClassID)
        {
            this._classID = ClassID;
        }
        #endregion
    }
}
