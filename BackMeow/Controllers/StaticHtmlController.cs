using BackMeow.Models.ViewModel;
using BackMeow.Service;
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

        #region 靜態網站管理

        #region 靜態網站管理 - 關於

        #region 取得[關於我們]清單

        [HttpGet]
        public ActionResult About(StaticHtmlViewModel ViewModel, int page = 1)
        {
            StaticHtmlViewModel ResultViewModel = HttpGetStaticHtmlViewModel((StaticHtmlViewModel)TempData["StaticHtmlSelect"], StaticHtmlAction.About, page);
            return View(ResultViewModel);
            //return RedirectToAction("StaticHtmlSelect", ResultViewModel);
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

        #endregion 取得[關於我們]清單

        #region 藉由ID取得靜態網頁管理明細

        [HttpGet]
        public ActionResult StaticHtmlMain(Actions ActionType, string guid, string select_CreateTime, string select_HtmlContext, string select_sort,
            string selectType, int pages = 1)
        {
            TempData["Actions"] = ActionType;
            StaticHtmlAction select = (StaticHtmlAction)Enum.Parse(typeof(StaticHtmlAction), selectType);
            StaticHtmlDetailViewModel data = new StaticHtmlDetailViewModel();
            //if (ActionType == Actions.Update)
            //{
            data = _StaticHtmlService.ReturnStaticHtmlDetail(select, ActionType, guid);
            //}
            // KeepSelectBlock
            PackageTempData(select_CreateTime, select_HtmlContext, select_sort, select, pages);

            return View(data);
        }

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

        #endregion 藉由ID取得靜態網頁管理明細

        #region 靜態網頁管理 存檔

        [HttpPost]
        //[ValidateFile] //上傳照片 日後將此功能抽出 ,日後改使用 MVC File upload unobtrusive validation
        public ActionResult StaticHtmlMain(Actions actions, StaticHtmlDetailViewModel satichtmlViewModel,
            IEnumerable<HttpPostedFileBase> upload)
        {
            #region KeepSelectBlock

            TempData["Actions"] = actions;
            TempData["StaticHtmlSelect"] = (StaticHtmlViewModel)TempData["StaticHtmlSelect"];

            #endregion KeepSelectBlock

            if (ModelState.IsValid)
            {
                if (actions == Actions.Create) //建立資料
                {
                    satichtmlViewModel.StaticID = Guid.NewGuid().ToString().ToUpper();
                    satichtmlViewModel.PicGroupID = Guid.NewGuid();
                    TempData["message"] = _StaticHtmlService.CreateStaticHtml(satichtmlViewModel, SignInManagerName);
                }
                else //更新資料
                {
                    TempData["message"] = _StaticHtmlService.UpdateStaticHtml(satichtmlViewModel);
                }

                #region 上傳照片 日後將此功能抽出

                //var result = new BasicController().Uploaded(upload);
                bool UploadResult = UploadFile(upload, satichtmlViewModel.StaticID);
                if (UploadResult)
                {
                    //存入DB
                    _StaticHtmlService.CreatePictureInfo(upload, satichtmlViewModel.PicGroupID, SignInManagerName);
                }

                #endregion 上傳照片 日後將此功能抽出

                _StaticHtmlService.Save();
            }

            // 顯示資料
            //StaticHtmlAction select = (StaticHtmlAction)Enum.Parse(typeof(StaticHtmlAction), );
            satichtmlViewModel = _StaticHtmlService.ReturnStaticHtmlDetail(satichtmlViewModel.StaticHtmlActionType, actions, satichtmlViewModel.StaticID);

            //return RedirectToAction("StaticHtmlMain", "StaticHtml", new { Area = "", selectType= satichtmlViewModel.SubjectID });
            return View(satichtmlViewModel);

            //switch (satichtmlViewModel.StaticHtmlActionType)
            //{
            //    case StaticHtmlAction.About:
            //    case StaticHtmlAction.Space:
            //        return RedirectToAction(Temp, "StaticHtml", new { Area = "" });
            //    case StaticHtmlAction.Contract:
            //    case StaticHtmlAction.Joinus:
            //        return RedirectToAction("Index", "StaticHtml", new { Area = "" });
            //    default:
            //        return RedirectToAction("SystemRoles", new
            //        {
            //            ViewModel = searchBlock,
            //            pages = searchBlock.page
            //        });
            //        break;
            //}

            //
            //TempData["StaticHtmlSelect"] = new StaticHtmlViewModel()
            //{
            //    Header = new StaticHtmlListHeaderViewModel()
            //    {
            //        CreateTime = select_CreateTime,
            //        HtmlContext = select_HtmlContext,
            //        sort = select_sort
            //    },
            //    page = pages,
            //    StaticHtmlActionType = select
            //};
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

        #endregion 靜態網頁管理 存檔

        #endregion 靜態網站管理 - 關於

        #region 靜態網站管理 - 空間介紹

        #region 取得[關於我們]清單

        [HttpGet]
        public ActionResult Space(StaticHtmlViewModel ViewModel, int page = 1)
        {
            StaticHtmlViewModel ResultViewModel = HttpGetStaticHtmlViewModel((StaticHtmlViewModel)TempData["StaticHtmlSelect"], StaticHtmlAction.Space, page);
            return RedirectToAction("StaticHtmlSelect", ResultViewModel);
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

        #endregion 取得[關於我們]清單

        #endregion 靜態網站管理 - 空間介紹

        #endregion 靜態網站管理

        [HttpPost]
        public JsonResult Delete(string StaticGuid, string guid, string actionTable)
        {
            string GetResult = "";
            TableName actionTableS = (TableName)Enum.Parse(typeof(TableName), actionTable);
            if (actionTableS == TableName.StaticHtml)
            {
                _StaticHtmlService.DeleteStaticHtml(guid);
            }
            else // actionTable == TableName.PictureInfo
            {
                _StaticHtmlService.DeletePictureInfo(guid);
            }
            _StaticHtmlService.Save();
            string viewContent = RenderRazorViewToString(ControllerContext, "_UploadFiles", _StaticHtmlService.GetPictureInfo(guid));
            //PartialView("_UploadFiles", )

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
    }
}