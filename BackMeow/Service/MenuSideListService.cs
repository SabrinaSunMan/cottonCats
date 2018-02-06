using AutoMapper;
using BackMeow.App_Start;
using StoreDB.Interface;
using StoreDB.Model.Partials;
using StoreDB.Model.ViewModel;
using StoreDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackMeow.Service
{
    /// <summary>
    /// 後台 側邊選單 相關.
    /// </summary>
    public class MenuSideListService
    {
        private readonly IRepository<MenuSideList> _MenuSideListRep;
        private readonly IRepository<MenuTree> _MenuTree;
        private readonly IRepository<MenuTreeRoot> _MenuTreeRoot;
        private readonly IUnitOfWork _unitOfWork;

        public MenuSideListService(IUnitOfWork unitOfWork)
        {
            _MenuSideListRep = new Repository<MenuSideList>(unitOfWork);
            _MenuTree = new Repository<MenuTree>(unitOfWork);
            _MenuTreeRoot = new Repository<MenuTreeRoot>(unitOfWork);
            _unitOfWork = unitOfWork;
        }


        //該名使用者可以使用的目錄
       public List<MenuTreeRootStratumViewModel> ReturnMenuSideViewModel(string guid)
        {
            List<MenuTreeRoot> getMenuTreeRoot = _MenuTreeRoot.GetAll().OrderBy(s=>s.TRootOrder).ToList();
            //List<MenuSideContentViewModel> getMenuSideContent = PackageMenuSideViewModel(guid).ToList();

            List<MenuTreeRootStratumViewModel> SideMenuViewModel = new List<MenuTreeRootStratumViewModel>();
            foreach(var items in getMenuTreeRoot)
            {
                List<MenuTree> tmp = _MenuTree.Query(s=>s.TRootID.Equals(items.TRootID)).OrderBy(s=>s.MenuOrder).ToList();

                MenuTreeRootStratumViewModel tmpSideModel = new MenuTreeRootStratumViewModel()
                {
                    TRootID = items.TRootID.ToString(),
                    TRootName = items.TRootName,
                    UrlIcon = items.UrlIcon,
                    TRootOrder = items.TRootOrder,
                    ActionName = tmp.FirstOrDefault().ActionName,
                    ControllerName = tmp.FirstOrDefault().ActionName,
                    tree = tmp
                };
                SideMenuViewModel.Add(tmpSideModel);
            }  
            return SideMenuViewModel;
    }

        public bool CheckRequestPage(string guid, string Controller, string ActionName)
        {
            //呼叫 PackageMenuSideViewModel(guid);
            //這邊要判斷 該使用者以及點擊的網頁是否他可以訪問的?
            return true;
        }

        //這個要放在Filters 每次登入就去檢查是否有可以讀取該網頁的權限
        private IEnumerable<MenuSideContentViewModel> PackageMenuSideViewModel(string guid)
        {
            // 取得 後台_測邊功能列與使用者的連繫Table
            List<MenuSideList> sidemenu = _MenuSideListRep.GetAll().ToList();
            // 取得子功能
            List<MenuTree> menu = _MenuTree.GetAll().ToList();
            // 取得根目錄的功能
            List<MenuTreeRoot> menuRoot = _MenuTreeRoot.GetAll().ToList();

            IEnumerable<MenuSideContentViewModel> t1 = from menulist in sidemenu
                     join tree in menu
                  on menulist.MenuID equals tree.MenuID
                     join Roots in menuRoot
                 on tree.TRootID equals Roots.TRootID
                     where menulist.Id == new Guid(guid)
                     orderby Roots.TRootOrder, tree.MenuOrder
                     select new
                     MenuSideContentViewModel
                     {
                         TRootID = Roots.TRootID.ToString(),
                         TRootName = Roots.TRootName,
                         TRootOrder = Roots.TRootOrder,
                         ActionName = tree.ActionName,
                         ControllerName = tree.ControllerName,
                         MenuName = tree.MenuName,
                         MenuOrder = tree.MenuOrder,
                         UrlIcon = Roots.UrlIcon
                     }; 

            return t1;
        }
    }
}