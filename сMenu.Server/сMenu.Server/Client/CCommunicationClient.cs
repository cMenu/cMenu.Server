using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Globalization;
using System.ServiceModel;
using System.Runtime.Remoting.Messaging;
using System.IO;

using cMenu.IO;
using cMenu.Communication.Server;
using cMenu.Communication.Server.Notifications;
using cMenu.Common.Base;
using cMenu.Globalization;

namespace cMenu.Communication.Client
{
    public class CCommunicationClient : CBaseEntity
    {
        #region PUBLIC EVENTS
        public static event OnFunctionExecutedDelegate OnFunctionExecuted;
        public static event OnNotificationRequestedDelegate OnNotificationRequested;
        #endregion        

        #region PROTECTED FUNCTIONS
        protected static string _getParametersJSON(Dictionary<string, object> Parameters)
        {
            string R = "";
            var Stream = Parameters.SerializeJSONStream();
            R = Stream.ToDataString();
            return R;
        }
        protected static void _functionsCallback(IAsyncResult Result)
        {
            AsyncResult R = (AsyncResult)Result;
            FunctionExecutedDelegateCaller Caller = (FunctionExecutedDelegateCaller)R.AsyncDelegate;
            var FunctionResult = Caller.EndInvoke(Result);

            if (OnFunctionExecuted != null)
                OnFunctionExecuted(FunctionResult);
        }
        protected static void _notificationsCallback(IAsyncResult Result)
        {
            AsyncResult R = (AsyncResult)Result;
            NotificationRequestedDelegateCaller Caller = (NotificationRequestedDelegateCaller)R.AsyncDelegate;
            var FunctionResult = Caller.EndInvoke(Result);

            if (OnNotificationRequested != null)
                OnNotificationRequested((CNotificationResponse)FunctionResult);
        }
        #endregion        

        #region APPLICATION SERVER FUNCTIONS
        public static object sExecuteFunction(string FunctionID, string ServerAddress, EnFunctionResultFormat ResultFormat, Dictionary<string, object> Parameters)
        {
            object R = null;

            Parameters.Add(CServerFunctionParams.CONST_FUNC_PARAM_FUNC_ID, FunctionID);

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxBufferPoolSize = Int32.MaxValue;
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            binding.MaxBufferSize = Int32.MaxValue;
            binding.TransferMode = TransferMode.Streamed;

            binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxDepth = 32;

            EndpointAddress address = new EndpointAddress(ServerAddress);
            ChannelFactory<IApplicationServer> factory = new ChannelFactory<IApplicationServer>(binding, address);
            IApplicationServer channel = factory.CreateChannel();

            string ParametersJSON = _getParametersJSON(Parameters);
            try
            { R = channel.ExecuteFunction(ParametersJSON, (int)ResultFormat); }
            catch (EndpointNotFoundException Ex)
            {
                R = new CNotificationResponse()
                {
                    Content = Ex.Message,
                    ErrorCode = -2,
                    Message = CGlobalizationHelper.sGetStringResource("ERROR_MSG_SERVICE_UNAVAILABLE", CultureInfo.CurrentCulture),
                    Name = "Notification Response"
                };
            }
            catch (CommunicationException Ex)
            {
                R = new CNotificationResponse()
                {
                    Content = Ex.Message,
                    ErrorCode = -2,
                    Message = CGlobalizationHelper.sGetStringResource("ERROR_MSG_COMMUNICATION_CLIENT_ERROR", CultureInfo.CurrentCulture),
                    Name = "Notification Response"
                };
            }

            return R;
        }
        public static IAsyncResult sExecuteFunctionAsync(string FunctionID, string ServerAddress, EnFunctionResultFormat ResultFormat, Dictionary<string, object> Parameters)
        {
            Parameters.Add(CServerFunctionParams.CONST_FUNC_PARAM_FUNC_ID, FunctionID);

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxBufferPoolSize = Int32.MaxValue;
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            binding.MaxBufferSize = Int32.MaxValue;
            binding.TransferMode = TransferMode.Streamed;

            binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxDepth = 32;

            EndpointAddress address = new EndpointAddress(ServerAddress);

            ChannelFactory<IApplicationServer> factory =
                new ChannelFactory<IApplicationServer>(binding, address);

            IApplicationServer channel = factory.CreateChannel();
            FunctionExecutedDelegateCaller Delegate = new FunctionExecutedDelegateCaller(channel.ExecuteFunction);
            string ParametersJSON = _getParametersJSON(Parameters);
            return Delegate.BeginInvoke(ParametersJSON, (int)ResultFormat, _functionsCallback, null);
        }
        #endregion        

        #region NOTIFICATION SERVER FUNCTIONS
        public static CNotificationResponse sRequestNotification(CNotificationRequest Request, string ServerAddress)
        {
            CNotificationResponse R = null;

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxBufferPoolSize = Int32.MaxValue;
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            binding.MaxBufferSize = Int32.MaxValue;
            binding.TransferMode = TransferMode.Streamed;

            binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxDepth = 32;

            EndpointAddress address = new EndpointAddress(ServerAddress);
            ChannelFactory<INotificationServer> factory = new ChannelFactory<INotificationServer>(binding, address);
            INotificationServer channel = factory.CreateChannel();

            try
            { R = channel.RequestNotification(Request); }
            catch (EndpointNotFoundException Ex)
            {
                R = new CNotificationResponse()
                {
                    Content = Ex.Message,
                    ErrorCode = -2,
                    Message = CGlobalizationHelper.sGetStringResource("ERROR_MSG_SERVICE_UNAVAILABLE", CultureInfo.CurrentCulture),
                    Name = "Notification Response"
                };
            }
            catch (CommunicationException Ex)
            {
                R = new CNotificationResponse()
                {
                    Content = Ex.Message,
                    ErrorCode = -2,
                    Message = CGlobalizationHelper.sGetStringResource("ERROR_MSG_COMMUNICATION_CLIENT_ERROR", CultureInfo.CurrentCulture),
                    Name = "Notification Response"
                };
            }


            return R;
        }
        public static IAsyncResult sRequestNotificationAsync(CNotificationRequest Request, string ServerAddress)
        {
            CNotificationResponse R = null;

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxBufferPoolSize = Int32.MaxValue;
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            binding.MaxBufferSize = Int32.MaxValue;
            binding.TransferMode = TransferMode.Streamed;

            binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxDepth = 32;

            EndpointAddress address = new EndpointAddress(ServerAddress);
            ChannelFactory<INotificationServer> factory = new ChannelFactory<INotificationServer>(binding, address);
            INotificationServer channel = factory.CreateChannel();
            NotificationRequestedDelegateCaller Delegate = new NotificationRequestedDelegateCaller(channel.RequestNotification);

            return Delegate.BeginInvoke(Request, _notificationsCallback, null);
        }
        #endregion        
    }
}
