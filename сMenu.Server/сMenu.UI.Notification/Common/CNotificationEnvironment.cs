using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iMenu.Common;
using iMenu.IO;
using iMenu.Communication;
using iMenu.Communication.Server;
using iMenu.Communication.Server.Notifications;
using iMenu.Communication.Server.Configuration;

namespace iMenu.UI.Notification.Common
{
    public class CNotificationEnvironment
    {
        protected static Hashtable _notifications = new Hashtable();
        protected static CNotificationApplicationConfiguration _configuration;
        protected static INotificationServer _notificationServer;
        protected static CCommunicationServer _server;

        public static Hashtable Notifications
        {
            get { return _notifications; }
            set { _notifications = value; }
        }
        public static CNotificationApplicationConfiguration Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }
        public static INotificationServer NotificationServer
        {
            get { return _notificationServer; }
            set { _notificationServer = value; }
        }
        public static CCommunicationServer Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public static int sInitializeServer()
        {
            _server = new CCommunicationServer(Guid.Parse(_configuration.ID), _configuration.Name);
            _server.Configuration = new CCommunicationServerConfiguration()
            {
                Address = _configuration.Address,
                ConnectionString = "",
                DatabaseType = EnServerDB.EUnknown,
                Description = _configuration.Description
            };
            _server.ServerInterfaceType = typeof(INotificationServer);
            _server.ServerPrimaryType = typeof(CUINotificationServer);
            _server.Initialize();

            return -1;
        }
        public static int sStartServer()
        {
            sInitializeServer();
            var R = _server.Start();
            return R;
        }
        public static int sStopServer()
        {
            return _server.Stop();
        }
        public static int sRestartServer()
        {
            sStopServer();
            sInitializeServer();
            sStartServer();
            return -1;
        }
    }
}
