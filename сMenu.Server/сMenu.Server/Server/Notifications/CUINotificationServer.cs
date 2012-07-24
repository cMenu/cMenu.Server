using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Globalization;

using cMenu.Common;
using cMenu.Common.ErrorHandling;
using cMenu.Globalization;

namespace cMenu.Communication.Server.Notifications
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] 
    public class CUINotificationServer : INotificationServer
    {
        public static  event OnNotificationArrivedDelegate OnNotificationArrived;

        public CNotificationResponse RequestNotification(CNotificationRequest Request)
        {
            CNotificationResponse R = new CNotificationResponse();

            if (OnNotificationArrived == null)
            {
                R.ID = Guid.NewGuid();
                R.ErrorCode = -2;
                R.Name = "Notification Response";
                R.Message = CGlobalizationHelper.sGetStringResource("ERROR_MSG_UNABLE_SHOW_NOTIFICATION", CultureInfo.CurrentCulture);
                R.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_UNABLE_SHOW_NOTIFICATION", CultureInfo.CurrentCulture);

                return R;
            }

            OnNotificationArrived(Request);
            R.ID = Guid.NewGuid();
            R.ErrorCode = -1;
            R.Name = "Notification Response";
            R.Message = CGlobalizationHelper.sGetStringResource("ERROR_MSG_SHOW_NOTIFICATION", CultureInfo.CurrentCulture);
            R.Content = CGlobalizationHelper.sGetStringResource("ERROR_MSG_SHOW_NOTIFICATION", CultureInfo.CurrentCulture);

            return R;
        }
    }
}
