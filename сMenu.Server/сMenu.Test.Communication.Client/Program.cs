using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Security;
using cMenu.Communication;
using cMenu.Communication.Server.Notifications;

namespace cMenu.Test.Communication.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, object> Params = new Dictionary<string, object>();
            Params.Add(CServerFunctionParams.CONST_FUNC_PARAM_LOGIN, "Administrator1");
            Params.Add(CServerFunctionParams.CONST_FUNC_PARAM_PASSWORD, "Password");
            Params.Add(CServerFunctionParams.CONST_FUNC_PARAM_SESSION_DEADLINE, new DateTime(3000, 1, 1));
            Params.Add(CServerFunctionParams.CONST_FUNC_PARAM_DEVICE_NAME, "New Device");
            Params.Add(CServerFunctionParams.CONST_FUNC_PARAM_DEVICE_MAC, "Device Mac");
            /// Params.Add(CServerFunctionParams.CONST_FUNC_PARAM_DEVICE_ID, "d0413280-5e3e-40cd-ae42-3b020029f690");
            /// Params.Add(CServerFunctionParams.CONST_FUNC_PARAM_AUTH_TOKEN, "2LxsyJOs|CcqjYkuA5AiPXsLxXeK4FK61E3Tx1qYN/LDILr7zCgI=");


            var RR = cMenu.Communication.Client.CCommunicationClient.sExecuteFunction("", "http://127.0.0.1/ApplicationServer", cMenu.Communication.EnFunctionResultFormat.EBinary, Params);
            /// var RR = cMenu.Communication.Client.CCommunicationClient.sRequestNotification(new CNotificationRequest() { Content = "1111", Date = DateTime.Now, Header = "111" }, "http://127.0.0.1/Notification_1");
            Console.ReadLine();
        }

        static void CCommunicationClient_OnNotificationRequested(CNotificationResponse Result)
        {
            var i = 0;
        }
    }
}
