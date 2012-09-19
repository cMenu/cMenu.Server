using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

using cMenu.DB;
using cMenu.IO;

namespace cMenu.Metaobjects.Linq
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
                var Value = _attributes[_attributeID][Locale].AttributeValue;
                return Value.PostProcessObjectValue<T>(null);
            }
            set 
            { 
                _attributes[_attributeID][Locale].AttributeValue = value; 
            }
        }
        public T DefaultValue
        {
            get 
            { 
                var Value = _attributes[_attributeID][-1].AttributeValue;
                return Value.PostProcessObjectValue<T>(null);
            }
            set 
            {
                _attributes[_attributeID][-1].AttributeValue = value; 
            }
        }
        #endregion
    }
}
