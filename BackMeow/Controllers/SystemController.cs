using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackMeow.Controllers
{
    public class SystemController : Controller
    {
        
        // GET: System 
        //取得所有後台使用者清單
        public ActionResult SystemRolesList()
        {
            
            return View();
        }
    }
}