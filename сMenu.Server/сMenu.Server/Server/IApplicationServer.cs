using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;

namespace cMenu.Communication.Server
{
    [ServiceContract]
    public interface IApplicationServer
    {
        [OperationContract]
        [WebGet(UriTemplate = "/ExecuteFunctionFromWeb?Parameters={Parameters}")]
        Stream ExecuteFunctionFromWeb(string Parameters);

        [OperationContract]
        [WebGet(UriTemplate = "/ExecuteFunction?Parameters={Parameters}&Format={Format}")]
        object ExecuteFunction(string Parameters, int Format = 2);
    }
}
