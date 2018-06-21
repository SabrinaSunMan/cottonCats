using BackMeow.Filters;
using BackMeow.Models;
using BackMeow.Models.ViewModel;
using BackMeow.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StoreDB;
using StoreDB.Enum;
using StoreDB.Repositories;
using StoreDB.Service;
using System;
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
        //private ReturnMsg _ReturnMsg; 

        public SystemController()
        {
            var unitOfWork = new EFUnitOfWork();
            _UserService = new AspNetUsersService(unitOfWork);
            _logSvc = new LoggingService(unitOfWork);
            _menuSide = new MenuSideListService(unitOfWork);
            // _ReturnMsg = new ReturnMsg();
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
        public ActionResult SystemRoles(SystemRolesViewModel ViewModel, int page = 1)
        {
            SystemRolesViewModel ResultViewModel;
            SystemRolesViewModel searchBlock = (SystemRolesViewModel)TempData["SystemRolesSelect"];
            if (searchBlock == null) /*空*/
            {
                ResultViewModel = _UserService.GetSystemRolesListViewModel(new SystemRolesListHeaderViewModel(), page);
            }
            else
            {
                ResultViewModel = _UserService.GetSystemRolesListViewModel(searchBlock.Header, page);
            }
            return View(ResultViewModel);
        }

        [HttpPost]
        public ActionResult SystemRoles(SystemRolesViewModel ViewModel)
        {
            SystemRolesViewModel ResultViewModel = _UserService.GetSystemRolesListViewModel(ViewModel.Header);
            TempData["SystemRolesSelect"] = ResultViewModel;
            return View(ResultViewModel);
        }
        #endregion
        #region 藉由ID取得後台使用者
        [HttpGet]
        public ActionResult SystemRolesMain(Actions ActionType, string guid, string selectEmail, string selectUserName, int pages = 1)
        { 
            TempData["Actions"] = ActionType;
            AspNetUsersDetailViewModel data = new AspNetUsersDetailViewModel();
            if (ActionType == Actions.Update)
            {
                data = _UserService.ReturnAspNetUsersDetail(ActionType, guid);
            }
            #region KeepSelectBlock
            pages = pages == 0 ? 1 : pages;
            TempData["SystemRolesSelect"] = new SystemRolesViewModel()
            {
                Header = new SystemRolesListHeaderViewModel()
                {
                    Email = selectEmail,
                    UserName = selectUserName
                },
                page = pages
            };
            #endregion
            return View(data);
        }

        //    object myFieldObjectB = SelectModel;
        //    List<string> filter = new List<string>();

        //        //foreach每一個欄位屬性及值,並進行判斷儲存
        //        foreach (PropertyInfo element in myFieldObjectB.GetType().GetProperties())
        //        {
        //            SystemRolesListHeaderViewModel tmp = new SystemRolesListHeaderViewModel();
        //    tmp = (SystemRolesListHeaderViewModel) element.GetValue(myFieldObjectB, null);
        //    //if (propertyValue)
        //    //{
        //    //    filter.Add(element.Name);
        //    //}
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateExclude]
        public async Task<ActionResult> SystemRolesMain(AspNetUsersDetailViewModel AspNetUsersModel, Actions actions)//, Actions actions)
                                                                                                                     //(FormCollection AspNetUsersModel,string guid) //, 
        {
            bool boolResult = true; // 取決於導向頁面
            string thisUserID; //使用者ID
            SystemRolesViewModel searchBlock = (SystemRolesViewModel)TempData["SystemRolesSelect"];
            if (ModelState.IsValid)
            {
                if (actions == Actions.Create)//Check for validation errors //&& !string.IsNullOrWhiteSpace(AspNetUsersModel.Id) 
                {
                    #region
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
                            //return RedirectToAction("Index", "Home");
                            TempData["message"] = EnumHelper.GetEnumDescription(DataAction.CreateScuess);
                            thisUserID = user.Id;
                        }
                        else { AddErrors(result); boolResult = false; }
                    }
                    else
                    {
                        CustomerIdentityError(EnumHelper.GetEnumDescription(DataAction.CreateFailReapet));
                        //_ReturnMsg.enumMsg = BackReturnMsg.Repeat;
                        //CustomerIdentityError(_ReturnMsg);
                        boolResult = false;
                    }
                    #endregion
                }
                else if (actions == Actions.Update)
                {
                    #region
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
                            TempData["message"] = EnumHelper.GetEnumDescription(DataAction.UpdateScuess);
                        }
                        else { AddErrors(result); boolResult = false; }
                    }else
                    {
                        CustomerIdentityError(EnumHelper.GetEnumDescription(DataAction.UpdateFail));
                    }
                    _UserService.AspNetUsersDetailViewModelUpdate(AspNetUsersModel);
                    //可以批次增加同時輸入很多個Table
                    _UserService.Save();
                    #endregion
                }
                //else /*什麼事情都不做*/
                //{
                //}
                return RedirectToAction("SystemRoles", new
                {
                    ViewModel = searchBlock,
                    pages = searchBlock.page
                });
            }
            #region KeepSelectBlock
            TempData["Actions"] = actions;
            TempData["SystemRolesSelect"] = searchBlock;
            #endregion
            //return RedirectToAction("SystemRolesMain", new
            //{
            //    ActionType = actions,
            //    guid = AspNetUsersModel.Id,
            //    selectEmail = searchBlock.Header.Email,
            //    selectUserName = searchBlock.Header.UserName,
            //    pages = searchBlock.page
            //});
            return View(AspNetUsersModel);
               
        }
        #endregion
        #endregion

        /// <summary>
        /// 客制化錯誤訊息
        /// </summary>
        /// <param name="ErrorMsg">The error MSG.</param>
        //private void CustomerIdentityError(ReturnMsg Msg)
        //{
        private void CustomerIdentityError(string Msg)
        {
            //string Result = !string.IsNullOrEmpty(Msg.StrMsg) ? Msg.StrMsg : Msg.enumMsg.ToString();
            AddErrors(IdentityResult.Failed(Msg));
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