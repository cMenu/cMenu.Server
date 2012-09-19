using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;

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
            get 
            { 
                var Value = this._attributes[this._attributeID, Locale];
                return Value.PostProcessObjectValue<T>(null);
            }
            set 
            { 
                this._attributes[this._attributeID, Locale] = value; 
            }
        }
        public T DefaultValue
        {
            get 
            { 
                var Value = this._attributes[this._attributeID, -1];
                return Value.PostProcessObjectValue<T>(null);
            }
            set 
            { 
                this._attributes[this._attributeID, -1] = value; 
            }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaobjectAttributeLocalized()
        {
 
        }
        #endregion
    }
}
