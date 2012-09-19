using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Metadata.Attributes
{
    [Serializable]
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class CMetaPropertyBindingAttribute : System.Attribute
    {
        #region PROTECTED FIELDS
        protected string _classID;
        protected string _fieldName = "";
        protected bool _visibleForMetadataManager = true;
        protected bool _allowBinding = true;        
        protected int _dictionaryKey = -1;
        protected int _dictionaryAttributeKey = -1;
        #endregion

        #region PUBLIC FIELDS
        public string ClassID
        {
            get { return _classID; }
            set { _classID = value; }
        }
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }
        public bool VisibleForMetadataManager
        {
            get { return _visibleForMetadataManager; }
            set { _visibleForMetadataManager = value; }
        }
        public bool AllowBinding
        {
            get { return _allowBinding; }
            set { _allowBinding = value; }
        }        
        public int DictionaryKey
        {
            get { return _dictionaryKey; }
            set { _dictionaryKey = value; }
        }
        public int DictionaryAttributeKey
        {
            get { return _dictionaryAttributeKey; }
            set { _dictionaryAttributeKey = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaPropertyBindingAttribute()
        {
        }
        public CMetaPropertyBindingAttribute(string ClassID, string FieldName)
        {
            this._classID = ClassID;
            this._fieldName = FieldName;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        #endregion

        #region STATIC FUNCTIONS
        #endregion

    }
}
