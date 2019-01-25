using BackMeow.Models.ViewModel;
using BackMeow.Service;
using StoreDB.Enum;
using StoreDB.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BackMeow.Controllers
{
    public class ActivityController : Controller
    {
        private readonly ActivitiesService _ActivityService;
        private string FileUrl = WebConfigurationManager.AppSettings["UploadFileUrl"];
        private string _signInManager;

        public ActivityController()
        {
            var unitOfWork = new EFUnitOfWork();
            _ActivityService = new ActivitiesService(unitOfWork);
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

        // GET: Activity
        [HttpGet]
        public ActionResult ActivitiesList(ActitiesViewModel ViewModel, int page = 1)
        {
            ActitiesViewModel ResultViewModel;
            ActitiesViewModel searchBlock = (ActitiesViewModel)TempData["ActitiesSelect"];
            if (searchBlock == null) /*空*/
            {
                ResultViewModel = _ActivityService.GetActitiesListViewModel(new ActitiesListHeaderViewModel(), page);
            }
            else
            {
                ResultViewModel = _ActivityService.GetActitiesListViewModel(searchBlock.Header, page);
            }
            return View(ResultViewModel);
        }

        [HttpPost]
        public ActionResult ActivitiesList(ActitiesViewModel ViewModel)
        {
            ActitiesViewModel ResultViewModel = _ActivityService.GetActitiesListViewModel(ViewModel.Header);
            TempData["ActitiesSelect"] = ResultViewModel;
            return View(ResultViewModel);
        }

        #region 藉由ID取得活動項目

        [HttpGet]
        public ActionResult ActivitiesMain(DataAction ActionType, string guid, string selectCreateTime,
            string selectTitleName, string selectHtmlContext, string selectStartDate, string selectEndDate, int pages = 1)
        {
            TempData["DataAction"] = ActionType;
            ActitiesDetailViewModel data = new ActitiesDetailViewModel();
            data = _ActivityService.ReturnActitiesDetailViewModel(ActionType, guid);

            #region KeepSelectBlock

            pages = pages == 0 ? 1 : pages;
            TempData["ActitiesSelect"] = new ActitiesViewModel()
            {
                Header = new ActitiesListHeaderViewModel()
                {
                    CreateTime = selectCreateTime,
                    EndDate = selectEndDate,
                    HtmlContext = selectHtmlContext,
                    StartDate = selectStartDate,
                    TitleName = selectTitleName
                },
                page = pages
            };

            #endregion KeepSelectBlock

            return View(data);
        }

        [HttpPost]
        //[ValidateFile] //上傳照片 日後將此功能抽出 ,日後改使用 MVC File upload unobtrusive validation
        public ActionResult ActivitiesMain(DataAction actions, ActitiesDetailViewModel ActitiesViewModel,
            IEnumerable<HttpPostedFileBase> upload)
        {
            #region KeepSelectBlock

            TempData["DataAction"] = actions;
            TempData["ActitiesSelect"] = (ActitiesViewModel)TempData["ActitiesSelect"];

            #endregion KeepSelectBlock

            if (ModelState.IsValid)
            {
                if (actions == DataAction.Create) //建立資料
                {
                    ActitiesViewModel.ActivityID = Guid.NewGuid().ToString().ToUpper();
                    ActitiesViewModel.PicGroupID = Guid.NewGuid().ToString().ToUpper();
                    //TempData["message"] = _ActivityService.Create(ActitiesViewModel, SignInManagerName);
                }
                else //更新資料
                {
                    //TempData["message"] = _ActivityService.Update(ActitiesViewModel, SignInManagerName);
                }

                #region 上傳照片 日後將此功能抽出

                if (upload.Where(s => s != null).Count() > 0)
                {
                    bool UploadResult = UploadFile(upload, ActitiesViewModel.PicGroupID.ToString());
                    if (UploadResult)
                    {
                        //存入DB
                        _ActivityService.CreatePictureInfo(upload, Guid.Parse(ActitiesViewModel.PicGroupID), SignInManagerName);
                    }
                }

                #endregion 上傳照片 日後將此功能抽出

                _ActivityService.Save();
            }

            // 顯示資料
            ActitiesViewModel = _ActivityService.ReturnActitiesDetailViewModel(actions, ActitiesViewModel.ActivityID);
            return View(ActitiesViewModel);
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

        #endregion 藉由ID取得活動項目
    }
}