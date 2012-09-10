using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using cMenu.Web.Server.Tablet.Common;

namespace cMenu.Web.Server.Tablet
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801@

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void InitializeEnvironment()
        {
            CServerEnvironment.Initialize();
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "RouteUserGetInformation",
                "Users/{Key}",
                new { controller = "User", action = "GetUser", Key = UrlParameter.Optional }
            );
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{Key}",
                new { controller = "Home", action = "Index", Key = UrlParameter.Optional }
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            InitializeEnvironment();
        }
    }
}