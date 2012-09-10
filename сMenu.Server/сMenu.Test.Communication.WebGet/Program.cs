using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;

namespace cMenu.Test.Communication.WebGet
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        object EchoWithGet(string s);

        [OperationContract]
        [WebInvoke]
        object EchoWithPost(string s);
    }
    public class Service : IService
    {
        public object EchoWithGet(string s)
        {
            return 1;
        }

        public object EchoWithPost(string s)
        {
            return 1;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            WebServiceHost host = new WebServiceHost(typeof(Service), new Uri("http://localhost:8000/"));
            try
            {
                ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IService), new WebHttpBinding(), "");
                host.Open();
                using (ChannelFactory<IService> cf = new ChannelFactory<IService>(new WebHttpBinding(), "http://localhost:8000"))
                {
                    cf.Endpoint.Behaviors.Add(new WebHttpBehavior());

                    IService channel = cf.CreateChannel();

                    object s;

                    Console.WriteLine("Calling EchoWithGet via HTTP GET: ");
                    s = channel.EchoWithGet("Hello, world");
                    Console.WriteLine("   Output: {0}", s);

                    Console.WriteLine("");
                    Console.WriteLine("This can also be accomplished by navigating to");
                    Console.WriteLine("http://localhost:8000/EchoWithGet?s=Hello, world!");
                    Console.WriteLine("in a web browser while this sample is running.");

                    Console.WriteLine("");

                    Console.WriteLine("Calling EchoWithPost via HTTP POST: ");
                    s = channel.EchoWithPost("Hello, world");
                    Console.WriteLine("   Output: {0}", s);
                    Console.WriteLine("");
                }

                Console.WriteLine("Press <ENTER> to terminate");
                Console.ReadLine();

                host.Close();
            }
            catch (CommunicationException cex)
            {
                Console.WriteLine("An exception occurred: {0}", cex.Message);
                host.Abort();
            }
        }
    }
}
