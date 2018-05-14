using BackMeow.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackMeow.Controllers
{
    public class StaticHtmlController : Controller
    {
        // GET: StaticHtml
        public ActionResult About()
        {

            return View(new AboutViewModel());
        }
    }
}