using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using cMenu.IO;
using cMenu.Data.Configuration;
using cMenu.Common;

namespace cMenu.Communication.Server.Configuration
{
    [Serializable]
    [XmlRoot("Configuration")]
    public class CCommunicationServerConfiguration : CConfiguration
    {
        [XmlIgnore]
        public string ID
        {
            get { return this._getValue(CCommunicationCConsts.CONST_SERVER_ID); }
            set { this._setValue(CCommunicationCConsts.CONST_SERVER_ID, value); }
        }
        [XmlIgnore]
        public string Name
        {
            get { return this._getValue(CCommunicationCConsts.CONST_SERVER_NAME); }
            set { this._setValue(CCommunicationCConsts.CONST_SERVER_NAME, value); }
        }
        [XmlIgnore]
        public string Description
        {
            get { return this._getValue(CCommunicationCConsts.CONST_SERVER_DESC); }
            set { this._setValue(CCommunicationCConsts.CONST_SERVER_DESC, value); }
        }
        [XmlIgnore]
        public string Address
        {
            get { return this._getValue(CCommunicationCConsts.CONST_SERVER_ADDRESS); }
            set { this._setValue(CCommunicationCConsts.CONST_SERVER_ADDRESS, value); }
        }        
    }
}
