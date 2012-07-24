using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace cMenu.Communication.Server.Notifications
{
    [ServiceContract]
    public interface INotificationServer
    {
        [OperationContract]
        CNotificationResponse RequestNotification(CNotificationRequest Request);
    }
}
