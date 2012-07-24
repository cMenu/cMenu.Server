using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Communication.Server
{
    public interface IServerFunction
    {
        int Initialize(Dictionary<string, object> Parameters);
        CFunctionResult Execute(Dictionary<string, object> Parameters);
    }
}
