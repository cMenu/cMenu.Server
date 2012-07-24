using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using cMenu.Data.Configuration;

namespace cMenu.UI.ManagementTools.WPF.Core
{
    [Serializable]
    [XmlRoot("Configuration")]
    public class CMTApplicationConfiguration : CConfiguration
    {
        #region PROTECTED FIELDS
        protected List<CMTApplicationConnectionConfiguration> _connections = new List<CMTApplicationConnectionConfiguration>();
        #endregion

        #region PUBLIC FIELDS
        [XmlArray("Connections")]
        [XmlArrayItem("Connection")]
        public List<CMTApplicationConnectionConfiguration> Connections
        {
            get { return _connections; }
            set { _connections = value; }
        }
        #endregion
    }
}
