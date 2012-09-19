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
    [XmlRoot("NotificationConfiguration")]
    public class CNotificationServerConfiguration : CCommunicationServerConfiguration
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
        public EnNotificationApplicationType ApplicationType
        {
            get { return (EnNotificationApplicationType)int.Parse(this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_TYPE)); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_TYPE, ((int)value).ToString()); }
        }
        #endregion
    }
}
