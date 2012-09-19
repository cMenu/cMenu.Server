using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Communication.Server.Notifications;

namespace cMenu.Communication
{
    public delegate object FunctionExecutedDelegateCaller(string Parameters, int Format);
    public delegate object NotificationRequestedDelegateCaller(CNotificationRequest Request);

    public delegate void OnFunctionExecutedDelegate(object Result);
    public delegate void OnNotificationRequestedDelegate(CNotificationResponse Result);
    public delegate void OnNotificationArrivedDelegate(CNotificationRequest Notification);
    public delegate void OnCommunicationServerStatusChangedDelegate(object Sender, EnCommunicationServerStatus Status);
}
