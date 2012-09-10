using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace cMenu.Communication
{
    public enum EnCommunicationServerStatus
    {
        EEnabled = 1,
        EDisabled = 2,
        ESuspended = 3,
        EReady = 4
    }
    public enum EnFunctionResultType
    {
        ESuccess = 0,
        EError = 1
    }    
    public enum EnFunctionResultFormat
    {
        EBinary = 0,
        EXML = 1,
        EJSON = 2
    }
    public enum EnNotificationApplicationType
    {
        EUserInterfaceNotification = 0,
        EUserInterfaceInteraction = 1,
        ELocalNetworkPrinter = 2,
        EUnknown = 3
    }
}
