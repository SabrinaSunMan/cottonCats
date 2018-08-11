using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BackMeow
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Elmah"); /*不讓外使用者輸入Elmah到達指定位置*/
            //routes.IgnoreRoute("elmah.axd");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional } /*正式使用*/
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } /*測試使用*/
            );
        }
    }
}