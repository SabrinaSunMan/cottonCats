using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackMeow.Helper
{
    public static class CustomHtmlExtensions
    {
        public static MvcHtmlString RequiredLabel(this HtmlHelper helper,string labelString)
        {
            //var currentControllerName =
            //    (string)helper.ViewContext.RouteData.Values["controller"];

            //var currentActionName =
            //    (string)helper.ViewContext.RouteData.Values["action"];

            //return MvcHtmlString.Create($"<image src='https://unsplash.it/{w}/{h}/?random' />");

            //return "<p style='font-color:red'>*</p><label>" + labelString + "</label>";
            return MvcHtmlString.Create($"<p style='font-color:red'>*</p>");
        }
    }
}