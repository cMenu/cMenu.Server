using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.IO;
using cMenu.Communication;
using cMenu.Communication.Server;
using cMenu.Communication.Server.Notifications;
using cMenu.Communication.Server.Configuration;

namespace cMenu.UI.Notification.Common
{
    public class CNotificationServerEnvironment
    {
        protected static Hashtable _notifications = new Hashtable();
        protected static CNotificationServerConfiguration _configuration;
        protected static CCommunicationServer _server;

        public static Hashtable Notifications
        {
            get { return _notifications; }
            set { _notifications = value; }
        }
        public static CNotificationServerConfiguration Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }
        public static CCommunicationServer Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public static int sInitializeServer()
        {
            _server = new CCommunicationServer(Guid.Parse(_configuration.ID), _configuration.Name);
            _server.AllowWebRequests = false;
            _server.Configuration = new CCommunicationServerConfiguration()
            {
                Address = _configuration.Address,
                Description = _configuration.Description,
                ID = _configuration.ID,
                Name = _configuration.Name
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
