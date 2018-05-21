using BackMeow.Models.ViewModel;
using BackMeow.Service;
using StoreDB.Enum;
using StoreDB.Repositories;
using StoreDB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackMeow.Controllers
{
    public class StaticHtmlController : Controller
    {
        private readonly StaticHtmlService _StaticHtmlService;
        private readonly LoggingService _logSvc;

        public StaticHtmlController()
        {
            var unitOfWork = new EFUnitOfWork();
            _StaticHtmlService = new StaticHtmlService(unitOfWork);
            _logSvc = new LoggingService(unitOfWork);
        }

        #region 靜態網站管理
        #region 靜態網站管理 - 關於
        #region 取得[關於我們]清單
        [HttpGet]
        public ActionResult About(StaticHtmlViewModel ViewModel, int page = 1)
        {
            StaticHtmlViewModel ResultViewModel = HttpGetStaticHtmlViewModel((StaticHtmlViewModel)TempData["StaticHtmlSelect"], StaticHtmlAction.About,page);
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
        #endregion

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

        public ActionResult MainDetail(Actions ActionType, string guid, string select_CreateTime, string select_HtmlContext,string select_sort,
            string selectType, int pages = 1)
        {
            //if (!string.IsNullOrWhiteSpace(selectType))
            //{
            StaticHtmlAction select = (StaticHtmlAction)Enum.Parse(typeof(StaticHtmlAction), selectType);
            switch (select)
            {
                case StaticHtmlAction.About:
                case StaticHtmlAction.Space:
                    return RedirectToAction("Index", "Home", new { Area = "" });
                case StaticHtmlAction.Contract:
                case StaticHtmlAction.Joinus:
                    return RedirectToAction("Index", "Home", new { Area = "" });
                default:
                    break;
            }
            // } 
            TempData["StaticHtmlSelect"] = new StaticHtmlViewModel()
            {
                Header = new StaticHtmlListHeaderViewModel()
                {
                    CreateTime = select_CreateTime,
                    HtmlContext = select_HtmlContext,
                    sort = select_sort
                },
                page = pages,
                StaticHtmlActionType = select
            };
            
            return RedirectToAction(selectType, "StaticHtml", new { Area = "" });
        }

        #region 藉由ID取得靜態網頁管理明細
        [HttpGet]
        public ActionResult AboutMain(Actions ActionType, string guid, string selectEmail, string selectUserName, int pages = 1)
        {
            //TempData["Actions"] = ActionType;
            //AspNetUsersDetailViewModel data = new AspNetUsersDetailViewModel();
            //if (ActionType == Actions.Update)
            //{
            //    data = _UserService.ReturnAspNetUsersDetail(ActionType, guid);
            //}
            //#region KeepSelectBlock
            //pages = pages == 0 ? 1 : pages;
            //TempData["SystemRolesSelect"] = new SystemRolesViewModel()
            //{
            //    Header = new SystemRolesListHeaderViewModel()
            //    {
            //        Email = selectEmail,
            //        UserName = selectUserName
            //    },
            //    page = pages
            //};
            //#endregion
            //return View(data);
            return View();
        }
        #endregion
        #endregion

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
        #endregion
        #endregion
        #endregion
    }
}