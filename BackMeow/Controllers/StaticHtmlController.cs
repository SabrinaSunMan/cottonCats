using BackMeow.Models.ViewModel;
using BackMeow.Service;
using Microsoft.AspNet.Identity;
using StoreDB.Enum;
using StoreDB.Repositories;
using StoreDB.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BackMeow.Controllers
{
    public class StaticHtmlController : Controller
    {
        private readonly StaticHtmlService _StaticHtmlService;
        private readonly AspNetUsersService _UserService;
        private string FileUrl = WebConfigurationManager.AppSettings["UploadFileUrl"];
        private string _signInManager;
        private readonly LoggingService _logSvc;

        public StaticHtmlController()
        {
            var unitOfWork = new EFUnitOfWork();
            _StaticHtmlService = new StaticHtmlService(unitOfWork);
            _logSvc = new LoggingService(unitOfWork);
            _UserService = new AspNetUsersService(unitOfWork);
        }

        public string SignInManagerName
        {
            get
            {
                return _signInManager ?? HttpContext.User.Identity.Name.ToString();
            }
            private set
            {
                _signInManager = value;
            }
        }

        #region List - 靜態網站管理 - 關於

        [HttpGet]
        public ActionResult About(StaticHtmlViewModel ViewModel, int page = 1)
        {
            StaticHtmlViewModel ResultViewModel = HttpGetStaticHtmlViewModel((StaticHtmlViewModel)TempData["StaticHtmlSelect"], StaticHtmlAction.About, page);
            return View(ResultViewModel);
        }

        [HttpPost]
        public ActionResult About(StaticHtmlViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                StaticHtmlViewModel ResultViewModel = _StaticHtmlService.GetStaticHtmlListViewModel(ViewModel.Header, StaticHtmlAction.About);
                TempData["StaticHtmlSelect"] = ResultViewModel;
                return View(ResultViewModel);
            }
            else return View(ViewModel);
        }

        #endregion List - 靜態網站管理 - 關於

        #region List - 靜態網站管理 - 空間介紹

        [HttpGet]
        public ActionResult Space(StaticHtmlViewModel ViewModel, int page = 1)
        {
            StaticHtmlViewModel ResultViewModel = HttpGetStaticHtmlViewModel((StaticHtmlViewModel)TempData["StaticHtmlSelect"], StaticHtmlAction.Space, page);
            return View(ResultViewModel);
        }

        [HttpPost]
        public ActionResult Space(StaticHtmlViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                StaticHtmlViewModel ResultViewModel = _StaticHtmlService.GetStaticHtmlListViewModel(ViewModel.Header, StaticHtmlAction.Space);
                TempData["StaticHtmlSelect"] = ResultViewModel;
                return View(ResultViewModel);
            }
            else return View(ViewModel);
        }

        #endregion List - 靜態網站管理 - 空間介紹

        #region Main - 靜態網站管理 - 線上認養合約書

        [HttpGet]
        public ActionResult Contract(int page = 1)
        {
            return RedirectToAction("StaticHtmlMain", "StaticHtml",
                       new
                       {
                           ActionType = DataAction.Update,
                           guid = "EB26765E-F4C2-42EF-84E5-72925ADDA4D1",
                           selectType = StaticHtmlAction.Contract
                       });
        }

        #endregion Main - 靜態網站管理 - 線上認養合約書

        #region Main - 靜態網站管理 - 義工招募

        [HttpGet]
        public ActionResult Joinus(StaticHtmlViewModel ViewModel, int page = 1)
        {
            return RedirectToAction("StaticHtmlMain", "StaticHtml",
                        new
                        {
                            ActionType = DataAction.Update,
                            guid = "E39F8AB4-6496-412B-BBE8-09673B3D798B",
                            selectType = StaticHtmlAction.Joinus
                        });
        }

        #endregion Main - 靜態網站管理 - 義工招募

        /// <summary>
        /// 靜態網頁管理 取得 ViewModel
        /// </summary>
        /// <param name="searchBlock">The search block.</param>
        /// <param name="select">The select.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        private StaticHtmlViewModel HttpGetStaticHtmlViewModel(StaticHtmlViewModel searchBlock, StaticHtmlAction select, int page = 1)
        {
            StaticHtmlViewModel ResultViewModel = new StaticHtmlViewModel() { StaticHtmlActionType = select };
            if (searchBlock == null) /*空*/
            {
                ResultViewModel = _StaticHtmlService.GetStaticHtmlListViewModel(new StaticHtmlListHeaderViewModel(), select, page);
            }
            else
            {
                ResultViewModel = _StaticHtmlService.GetStaticHtmlListViewModel(searchBlock.Header, select, page);
            }
            return ResultViewModel;
        }

        #region Main - 明細內容動作

        /// <summary>
        /// 藉由ID取得靜態網頁管理明細.
        /// </summary>
        /// <param name="ActionType">Type of the action.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="select_CreateTime">The select create time.</param>
        /// <param name="select_HtmlContext">The select HTML context.</param>
        /// <param name="select_sort">The select sort.</param>
        /// <param name="selectType">Type of the select.</param>
        /// <param name="pages">The pages.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StaticHtmlMain(DataAction ActionType, string guid, string select_CreateTime, string select_HtmlContext, string select_sort,
            string selectType, int pages = 1)
        {
            TempData["DataAction"] = ActionType;
            StaticHtmlAction select = (StaticHtmlAction)Enum.Parse(typeof(StaticHtmlAction), selectType);
            StaticHtmlDetailViewModel data = new StaticHtmlDetailViewModel();
            data = _StaticHtmlService.ReturnStaticHtmlDetail(select, ActionType, guid);
            PackageTempData(select_CreateTime, select_HtmlContext, select_sort, select, pages);

            return View(data);
        }

        /// <summary>
        /// Packages the temporary data.
        /// </summary>
        /// <param name="select_CreateTime">The select create time.</param>
        /// <param name="select_HtmlContext">The select HTML context.</param>
        /// <param name="select_sort">The select sort.</param>
        /// <param name="selectType">Type of the select.</param>
        /// <param name="pages">The pages.</param>
        private void PackageTempData(string select_CreateTime, string select_HtmlContext, string select_sort,
            StaticHtmlAction selectType, int pages = 1)
        {
            pages = pages == 0 ? 1 : pages;
            TempData["StaticHtmlSelect"] = new StaticHtmlViewModel()
            {
                Header = new StaticHtmlListHeaderViewModel()
                {
                    CreateTime = select_CreateTime,
                    HtmlContext = select_HtmlContext,
                    sort = select_sort
                },
                page = pages,
                StaticHtmlActionType = selectType
            };
        }

        /// <summary>
        /// 靜態網頁管理 存檔
        /// </summary>
        /// <param name="actions">The actions.</param>
        /// <param name="satichtmlViewModel">The satichtml view model.</param>
        /// <param name="upload">The upload.</param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateFile] //上傳照片 日後將此功能抽出 ,日後改使用 MVC File upload unobtrusive validation
        public ActionResult StaticHtmlMain(DataAction actions, StaticHtmlDetailViewModel satichtmlViewModel,
            IEnumerable<HttpPostedFileBase> upload)
        {
            #region KeepSelectBlock

            TempData["DataAction"] = actions;
            TempData["StaticHtmlSelect"] = (StaticHtmlViewModel)TempData["StaticHtmlSelect"];

            #endregion KeepSelectBlock

            if (ModelState.IsValid)
            {
                if (actions == DataAction.Create) //建立資料
                {
                    satichtmlViewModel.StaticID = Guid.NewGuid().ToString().ToUpper();
                    satichtmlViewModel.PicGroupID = Guid.NewGuid().ToString().ToUpper();
                    TempData["message"] = _StaticHtmlService.CreateStaticHtml(satichtmlViewModel, SignInManagerName);
                }
                else //更新資料
                {
                    TempData["message"] = _StaticHtmlService.UpdateStaticHtml(satichtmlViewModel, SignInManagerName);
                }

                #region 上傳照片 日後將此功能抽出

                if (upload.Where(s => s != null).Count() > 0)
                {
                    bool UploadResult = UploadFile(upload, satichtmlViewModel.PicGroupID.ToString());
                    if (UploadResult)
                    {
                        //存入DB
                        _StaticHtmlService.CreatePictureInfo(upload, Guid.Parse(satichtmlViewModel.PicGroupID), SignInManagerName);
                    }
                }

                #endregion 上傳照片 日後將此功能抽出

                _StaticHtmlService.Save();
            }

            // 顯示資料
            satichtmlViewModel = _StaticHtmlService.ReturnStaticHtmlDetail(satichtmlViewModel.StaticHtmlActionType, actions, satichtmlViewModel.StaticID);
            return View(satichtmlViewModel);
        }

        /// <summary>
        /// Uploads the file. 上傳照片 日後將此功能抽出 ,日後改使用 MVC File upload unobtrusive validation
        /// </summary>
        /// <param name="upload">The upload.</param>
        /// <returns></returns>
        private bool UploadFile(IEnumerable<HttpPostedFileBase> upload, string FolderName)
        {
            bool TmpResult = false;
            try
            {
                if (upload != null)
                {
                    if (upload.Count() > 0)
                    {
                        string savePath = Server.MapPath(FileUrl) + FolderName;
                        if (!Directory.Exists(savePath))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(savePath);
                        }
                        foreach (var uploadFile in upload)
                            if (uploadFile.ContentLength > 0 && uploadFile.FileName.Length < 20)
                            {
                                string N_savePath = savePath + "\\" + uploadFile.FileName;
                                uploadFile.SaveAs(N_savePath);
                                TmpResult = true;
                            }
                    }
                }
            }
            catch (IOException e)
            {
            }
            return TmpResult;
        }

        #endregion Main - 明細內容動作

        #region 回傳 刪除後Context 日後可抽出

        [HttpPost]
        public JsonResult Delete(string StaticGuid, string guid, string actionTable)
        {
            string GetResult = "";
            TableName actionTableS = (TableName)Enum.Parse(typeof(TableName), actionTable);
            if (actionTableS == TableName.StaticHtml)
            {
                GetResult = _StaticHtmlService.DeleteStaticHtml(guid, SignInManagerName);
            }
            else // actionTable == TableName.PictureInfo
            {
                GetResult = _StaticHtmlService.DeletePictureInfo(guid, SignInManagerName);
            }
            _StaticHtmlService.Save();
            string viewContent = RenderRazorViewToString(ControllerContext, "_UploadFiles", _StaticHtmlService.GetPictureInfo(guid));
            return Json(new { result = GetResult, viewModel = viewContent }

                , JsonRequestBehavior.AllowGet);
        }

        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }

        public static string RenderRazorViewToString(ControllerContext controllerContext, string viewName, object model)
        {
            controllerContext.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        #endregion 回傳 刪除後Context 日後可抽出

        /// <summary>
        /// 客制化錯誤訊息
        /// </summary>
        /// <param name="ErrorMsg">The error MSG.</param>
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