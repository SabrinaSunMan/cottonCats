using BackMeow.Service;
using Microsoft.AspNet.Identity.Owin;
using StoreDB.Model.Partials;
using StoreDB.Repositories;
using System;
using System.Web;
using System.Web.Mvc;

namespace BackMeow.Filters
{
    /// <summary>
    /// 客製化_驗證該頁面是否可以被訪問
    /// </summary>
    /// <seealso cref="System.Web.Mvc.AuthorizeAttribute" />
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly AspNetUsersService _UserService;
        private readonly MenuSideListService _MenuService;
        private ApplicationSignInManager _signInManager;

        public CustomAuthorizeAttribute()
        {
            var unitOfWork = new EFUnitOfWork();
            _UserService = new AspNetUsersService(unitOfWork);
            _MenuService = new MenuSideListService(unitOfWork);
        }

        public object Request { get; private set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string NotifyUrl = "/Account/Login";
            //if (AuthorizeCore(filterContext.HttpContext))
            //{
            //    HttpCachePolicyBase cachePolicy =
            //        filterContext.HttpContext.Response.Cache;
            //    cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            //    //cachePolicy.AddValidationCallback(CacheValidateHandler, null);
            //}

            /// This code added to support custom Unauthorized pages.
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (NotifyUrl != null)
                    filterContext.Result = new RedirectResult(NotifyUrl + "?Msg=很抱歉，您未使用該頁面權限");
                else
                    // Redirect to Login page.
                    HandleUnauthorizedRequest(filterContext);
            }
            /// End of additional code
            else
            {
                // Redirect to Login page.
                HandleUnauthorizedRequest(filterContext);
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            String[] users = Users.Split(',');//取得輸入user清單
            String[] roles = Roles.Split(',');//取得輸入role清單
            if (!httpContext.User.Identity.IsAuthenticated)//判斷是否已驗證
                return false;

            var rd = httpContext.Request.RequestContext.RouteData;
            string Action = rd.GetRequiredString("action");
            string Controller = rd.GetRequiredString("controller");
            //string currentArea = rd.Values["area"] as string;

            _signInManager = httpContext.GetOwinContext().Get<ApplicationSignInManager>();
            //ApplicationSignInManager UserManager = new ApplicationSignInManager(_signInManager);
            _UserService.UserName = httpContext.User.Identity.Name.ToString(); //登入的使用者帳號
            AspNetUsers AspNetusers = _UserService.GetAspNetUserBySelectPramters();

            //if (_MenuService.CheckRequestPage(AspNetusers.Id, Controller))
            //{
            //return false;
            return true;
            //}
            //else return false;
        }
    }
}