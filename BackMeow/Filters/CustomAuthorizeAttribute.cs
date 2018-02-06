
using BackMeow.Service;
using Microsoft.AspNet.Identity.Owin;
using StoreDB.Model.Partials;
using StoreDB.Repositories;
using System;
using System.Web;
using System.Web.Mvc;

namespace BackMeow.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly AspNetUsersService _UserService;
        private ApplicationSignInManager _signInManager;

        public CustomAuthorizeAttribute()
        {
            var unitOfWork = new EFUnitOfWork();
            _UserService = new AspNetUsersService(unitOfWork);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext"); 
            //var test = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

            String[] users = Users.Split(',');//取得輸入user清單
            String[] roles = Roles.Split(',');//取得輸入role清單
            if (!httpContext.User.Identity.IsAuthenticated)//判斷是否已驗證
                return false;

            var rd = httpContext.Request.RequestContext.RouteData;
            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            //string currentArea = rd.Values["area"] as string;

            _signInManager = httpContext.GetOwinContext().Get<ApplicationSignInManager>();
            //ApplicationSignInManager UserManager = new ApplicationSignInManager(_signInManager);
            string Username = httpContext.User.Identity.Name.ToString(); //登入的使用者帳號
            AspNetUsers AspNetusers = _UserService.GetAspNetUserByName(Username);
            
            if (roles.Length != 0)
            {
                //BookInfoDBContext _DBContex = new BookInfoDBContext();
                //SkillTreeHomeworkEntities db = _DBContex.LocalDBConnection();
                //String account = httpContext.User.Identity.Name.ToString(); //登入的使用者帳號
                //bool Isright = false;//角色是否正確
                ////var q = from tbl in db.USERPROFILE
                ////        where tbl.ACCOUNT == account
                ////        select new
                ////        {
                ////            tbl.ROLE
                ////        };
                ////foreach (String inputval in roles)//循環比對角色資料
                ////{
                ////    if (q.First().ROLE.ToString() == inputval)
                ////        return true;
                ////    else
                ////        Isright = false;
                ////}
                //return Isright;
            }
            return true; 
        } 
    } 
}