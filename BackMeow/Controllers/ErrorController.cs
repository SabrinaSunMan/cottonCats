using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackMeow.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult ErrorPage(string Msg)
        {
            ViewBag.ErrorMsg = Msg; 
            return View();
        }
    }
}