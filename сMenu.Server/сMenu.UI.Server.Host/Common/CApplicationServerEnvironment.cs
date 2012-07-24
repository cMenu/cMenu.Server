using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.Communication.Server;
using cMenu.Communication.Server.Configuration;

namespace cMenu.UI.Server.Host.Common
{
    public class CApplicationServerEnvironment
    {
        protected static CApplicationServerConfiguration _configuration;
        protected static CCommunicationServer _server;

        public static CApplicationServerConfiguration Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }
        public static CCommunicationServer CommunicationServer
        {
            get { return _server; }
            set { _server = value; }
        }

        public static int sInitializeServer()
        {
            if (_server != null)
                _server.Status = Communication.EnCommunicationServerStatus.EDisabled;

            _server = new CCommunicationServer(Guid.Parse(_configuration.ID), _configuration.Name);
            _server.AllowWebRequests = true;
            _server.Configuration = new CCommunicationServerConfiguration()
            {
                Address = _configuration.Address,                
                Description = _configuration.Description,
                ID = _configuration.ID,
                Name = _configuration.Name                
            };
            _server.Configuration = _configuration;
            _server.ServerInterfaceType = typeof(IApplicationServer);
            _server.ServerPrimaryType = typeof(CApplicationServer);
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
