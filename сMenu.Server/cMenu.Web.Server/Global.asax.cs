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

            #region USERS
            routes.MapRoute(
                "RouteUserGetInformationByKey",
                "Users/Key/{UserIdentity}/{Passhash}/{SessionID}/{Key}",
                new { controller = "User", action = "GetUserByKey", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, Key = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteUserGetInformationByID",
                "Users/ID/{UserIdentity}/{Passhash}/{SessionID}/{ID}",
                new { controller = "User", action = "GetUserByID", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, ID = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteUserLogin",
                "Users/Login/{UserIdentity}/{Password}/{TableID}",
                new { controller = "User", action = "Login", UserIdentity = UrlParameter.Optional, Password = UrlParameter.Optional, TableID = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteUserLogout",
                "Users/Logout/{UserIdentity}/{Passhash}/{SessionID}",
                new { controller = "User", action = "Logout", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteUserRegister",
                "Users/Register/{Login}/{Email}/{Mobile}/{Password}",
                new { controller = "User", action = "Register", Login = UrlParameter.Optional, Email = UrlParameter.Optional, Mobile = UrlParameter.Optional, Password = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteUserEdit",
                "Users/Edit/{UserIdentity}/{Passhash}/{SessionID}/{JSON}",
                new { controller = "User", action = "Edit", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, JSON = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteCheckLogin",
                "Users/Check/Login/{Login}",
                new { controller = "User", action = "CheckLoginExistence", Login = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteCheckEmail",
                "Users/Check/Email/{Email}",
                new { controller = "User", action = "CheckEmailExistence", Email = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteCheckPhone",
                "Users/Check/Phone/{Phone}",
                new { controller = "User", action = "CheckEmailExistence", Phone = UrlParameter.Optional }
            );
            #endregion

            #region ORGANIZATIONS
            routes.MapRoute(
                "RouteOrganizationGetInformationByKey",
                "Organization/Key/{UserIdentity}/{Passhash}/{SessionID}/{Key}",
                new { controller = "Organization", action = "GetOrganizationByKey", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, Key = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteOrganizationGetInformationByID",
                "Organization/ID/{UserIdentity}/{Passhash}/{SessionID}/{ID}",
                new { controller = "Organization", action = "GetOrganizationByID", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, ID = UrlParameter.Optional }
            );
            #endregion

            #region MENU
            routes.MapRoute(
                "RouteActiveMenuByKey",
                "Menu/Key/{UserIdentity}/{Passhash}/{SessionID}/{OrganizationKey}",
                new { controller = "Menu", action = "GetActiveMenuByKey", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, OrganizationKey = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteActiveMenuByID",
                "Menu/ID/{UserIdentity}/{Passhash}/{SessionID}/{OrganizationID}",
                new { controller = "Menu", action = "GetActiveMenuByID", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, OrganizationID = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteCategoryByKey",
                "Category/Key/{UserIdentity}/{Passhash}/{SessionID}/{CategoryKey}",
                new { controller = "Menu", action = "GetCategoryInformationByKey", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, CategoryKey = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteCategoryByID",
                "Category/ID/{UserIdentity}/{Passhash}/{SessionID}/{CategoryID}",
                new { controller = "Menu", action = "GetCategoryInformationByID", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, CategoryID = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteServiceByKey",
                "Service/Key/{UserIdentity}/{Passhash}/{SessionID}/{ServiceKey}",
                new { controller = "Menu", action = "GetServiceInformationByKey", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, ServiceKey = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteServiceByID",
                "Service/ID/{UserIdentity}/{Passhash}/{SessionID}/{ServiceID}",
                new { controller = "Menu", action = "GetServiceInformationByID", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, ServiceID = UrlParameter.Optional }
            );
            #endregion

            #region ORDERS
            routes.MapRoute(
                "RouteOrderByKey",
                "Order/Key/{UserIdentity}/{Passhash}/{SessionID}/{OrderKey}",
                new { controller = "Menu", action = "GetOrderInformationByKey", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, OrderKey = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteOrderByID",
                "Order/ID/{UserIdentity}/{Passhash}/{SessionID}/{OrderID}",
                new { controller = "Menu", action = "GetOrderInformationByID", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, OrderID = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteMakeOrder",
                "Order/Make/{UserIdentity}/{Passhash}/{SessionID}/{OrderJSON}",
                new { controller = "Menu", action = "MakeOrder", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional, OrderJSON = UrlParameter.Optional }
            );
            routes.MapRoute(
                "RouteCallOficiant",
                "Order/Oficiant/{UserIdentity}/{Passhash}/{SessionID}",
                new { controller = "Menu", action = "CallOficiant", UserIdentity = UrlParameter.Optional, Passhash = UrlParameter.Optional, SessionID = UrlParameter.Optional }
            );
            #endregion

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "User", action = "Index"}
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