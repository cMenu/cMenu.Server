using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Metaobjects
{
    [Serializable]
    public class CMetaobjectAttributeLocalized<T>
    {
        #region PROTECTED FIELDS
        protected string _attributeID = "";
        protected CMetaobjectAttributes _attributes = new CMetaobjectAttributes();
        #endregion

        #region PUBLIC FIELDS
        public CMetaobjectAttributes Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }
        public string AttributeID
        {
            get { return _attributeID; }
            set { _attributeID = value; }
        }
        public T this[int Locale]
        {
            get { return (T)this._attributes[this._attributeID, Locale]; }
            set { this._attributes[this._attributeID, Locale] = value; }
        }
        public T DefaultValue
        {
            get { return (T)this._attributes[this._attributeID, -1]; }
            set { this._attributes[this._attributeID, -1] = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaobjectAttributeLocalized()
        {
 
        }
        #endregion
    }
}
