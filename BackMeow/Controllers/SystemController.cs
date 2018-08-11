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
using System.Collections.Generic;
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
        private readonly MemberService _MemberService;
        private readonly LoggingService _logSvc;
        private string _signInManagerNames;
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
            _MemberService = new MemberService(unitOfWork);
            _logSvc = new LoggingService(unitOfWork);
            _menuSide = new MenuSideListService(unitOfWork);
            // _ReturnMsg = new ReturnMsg();
            _actionName = "";// this.ControllerContext.RouteData.Values["action"].ToString();
            _controllerName = "";// this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        public string SignInManagerName
        {
            get
            {
                return _signInManagerNames ?? HttpContext.User.Identity.Name.ToString();
            }
            private set
            {
                _signInManagerNames = value;
            }
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

        #region List - 取得所有後台使用者清單

        /// <summary>
        /// Systems the roles.
        /// </summary>
        /// <param name="ViewModel">The view model.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Systems the roles.
        /// </summary>
        /// <param name="ViewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SystemRoles(SystemRolesViewModel ViewModel)
        {
            SystemRolesViewModel ResultViewModel = _UserService.GetSystemRolesListViewModel(ViewModel.Header);
            TempData["SystemRolesSelect"] = ResultViewModel;
            return View(ResultViewModel);
        }

        #endregion List - 取得所有後台使用者清單

        #region Main - 明細內容動作

        /// <summary>
        /// 藉由ID取得後台使用者
        /// </summary>
        /// <param name="ActionType">Type of the action.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="selectEmail">The select email.</param>
        /// <param name="selectUserName">Name of the select user.</param>
        /// <param name="pages">The pages.</param>
        /// <returns></returns>
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
            #endregion KeepSelectBlock
            return View(data);
        }

        /// <summary>
        /// Systems the roles main.
        /// </summary>
        /// <param name="AspNetUsersModel">The ASP net users model.</param>
        /// <param name="actions">The actions.</param>
        /// <returns></returns>
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
                var user = new ApplicationUser
                {
                    UserName = AspNetUsersModel.UserName,
                    Email = AspNetUsersModel.Email,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Status = true
                };

                if (actions == Actions.Create)
                {
                    #region
                    user.Id = Guid.NewGuid().ToString().ToUpper();
                    _UserService.UserName = user.UserName;
                    _UserService.UserEmail = user.Email;

                    if (_UserService.GetAspNetUserBySelectPramters() == null)
                    {
                        var result = await UserManager.CreateAsync(user, AspNetUsersModel.Password);
                        if (result.Succeeded)
                        {
                            //-------- 待補：建立使用者應要把權限也寫入!
                            _UserService.CreateUserMenuTree(user.Id);
                            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                            // 傳送包含此連結的電子郵件
                            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                            await UserManager.SendEmailAsync(user.Id, "確認您的帳戶", "請按一下此連結確認您的帳戶 <a href=\"" + callbackUrl + "\">這裏</a>");
                            TempData["message"] = EnumHelper.GetEnumDescription(DataAction.CreateScuess);
                            thisUserID = user.Id;
                        }
                        else
                        {
                            AddErrors(result);
                            boolResult = false;
                        }
                    }
                    else
                    {
                        CustomerIdentityError(EnumHelper.GetEnumDescription(DataAction.CreateFailReapet));
                        boolResult = false;
                    }
                    #endregion Main - 明細內容動作
                }
                else if (actions == Actions.Update)
                {
                    #region
                    if (!string.IsNullOrEmpty(AspNetUsersModel.Old_Password) && !string.IsNullOrEmpty(AspNetUsersModel.Password))
                    {
                        bool passwordIsEdit = false;
                        try
                        {
                            var checkPassword = UserManager.PasswordHasher.VerifyHashedPassword(AspNetUsersModel.Password, AspNetUsersModel.Old_Password);
                            if (checkPassword != PasswordVerificationResult.Success)
                            {
                                passwordIsEdit = true;
                            }
                        }
                        catch
                        { passwordIsEdit = true; }
                        if (passwordIsEdit)
                        {
                            user.Id = AspNetUsersModel.Id;
                            // 變更密碼
                            var result = await UserManager.ChangePasswordAsync(user.Id, AspNetUsersModel.Old_Password, AspNetUsersModel.Password);
                            if (result.Succeeded)
                            {
                                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            }
                            else { AddErrors(result); boolResult = false; }
                        }
                        else
                        {
                            CustomerIdentityError(EnumHelper.GetEnumDescription(DataAction.UpdateFail));
                        }
                    }

                    _UserService.AspNetUsersDetailViewModelUpdate(AspNetUsersModel);
                    //可以批次增加同時輸入很多個Table
                    _UserService.Save();
                    TempData["message"] = EnumHelper.GetEnumDescription(DataAction.UpdateScuess);
                    #endregion 後台使用者管理
                }
                else
                {
                    string ErrorMsg = "";
                    foreach (var items in ModelState.Values)
                    {
                        foreach (ModelError Erroritem in items.Errors)
                        {
                            ErrorMsg += Erroritem.ErrorMessage + " ";
                        }
                    }
                    CustomerIdentityError(ErrorMsg);
                    boolResult = false;
                }

                if (boolResult)
                {
                    return RedirectToAction("SystemRoles", new
                    {
                        ViewModel = searchBlock,
                        pages = searchBlock.page
                    });
                }
            }
            #region KeepSelectBlock
            TempData["Actions"] = actions;
            TempData["SystemRolesSelect"] = searchBlock;
            #endregion KeepSelectBlock
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

        #region 會員管理
        #region List - 取得所有會員清單

        /// <summary>
        /// Members the specified view model.
        /// </summary>
        /// <param name="ViewModel">The view model.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Member(MemberViewModel ViewModel, int page = 1)
        {
            MemberViewModel ResultViewModel;
            MemberViewModel searchBlock = (MemberViewModel)TempData["MemeberSelect"];
            if (searchBlock == null) /*空*/
            {
                ResultViewModel = _MemberService.GetMemberListViewModel(new MemberListHeaderViewModel(), page);
            }
            else
            {
                ResultViewModel = _MemberService.GetMemberListViewModel(searchBlock.Header, page);
            }
            return View(ResultViewModel);
        }

        /// <summary>
        /// Members the specified view model.
        /// </summary>
        /// <param name="ViewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Member(MemberViewModel ViewModel)
        {
            MemberViewModel ResultViewModel = _MemberService.GetMemberListViewModel(ViewModel.Header);
            TempData["MemeberSelect"] = ResultViewModel;
            return View(ResultViewModel);
        }

        #endregion

        #region Main - 明細內容動作

        /// <summary>
        /// 藉由ID取得後台使用者
        /// </summary>
        /// <param name="ActionType">Type of the action.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="selectName">Name of the select.</param>
        /// <param name="selectCreateTime">The select create time.</param>
        /// <param name="selectPhoneNumber">The select phone number.</param>
        /// <param name="pages">The pages.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MemberMain(Actions ActionType, string guid, string selectName, string selectCreateTime, string selectPhoneNumber, int pages = 1)
        {
            TempData["Actions"] = ActionType;
            MemberDetailViewModel data = new MemberDetailViewModel();
            if (ActionType == Actions.Update)
            {
                data = _MemberService.ReturnMemberDetail(ActionType, guid);
            }
            #region KeepSelectBlock
            pages = pages == 0 ? 1 : pages;
            TempData["MemeberSelect"] = new MemberViewModel()
            {
                Header = new MemberListHeaderViewModel()
                {
                    CreateTime = selectCreateTime,
                    Name = selectName,
                    PhoneNumber = Convert.ToInt16(selectPhoneNumber)
                },
                page = pages
            };
            #endregion KeepSelectBlock
            return View(data);
        }

        /// <summary>
        /// Members the main.
        /// </summary>
        /// <param name="actions">The actions.</param>
        /// <param name="memberViewModel">The member view model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MemberMain(Actions actions, MemberDetailViewModel memberViewModel)
        {
            #region KeepSelectBlock

            TempData["Actions"] = actions;
            TempData["MemeberSelect"] = (MemberViewModel)TempData["MemeberSelect"];

            #endregion KeepSelectBlock

            if (ModelState.IsValid)
            {
                if (actions == Actions.Create) //建立資料
                {
                    memberViewModel.MemberID = Guid.NewGuid(); // 日後可統一 Guid 或是 String 型態
                    TempData["message"] = _MemberService.CreateMember(memberViewModel, SignInManagerName);
                }
                else //更新資料
                {
                    TempData["message"] = _MemberService.UpdateMember(memberViewModel, SignInManagerName);
                }

                _MemberService.Save();
            }

            // 顯示資料
            memberViewModel = _MemberService.ReturnMemberDetail(actions, memberViewModel.MemberID.ToString().ToUpper());
            return View(memberViewModel);
        }

        #endregion
        #endregion

        [HttpPost]
        public JsonResult Delete(string guid, string actionTable)
        {
            string GetResult = "";
            TableName actionTableS = (TableName)Enum.Parse(typeof(TableName), actionTable);
            if (actionTableS == TableName.AspNetUsers)
            {
                TempData["message"] = GetResult = _UserService.DeleteUser(guid);
                _UserService.Save();
            }
            else
            {
                //GetResult = _StaticHtmlService.DeletePictureInfo(guid, SignInManagerName);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

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
    }
}