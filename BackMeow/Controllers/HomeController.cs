using BackMeow.Filters;
using BackMeow.Service;
using Microsoft.AspNet.Identity;
using StoreDB.Model.ViewModel;
using StoreDB.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Security;

namespace BackMeow.Controllers
{
    [CustomAuthorize] 
    public class HomeController : Controller
    {
        private readonly AspNetUsersService _UserService;
        private ApplicationUserManager _userManager;
        private MenuSideListService _menuSide;

        public HomeController()
        {
            var unitOfWork = new EFUnitOfWork();
            _UserService = new AspNetUsersService(unitOfWork);
            _menuSide = new MenuSideListService(unitOfWork);
        }

        public ActionResult Index()
        {
            //FormsIdentity id = (FormsIdentity)User.Identity;
            //FormsAuthenticationTicket ticket = id.Ticket;

            //string cookiePath = ticket.CookiePath;
            //string expireDate = ticket.Expiration.ToString();
            //string expired = ticket.Expired.ToString();
            //string isPersistent = ticket.IsPersistent.ToString();
            //string issueDate = ticket.IssueDate.ToString();
            //string name = ticket.Name;
            //string userData = ticket.UserData;
            //string version = ticket.Version.ToString();

            int i = 0;
            //throw new NullReferenceException();
            //throw new Exception("Error!Test");
            //throw new HttpException(404, "页面未找到");
            return View();
        }

        /// <summary>
        /// 根據登入者身分確認並將可使用功能呈現至頁面上
        /// </summary>
        /// <returns></returns>
        public ActionResult SideBarContent()
        {
            List<MenuTreeRootStratumViewModel> SideViewModel = new List<MenuTreeRootStratumViewModel>();
            var userID = User.Identity.GetUserId();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null && userID !=null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                }
                SideViewModel = _menuSide.ReturnMenuSideViewModel(userID.ToString()).ToList();
            }  
            return View(SideViewModel);
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