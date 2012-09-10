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
    public class CApplicationServerConfiguration : CCommunicationServerConfiguration
    {
        #region PROTECTED FIELDS
        protected List<CServerFunctionConfiguration> _functions = new List<CServerFunctionConfiguration>();
        protected List<CNotificationConfiguration> _notifications = new List<CNotificationConfiguration>();
        #endregion

        #region PUBLIC FIELDS
        [XmlIgnore]
        public string ConnectionString
        {
            get { return this._getValue(CCommunicationCConsts.CONST_SERVER_CONNECTION_STRING); }
            set { this._setValue(CCommunicationCConsts.CONST_SERVER_CONNECTION_STRING, value); }
        }
        [XmlIgnore]
        public EnServerDB DatabaseType
        {
            get
            {
                var DBType = this._getValue(CCommunicationCConsts.CONST_SERVER_DB_TYPE);
                EnServerDB OutTemp = EnServerDB.EUnknown;
                return (EnServerDB.TryParse(DBType, out OutTemp) ? OutTemp : EnServerDB.EUnknown);
            }
            set
            {
                this._setValue(CCommunicationCConsts.CONST_SERVER_DB_TYPE, value.ToString());
            }
        }
        [XmlIgnore]
        public string FunctionsPath
        {
            get { return this._getValue(CCommunicationCConsts.CONST_SERVER_APP_SERVER_PATH); }
            set { this._setValue(CCommunicationCConsts.CONST_SERVER_APP_SERVER_PATH, value); }
        }
        [XmlArray("Functions")]
        [XmlArrayItem("FunctionConfiguration")]
        public List<CServerFunctionConfiguration> Functions
        {
            get { return _functions; }
            set { _functions = value; }
        }
        [XmlArray("Notifications")]
        [XmlArrayItem("NotificationConfiguration")]
        public List<CNotificationConfiguration> Notifications
        {
            get { return _notifications; }
            set { _notifications = value; }
        }
        #endregion
    }
}
