using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using iMenu.IO;
using iMenu.Data.Configuration;
using iMenu.Common;

namespace iMenu.Communication.Server.Configuration
{
    [Serializable]
    [XmlRoot("NotificationConfiguration")]
    public class CNotificationApplicationConfiguration : CConfiguration
    {
        #region PUBLIC FIELDS
        [XmlIgnore]
        public bool AutostartService
        {
            get { return bool.Parse(this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_AUTOSTART_SVC)); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_AUTOSTART_SVC, value.ToString()); }
        }
        [XmlIgnore]
        public bool AutostartApplication
        {
            get { return bool.Parse(this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_AUTOSTART_APP)); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_AUTOSTART_APP, value.ToString()); }
        }
        [XmlIgnore]
        public bool PlayAudio
        {
            get { return bool.Parse(this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_PLAY_AUDIO)); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_PLAY_AUDIO, value.ToString()); }
        }
        [XmlIgnore]
        public string AudioFile
        {
            get { return this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_AUDIO_FILE); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_AUDIO_FILE, value); }
        }
        [XmlIgnore]
        public string ID
        {
            get { return this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_ID); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_ID, value); }
        }
        [XmlIgnore]
        public string Name
        {
            get { return this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_NAME); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_NAME, value); }
        }
        [XmlIgnore]
        public string Description
        {
            get { return this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_DESC); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_DESC, value); }
        }        
        [XmlIgnore]
        public string Address
        {
            get { return this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_ADDRESS); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_ADDRESS, value); }
        }
        [XmlIgnore]
        public EnNotificationApplicationType ApplicationType
        {
            get { return (EnNotificationApplicationType)int.Parse(this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_TYPE)); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_TYPE, ((int)value).ToString()); }
        }
        #endregion
    }
}
