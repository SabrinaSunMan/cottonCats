using System.Web.Mvc;

namespace BackMeow.Controllers
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

        public ActionResult BackTest()
        {
            //BackDbContext b = new BackDbContext();
            //Car abc = b.Cars.FirstOrDefault();
            //return View(abc);
            return View();
        }
    }
}