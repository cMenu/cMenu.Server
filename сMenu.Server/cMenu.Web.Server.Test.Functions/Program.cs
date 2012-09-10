using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Web.Server.Tablet.Controllers;

namespace cMenu.Web.Server.Test.Functions
{
    class Program
    {
        static void Main(string[] args)
        {
            UserController Ctrl = new UserController();
            var R = Ctrl.Index(-255);

        }
    }
}
