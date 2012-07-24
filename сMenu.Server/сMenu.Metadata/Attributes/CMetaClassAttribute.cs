using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;

namespace cMenu.Metadata.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class CMetaClassAttribute : System.Attribute
    {
        #region PROTECTED FIELDS
        protected string _classID;
        protected List<CMetaPropertyBindingAttribute> _bindings = new List<CMetaPropertyBindingAttribute>();
        protected bool _visibleForMetadataManager = false;
        #endregion

        #region PUBLIC FIELDS
        public string ClassID
        {
            get { return _classID; }
            set { _classID = value; }
        }
        public List<CMetaPropertyBindingAttribute> Bindings
        {
            get { return _bindings; }
            set { _bindings = value; }
        }
        public bool VisibleForMetadataManager
        {
            get { return _visibleForMetadataManager; }
            set { _visibleForMetadataManager = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaClassAttribute()
        {

        }
        public CMetaClassAttribute(string ClassID, IDatabaseProvider Provider)
        {
            this._classID = ClassID;
            this._bindings = CMetaPropertyBindingAttribute.sGetMetaAttributesByClassID(Guid.Parse(ClassID), Provider);
        }
        #endregion
    }
}
