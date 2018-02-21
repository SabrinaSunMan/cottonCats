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
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BackMeow.Controllers
{
    [Authorize]
    public class SystemController : Controller
    {
        private Utility _utility = new Utility();
        private readonly AspNetUsersService _UserService;
        private readonly LoggingService _logSvc;
        private readonly string _actionName;
        private readonly string _controllerName;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private MenuSideListService _menuSide;
        private ReturnMsg _ReturnMsg;
         

        public SystemController()
        {
            var unitOfWork = new EFUnitOfWork();
            _UserService = new AspNetUsersService(unitOfWork);
            _logSvc = new LoggingService(unitOfWork);
            _menuSide = new MenuSideListService(unitOfWork);
            _ReturnMsg = new ReturnMsg();
            _actionName = "";// this.ControllerContext.RouteData.Values["action"].ToString();
            _controllerName = "";// this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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
        public ActionResult SystemRoles()
        {
            SystemRolesListViewModel ResultViewModel = _UserService.GetSystemRolesListViewModel(new SystemRolesListHeaderViewModel(), 1);
            return View(ResultViewModel);
        }

        [HttpPost]
        public ActionResult SystemRoles(SystemRolesListViewModel SystemRolesListViewModel, int page = 1)
        {
            SystemRolesListViewModel ResultViewModel = _UserService.GetSystemRolesListViewModel(SystemRolesListViewModel.Header, page);
            return View(ResultViewModel);
        }
        #endregion
        #region 藉由ID取得後台使用者
        [HttpGet]
        public ActionResult SystemRolesMain(Actions ActionType, string guid)
        {
            TempData["Actions"] = ActionType;
            if (ActionType == Actions.Update)
            {
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
        public async Task<ActionResult> SystemRolesMain(AspNetUsersDetailViewModel AspNetUsersModel, Actions actions)//, Actions actions)
                                                                                                    //(FormCollection AspNetUsersModel,string guid) //, 
        {
            if(actions == Actions.Create && !string.IsNullOrWhiteSpace(AspNetUsersModel.Id) &&
                ModelState.IsValid )//Check for validation errors
            {
                var user = new ApplicationUser
                {
                    UserName = AspNetUsersModel.UserName,
                    Email = AspNetUsersModel.Email,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Id = Guid.NewGuid().ToString().ToUpper()
                };
                _UserService.UserName = user.UserName;
                _UserService.UserEmail = user.Email;

                if (_UserService.GetAspNetUserBySelectPramters() == null)
                {
                    var result = await UserManager.CreateAsync(user, AspNetUsersModel.Password);
                    if (result.Succeeded)
                    {
                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // 傳送包含此連結的電子郵件
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(user.Id, "確認您的帳戶", "請按一下此連結確認您的帳戶 <a href=\"" + callbackUrl + "\">這裏</a>");
                        return RedirectToAction("Index", "Home");
                    }
                    else { AddErrors(result); }
                }
                else
                {
                    _ReturnMsg.enumMsg = BackReturnMsg.Repeat;
                    CustomerIdentityError(_ReturnMsg);
                }
            } else if(actions == Actions.Update) 
            {
                if (!string.IsNullOrEmpty(AspNetUsersModel.Old_Password))
                { // 變更密碼
                    var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), AspNetUsersModel.Old_Password, AspNetUsersModel.Password);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        }
                    }
                    else AddErrors(result);
                }
                _UserService.AspNetUsersDetailViewModelUpdate(AspNetUsersModel);
                _UserService.Save();
            }
            else /*什麼事情都不做*/
            {

            } 
            //if (!string.IsNullOrWhiteSpace(AspNetUsersModel.Id))
            //{
            //    if (!string.IsNullOrEmpty(AspNetUsersModel.Old_Password))
            //    { // 變更密碼
            //        var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), AspNetUsersModel.Old_Password, AspNetUsersModel.Password);
            //        if (result.Succeeded)
            //        {
            //            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //            if (user != null)
            //            {
            //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            //            }
            //        }
            //        else AddErrors(result);
            //    }
            //    _UserService.AspNetUsersDetailViewModelUpdate(AspNetUsersModel); 
            //    _UserService.Save(); 
            //}
            //else /*建立使用者*/
            //{
            //    if (ModelState.IsValid) 
            //    {
            //        var user = new ApplicationUser
            //        {
            //            UserName = AspNetUsersModel.UserName,
            //            Email = AspNetUsersModel.Email,
            //            CreateTime = DateTime.Now,
            //            UpdateTime = DateTime.Now,
            //            Id = Guid.NewGuid().ToString().ToUpper()
            //        };
            //        _UserService.UserName = user.UserName;
            //        _UserService.UserEmail = user.Email;

            //        if (_UserService.GetAspNetUserBySelectPramters() == null)
            //        {
            //            var result = await UserManager.CreateAsync(user, AspNetUsersModel.Password);
            //            if (result.Succeeded)
            //            {
            //                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
            //                // 傳送包含此連結的電子郵件
            //                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            //                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            //                await UserManager.SendEmailAsync(user.Id, "確認您的帳戶", "請按一下此連結確認您的帳戶 <a href=\"" + callbackUrl + "\">這裏</a>");
            //                return RedirectToAction("Index", "Home");
            //            }
            //            else
            //            {
            //                AddErrors(result);
            //            }
            //        }
            //        else
            //        {
            //            _ReturnMsg.enumMsg = BackReturnMsg.Repeat;
            //            CustomerIdentityError(_ReturnMsg);
            //        }
            //    } 
            //}

            //return View(_UserService.GetAspNetUsersById(AspNetUsersModel.Id));
            return RedirectToAction("SystemRolesMain",new { ActionType = actions, guid = AspNetUsersModel.Id });
        }
        #endregion
        #endregion

        /// <summary>
        /// 客制化錯誤訊息
        /// </summary>
        /// <param name="ErrorMsg">The error MSG.</param>
        private void CustomerIdentityError(ReturnMsg Msg)
        { 
            string Result = !string.IsNullOrEmpty(Msg.StrMsg) ? Msg.StrMsg : Msg.enumMsg.ToString();
            AddErrors(IdentityResult.Failed(Result));
        }

        /// <summary>
        /// Identity 回的結果
        /// </summary>
        /// <param name="result">The result.</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}