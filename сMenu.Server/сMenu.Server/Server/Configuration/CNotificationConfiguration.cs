using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using cMenu.Data.Configuration;

namespace cMenu.Communication.Server.Configuration
{
    [Serializable]
    [XmlRoot("NotificationConfiguration")]
    public class CNotificationConfiguration : CConfiguration
    {
        [XmlIgnore]
        public bool Enabled
        {
            get
            {
                try
                {
                    var R = bool.Parse(this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_ENABLED));
                    return R;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set
            {
                this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_ENABLED, value.ToString());
            }
        }
        [XmlIgnore]
        public Guid ID
        {
            get
            {
                try
                {
                    var R = Guid.Parse(this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_ID));
                    return R;
                }
                catch (Exception ex)
                {
                    return Guid.Empty;
                }
            }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_ID, value.ToString()); }
        }
        [XmlIgnore]
        public string Name
        {
            get { return this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_NAME); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_NAME, value); }
        }
        [XmlIgnore]
        public string Address
        {
            get { return this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_ADDRESS); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_ADDRESS, value); }
        }
        [XmlIgnore]
        public EnNotificationApplicationType Type
        {
            get { return (EnNotificationApplicationType)int.Parse(this._getValue(CCommunicationCConsts.CONST_NOTIFICATION_TYPE)); }
            set { this._setValue(CCommunicationCConsts.CONST_NOTIFICATION_TYPE, value.ToString()); }
        }

        public static CNotificationConfiguration sFindByID(List<CNotificationConfiguration> Notifications, Guid ID)
        {
            if (Notifications == null)
                return null;

            foreach (CNotificationConfiguration Notification in Notifications)
                if (Notification.ID == ID)
                    return Notification;
            return null;
        }
    }
}
