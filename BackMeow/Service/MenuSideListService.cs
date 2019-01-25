using StoreDB.Interface;
using StoreDB.Model.Partials;
using StoreDB.Model.ViewModel.BackcottonCats;
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
            List<MenuTreeRoot> getMenuTreeRoot = _MenuTreeRoot.GetAll().Where(s => s.TRootOrder != 0).OrderBy(s => s.TRootOrder).ToList();
            //List<MenuSideContentViewModel> getMenuSideContent = PackageMenuSideViewModel(guid).ToList();

            List<MenuTreeRootStratumViewModel> SideMenuViewModel = new List<MenuTreeRootStratumViewModel>();
            foreach (var items in getMenuTreeRoot)
            {
                List<MenuTree> tmp = _MenuTree.Query(s => s.TRootID.Equals(items.TRootID)).OrderBy(s => s.MenuOrder).ToList();
                MenuTreeRootStratumViewModel tmpSideModel = new MenuTreeRootStratumViewModel()
                {
                    TRootID = items.TRootID.ToString(),
                    TRootName = items.TRootName,
                    UrlIcon = items.UrlIcon,
                    TRootOrder = items.TRootOrder,
                    ActionName = tmp.FirstOrDefault().ActionName,
                    ControllerName = tmp.FirstOrDefault().ControllerName,
                    tree = tmp
                };
                SideMenuViewModel.Add(tmpSideModel);
            }
            return SideMenuViewModel;
        }

        /// <summary>
        /// 使用者是否可訪問該頁面權限
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="Controller">The controller.</param>
        /// <returns></returns>
        public bool CheckRequestPage(string guid, string Controller) //, string ActionName
        {
            List<MenuSideContentViewModel> userMenu = PackageMenuSideViewModel(guid).ToList();
            if (userMenu.Where(s => s.ControllerName.Equals(Controller)).ToList().Count > 0)
            { //目前只有去判斷到Contrller 並未判斷到 ActionName，如果需要得開Table
                return true;
            }
            return false;
        }

        /// <summary>
        /// 藉由使用者ID 開通權限. 日後可更改權限群組，現階段先以全部開通為基準
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        public void CreateMenuTree(string guid)
        {
            //List<MenuSideList> ReadySave = new List<MenuSideList>();
            foreach (var SaveItem in _MenuTree.GetAll())
            {
                Guid sss = new Guid(guid);
                MenuSideList ReadySave = new MenuSideList()
                {
                    Id = new Guid(guid),
                    MenuSideListID = Guid.NewGuid(),
                    MenuID = SaveItem.MenuID,
                    Status = true
                };
                _MenuSideListRep.Create(ReadySave);
            }
            _MenuSideListRep.Commit();
        }

        /// <summary>
        /// 藉由使用者ID取得
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
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