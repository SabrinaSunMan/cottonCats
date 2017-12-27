//using MeowStore.Filters;
using StoreDB.Model;
using StoreDB.Model.Partials;
using StoreDB.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MeowStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[CustomAuthorize]
        public ActionResult Test()
        {
            //StoreDbContext db = new StoreDbContext();
            Repository<Students> Students_info = new Repository<Students>();

            //string GG = GuidNew().

            //取得資料
            Students a  = Students_info.GetAll().FirstOrDefault();
             
            ////新增一筆資料
            //Students cust = new Students();
            //cust.studentName = "Sabrina";
            //Students_info.Create(cust); 
            //////MeowDbContext db = new MeowDbContext();
            ////Car ttt = db.Cars.FirstOrDefault(); 
            ////return View(ttt);
            return View(a);
        }
    }
}