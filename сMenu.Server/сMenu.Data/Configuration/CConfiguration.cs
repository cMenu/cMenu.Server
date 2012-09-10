using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cMenu.Data.Configuration
{
    [Serializable]
    [XmlRoot("Configuration")]
    public class CConfiguration
    {
        #region PROTECTED FIELDS
        protected List<CConfigurationValue> _values = new List<CConfigurationValue>();
        #endregion

        #region PUBLIC FIELDS
        [XmlArrayItem("ConfigurationValue")]
        [XmlArray("ConfigurationValues")]
        public List<CConfigurationValue> Values
        {
            get { return _values; }
            set { _values = value; }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected string _getValue(string ID)
        {
            CConfigurationValue ConfigValue = CConfigurationValue.sFindValueByID(this._values, ID);
            return (ConfigValue == null ? "" : ConfigValue.Value);
        }
        protected int _setValue(string ID, string Value)
        {
            CConfigurationValue ConfigValue = CConfigurationValue.sFindValueByID(this._values, ID);
            if (ConfigValue != null)
                ConfigValue.Value = Value;
            else
            {
                ConfigValue = new CConfigurationValue() { ID = ID, Value = Value };
                this._values.Add(ConfigValue);
            }
            return -1;
        }
        #endregion

    }
}
