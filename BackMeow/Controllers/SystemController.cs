using BackMeow.Filters;
using BackMeow.Models;
using BackMeow.Models.ViewModel;
using BackMeow.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StoreDB;
using StoreDB.Enum;
using StoreDB.Helper;
using StoreDB.Model.ViewModel;
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
        private readonly MemberService _MemberService;
        private readonly LoggingService _logSvc;
        private string _signInManagerNames;
        private readonly string _actionName;
        private readonly string _controllerName;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private MenuSideListService _menuSide;
        private PublicService _publicService;
        //private ReturnMsg _ReturnMsg;

        public SystemController()
        {
            var unitOfWork = new EFUnitOfWork();
            _UserService = new AspNetUsersService(unitOfWork);
            _MemberService = new MemberService(unitOfWork);
            _logSvc = new LoggingService(unitOfWork);
            _menuSide = new MenuSideListService(unitOfWork);
            _publicService = new PublicService();
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

        /// <summary>
        /// 藉由登入名稱取得ID.
        /// </summary>
        public string SignInManagerId
        {
            get
            {
                _UserService.UserName = HttpContext.User.Identity.Name.ToString(); //登入的使用者帳號
                return _UserService.GetAspNetUserBySelectPramters().Id;
            }
            private set
            {
                _signInManagerNames = value;
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
        public ActionResult SystemRoles(int page = 1)
        {
            SystemRolesViewModel ResultViewModel;
            SystemRolesViewModel searchBlock = (SystemRolesViewModel)TempData["SystemRolesSelect"];
            if (searchBlock == null) /*空*/
            {
                ResultViewModel = _UserService.GetSystemRolesListViewModel(new SystemRolesListHeaderViewModel(), page);
            }
            else
            {
                ResultViewModel = _UserService.GetSystemRolesListViewModel(searchBlock.Header,
                    page > searchBlock.page ?
                            page : searchBlock.page);
                SystemRolesKeepSelectBlock(ResultViewModel, DataAction.Read);
            }

            return View(ResultViewModel);
        }

        /// <summary>
        /// Systems the roles.
        /// </summary>
        /// <param name="ViewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SystemRoles(SystemRolesViewModel ViewModel, int page = 1)
        {
            SystemRolesViewModel ResultViewModel = _UserService.GetSystemRolesListViewModel(ViewModel.Header);
            SystemRolesKeepSelectBlock(ResultViewModel, DataAction.Read);
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
        public ActionResult SystemRolesMain(DataAction ActionType, string guid, string selectEmail, string selectUserName, int pages = 1)
        {
            AspNetUsersDetailViewModel data = new AspNetUsersDetailViewModel();
            if (ActionType == DataAction.Update)
            {
                data = _UserService.ReturnAspNetUsersDetail(ActionType, guid);
            }

            #region KeepSelectBlock

            TempData["Actions"] = ActionType;
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
        public async Task<ActionResult> SystemRolesMain(AspNetUsersDetailViewModel AspNetUsersModel, DataAction actions)//, DataAction actions)
                                                                                                                        //(FormCollection AspNetUsersModel,string guid) //,
        {
            bool boolResult = true; // 取決於導向頁面, True = 返回SystemRoles, False = 停在本頁
            string thisUserID; //暫存 使用者ID
            SystemRolesViewModel searchBlock = (SystemRolesViewModel)TempData["SystemRolesSelect"];

            // KeepSelectBlock
            SystemRolesKeepSelectBlock(searchBlock, actions);

            // STEP 1. 前端驗證是否通過
            if (ModelState.IsValid)
            {
                // STEP 2. 建立容器 user
                var user = new ApplicationUser
                {
                    UserName = AspNetUsersModel.UserName,
                    Email = AspNetUsersModel.Email,
                    PhoneNumber = AspNetUsersModel.PhoneNumber,
                    UpdateTime = AspNetUsersModel.UpdateTime,
                    CreateTime = AspNetUsersModel.CreateTime,
                    UpdateUser = SignInManagerId,
                    Status = true
                };

                if (actions == DataAction.Create)
                {
                    #region STEP 3. 判斷動作, [新增]

                    user.CreateUser = SignInManagerId;
                    user.Id = Guid.NewGuid().ToString().ToUpper();
                    _UserService.UserName = user.UserName;
                    _UserService.UserEmail = user.Email;

                    // STEP 4. 該使用者資訊是否存在資料庫, null才可繼續建立
                    if (_UserService.GetAspNetUserBySelectPramters() == null)
                    {
                        var result = await UserManager.CreateAsync(user, AspNetUsersModel.Password);
                        if (result.Succeeded)
                        {
                            //建立使用者應要把 MenuTree 權限也寫入!
                            _UserService.CreateUserMenuTree(user.Id);
                            TempData["message"] = EnumHelper.GetEnumDescription(DataAction.CreateScuess);
                            thisUserID = user.Id;
                            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                            // 傳送包含此連結的電子郵件
                            //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                            //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                            //await UserManager.SendEmailAsync(user.Id, "確認您的帳戶", "請按一下此連結確認您的帳戶 <a href=\"" + callbackUrl + "\">這裏</a>");
                        }
                        else
                        {
                            // 建立失敗, 回傳錯誤訊息
                            AddErrors(result);
                            boolResult = false;
                        }
                    }
                    else
                    {
                        // 建立失敗, 回傳錯誤訊息
                        CustomerIdentityError(EnumHelper.GetEnumDescription(DataAction.CreateFailReapet));
                        boolResult = false;
                    }

                    #endregion STEP 3. 判斷動作, [新增]
                }
                else if (actions == DataAction.Update)
                {
                    #region STEP 3. 判斷動作, [更新]

                    if (!string.IsNullOrEmpty(AspNetUsersModel.Old_Password) &&
                        !string.IsNullOrEmpty(AspNetUsersModel.Password))
                    {
                        bool passwordIsEdit = false;
                        try
                        {
                            var checkPassword = UserManager.PasswordHasher.
                                VerifyHashedPassword(AspNetUsersModel.Password, AspNetUsersModel.Old_Password);
                            if (checkPassword != PasswordVerificationResult.Success)
                            {
                                passwordIsEdit = true;
                            }
                        }
                        catch
                        {
                            passwordIsEdit = true;
                        }
                        if (passwordIsEdit)
                        {
                            user.Id = AspNetUsersModel.Id;
                            // 變更密碼
                            var result = await UserManager.
                                ChangePasswordAsync(user.Id, AspNetUsersModel.Old_Password, AspNetUsersModel.Password);
                            if (result.Succeeded)
                            {
                                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            }
                            else
                            {
                                // 建立失敗, 回傳錯誤訊息
                                AddErrors(result);
                                boolResult = false;
                            }
                        }
                        else
                        {
                            // 建立失敗, 回傳錯誤訊息
                            CustomerIdentityError(EnumHelper.GetEnumDescription(DataAction.UpdateFail));
                            boolResult = false;
                        }
                    }

                    _UserService.AspNetUsersDetailViewModelUpdate(AspNetUsersModel, SignInManagerId);
                    //可以批次增加同時輸入很多個Table
                    _UserService.Save();
                    TempData["message"] = EnumHelper.GetEnumDescription(DataAction.UpdateScuess);

                    #endregion STEP 3. 判斷動作, [更新]
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
            return View(AspNetUsersModel);
        }

        /// <summary>
        /// 保留搜尋頁面資料_管理者SystemRoles
        /// </summary>
        /// <param name="searchBlock"></param>
        /// <param name="actions"></param>
        private void SystemRolesKeepSelectBlock(SystemRolesViewModel searchBlock, DataAction actions)
        {
            TempData["Actions"] = actions;
            TempData["SystemRolesSelect"] = searchBlock;
        }

        #endregion Main - 明細內容動作

        #endregion 後台使用者管理

        #region 會員管理

        #region List - 取得所有會員清單

        /// <summary>
        /// Members the specified view model.
        /// </summary>
        /// <param name="ViewModel">The view model.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Member(int page = 1)
        {
            MemberViewModel ResultViewModel;
            MemberViewModel searchBlock = (MemberViewModel)TempData["MemeberSelect"];

            if (searchBlock == null) /*空*/
            {
                ResultViewModel = _MemberService.GetMemberListViewModel(new MemberListHeaderViewModel(), page);
                MemberGetViewBag("");
            }
            else
            {
                ResultViewModel = _MemberService.GetMemberListViewModel(searchBlock.Header, page > searchBlock.page ?
                            page : searchBlock.page);
                MemberKeepSelectBlock(ResultViewModel, DataAction.Read);
                MemberGetViewBag(searchBlock.Header.ContractCheck);
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
            MemberGetViewBag(ViewModel.Header.ContractCheck);
            return View(ResultViewModel);
        }

        #endregion List - 取得所有會員清單

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
        public ActionResult MemberMain(DataAction ActionType,
            string guid,
            string selectName,
            string selectContractCheck,
            string selectPhoneNumber,
            string selectCity,
            string selectCountry,
            int pages = 1)
        {
            TempData["Actions"] = ActionType;
            MemberDetailViewModel data = new MemberDetailViewModel();
            if (ActionType == DataAction.Update)
            {
                data = _MemberService.ReturnMemberDetail(ActionType, guid);
            }
            data.Sex = false;

            #region KeepSelectBlock

            pages = pages == 0 ? 1 : pages;
            TempData["MemeberSelect"] = new MemberViewModel()
            {
                Header = new MemberListHeaderViewModel()
                {
                    Name = selectName,
                    ContractCheck = selectContractCheck,
                    PhoneNumber = selectPhoneNumber == "" ? 0 : Convert.ToInt16(selectPhoneNumber),
                    CityDDL = selectCity,
                    CountyDDL = selectCountry
                },
                page = pages
            };
            MemberGetViewBag(selectContractCheck);

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
        public ActionResult MemberMain(DataAction actions, MemberDetailViewModel memberViewModel)
        {
            MemberViewModel searchBlock = (MemberViewModel)TempData["MemeberSelect"];
            // KeepSelectBlock
            MemberKeepSelectBlock(searchBlock, actions);
            PublicMethodResult ResultViewModel = new PublicMethodResult();
            if (ModelState.IsValid)
            {
                if (actions == DataAction.Create) //建立資料
                {
                    memberViewModel.MemberID = Guid.NewGuid(); // 日後可統一 Guid 或是 String 型態
                    ResultViewModel = _MemberService.CreateMember(memberViewModel, SignInManagerId);
                }
                else //更新資料
                {
                    ResultViewModel = _MemberService.UpdateMember(memberViewModel, SignInManagerId);
                }
                _MemberService.Save();
            }

            TempData["message"] = ResultViewModel.Result;
            if (ResultViewModel.ResultBool) // 取決於導向頁面, True = 返回SystemRoles, False = 停在本頁
            {
                return RedirectToAction("Member", new
                {
                    ViewModel = searchBlock,
                    pages = searchBlock.page
                });
            }
            // 顯示資料
            //memberViewModel = _MemberService.ReturnMemberDetail(actions, memberViewModel.MemberID.ToString().ToUpper());

            return View(memberViewModel);
        }

        /// <summary>
        /// 保留搜尋頁面資料_管理者SystemRoles.
        /// </summary>
        /// <param name="searchBlock"></param>
        /// <param name="actions"></param>
        private void MemberKeepSelectBlock(MemberViewModel searchBlock, DataAction actions)
        {
            TempData["Actions"] = actions;
            TempData["MemeberSelect"] = searchBlock;
        }

        /// <summary>
        /// 包裝 DropDownList : ContractCheck 是否填寫同意書.
        /// </summary>
        /// <param name="defaultvalue"></param>
        private void MemberGetViewBag(string DefaultValue)
        {
            ViewBag.ContractCheckList = _publicService.GetContractCheckList(DefaultValue).Result;
        }

        #endregion Main - 明細內容動作

        #endregion 會員管理

        [HttpPost]
        public JsonResult Delete(string guid, string actionTable)
        {
            string GetResult = "";
            TableName actionTableS = (TableName)Enum.Parse(typeof(TableName), actionTable);
            if (actionTableS == TableName.AspNetUsers)
            {
                // 這裡的刪除僅限於將 [Status] 更改成 false, 故 MenuTree 並不會連動
                TempData["message"] = GetResult = _UserService.DeleteUser(guid, SignInManagerId);
                _UserService.Save();
            }
            else
            {
                //GetResult = _StaticHtmlService.DeletePictureInfo(guid, SignInManagerName);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 藉由 欄位 與值, 判斷是否能找出資料庫匹配的資料.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMatchBool(string Filed, string MatchValue)
        {
            bool result = _MemberService.GetMatchBool(Filed, MatchValue);
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
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