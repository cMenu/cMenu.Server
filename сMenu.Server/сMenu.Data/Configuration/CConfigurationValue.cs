using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cMenu.Data.Configuration
{
    [Serializable]
    public class CConfigurationValue
    {
        #region PROTECTED FIELDS
        protected string _id;
        protected string _value;
        #endregion

        #region PUBLIC FIELDS
        [XmlAttribute("ID")]
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        [XmlAttribute("Value")]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion

        #region STATIC FUNCTIONS
        public static CConfigurationValue sFindValueByID(List<CConfigurationValue> Values, string ID)
        {
            foreach (CConfigurationValue Value in Values)
                if (Value.ID == ID)
                    return Value;
            return null;
        }
        #endregion
    }
}
