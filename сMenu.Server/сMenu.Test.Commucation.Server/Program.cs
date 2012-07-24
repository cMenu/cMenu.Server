using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Test.Commucation.Server
{
    class Program
    {
        static void Main(string[] args)
        {

            cMenu.Communication.Server.CCommunicationServer Srv = new Communication.Server.CCommunicationServer();
            /// Srv.Configuration = new Communication.Server.Configuration.CCommunicationServerConfiguration() { Address = "http://localhost:8082/Nixa", ConnectionString = "", DatabaseType = Common.EnServerDB.EMsSQL };
            Srv.Initialize();
            Srv.Start();

            Console.WriteLine("Press ENTER to stop server");
            Console.ReadLine();
        }
    }
}
