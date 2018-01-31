using BackMeow.Filters;
using BackMeow.Models;
using BackMeow.Models.ViewModel;
using BackMeow.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StoreDB;
using StoreDB.Enum;
using StoreDB.Model.Partials;
using StoreDB.Model.ViewModel;
using StoreDB.Repositories;
using StoreDB.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BackMeow.Controllers
{
    public class SystemController : Controller
    {
        private Utility _utility = new Utility();
        private readonly AspNetUsersService _UserService;
        private readonly LoggingService _logSvc;
        private ApplicationUserManager _userManager;
        private MenuSideListService _menuSide;

        public SystemController()
        {
            var unitOfWork = new EFUnitOfWork();
            _UserService = new AspNetUsersService(unitOfWork);
            _logSvc = new LoggingService(unitOfWork);
            _menuSide = new MenuSideListService(unitOfWork);
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: System 
        #region 後台使用者管理

        #region 取得所有後台使用者清單
        [HttpGet]
            public ActionResult SystemRolesList()
            {
                SystemRolesListViewModel ResultViewModel = _UserService.GetSystemRolesListViewModel(new SystemRolesListHeaderViewModel(), 1);
                return View(ResultViewModel);
            }

            [HttpPost]
            public ActionResult SystemRolesList(SystemRolesListViewModel SystemRolesListViewModel,int page = 1)
            {
                SystemRolesListViewModel ResultViewModel = _UserService.GetSystemRolesListViewModel(SystemRolesListViewModel.Header, page);
                return View(ResultViewModel);
            }
            #endregion
        #region 藉由ID取得後台使用者
            [HttpGet]
            public ActionResult SystemRolesMain(Actions ActionType,string guid)
            {
                TempData["Actions"] = ActionType;
                if (ActionType == Actions.Update)
                {
                IEnumerable<MenuSideViewModel> tmp = _menuSide.ReturnMenuSideViewModel();

                    return View(_UserService.ReturnAspNetUsersDetail(ActionType, guid));
                }
                else
                {
                    return View(new AspNetUsersDetailViewModel());
                }  
            }
         
            [HttpPost]
            [ValidateAntiForgeryToken]
            [ModelStateExclude]
            public async Task<ActionResult> SystemRolesMain(AspNetUsersDetailViewModel AspNetUsersModel)//, Actions actions)
            //(FormCollection AspNetUsersModel,string guid) //, 
            {
            //Actions ans = _utility.StringToEnumActions(actions); 
            AspNetUsers oldData = new AspNetUsers();
            if (!string.IsNullOrWhiteSpace(AspNetUsersModel.Id))
            {
                oldData = _UserService.GetAspNetUsersById(AspNetUsersModel.Id);
                TryUpdateModel(oldData);
                //if (ModelState.IsValid && TryUpdateModel(oldData, "", AspNetUsersModel.AllKeys, ViewData["Exclude"] as string[]))
                //{ 
                //    _UserService.Update(oldData);
                //    string AA = "我要儲存";
                //    //db.SaveChanges();
                //    return RedirectToAction("Index");
                //}
            }else /*建立*/
            {
                if (ModelState.IsValid) //Check for validation errors
                {
                    var user = new ApplicationUser
                    {
                        UserName = AspNetUsersModel.UserName,
                        Email = AspNetUsersModel.Email,
                        Account = AspNetUsersModel.Account,
                        CreateTime = DateTime.Now
                    };
                    var result = await UserManager.CreateAsync(user, AspNetUsersModel.Password);
                    if (result.Succeeded)
                    {
                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // 傳送包含此連結的電子郵件
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "確認您的帳戶", "請按一下此連結確認您的帳戶 <a href=\"" + callbackUrl + "\">這裏</a>");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //AddErrors(result);
                    }
                }
                //_UserService.Add(oldData); 
                //_logSvc.Add("FirstName", "LastName","Email", Guid.NewGuid());
                //_UserService.Save();
            }
            return View(AspNetUsersModel);
                //return RedirectToAction("SystemRolesMain",new { ActionType = actions, guid = AspNetUsersModel.Id });
                
        }
        #endregion
        #endregion

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}