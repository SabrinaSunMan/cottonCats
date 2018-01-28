using StoreDB.Interface;
using StoreDB.Model.Partials;
using StoreDB.Model.ViewModel;
using StoreDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        /// <summary>
        /// Get All SideMenu can use.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<MenuSideList> GetAllMenuSideList()
        {
            IEnumerable<MenuSideList> GetMenuSideList = _MenuSideListRep.GetAll().ToList();
            return GetMenuSideList;
        }

        /// <summary>
        /// Get All MenuTree.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<MenuTree> GetAllMenuTree()
        {
            IEnumerable<MenuTree> GetMenuTree =  _MenuTree.GetAll().ToList();

            return GetMenuTree;
        }

        /// <summary>
        /// Get All MenuTreeRoot.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<MenuTreeRoot> GetAllMenuTreeRoot()
        {
            IEnumerable<MenuTreeRoot> GetMenuTree = _MenuTreeRoot.GetAll().ToList();
            return GetMenuTree;
        }

        //private IEnumerable<MenuSideViewModel> ReturnMenuSideViewModel()
        //{
        //    // 取得 後台_測邊功能列與使用者的連繫Table
        //    List<MenuSideList> sidemenu = GetAllMenuSideList().ToList(); 
        //    // 取得子功能
        //    List<MenuTree> menu = GetAllMenuTree().ToList();
        //    // 取得根目錄的功能
        //    List<MenuTreeRoot> menuRoot = GetAllMenuTreeRoot().ToList(); 
        //    //          var test = from word in sidemenu 
        //    //                        select word;
        //    //          from d in Duty
        //    //          join c in Company on d.CompanyId equals c.id
        //    //          join s in SewagePlant on c.SewagePlantId equals s.id
        //    //            .Select(m => new
        //    //            {
        //    //                duty = s.Duty.Duty,
        //    //                CatId = s.Company.CompanyName,
        //    //                SewagePlantName = s.SewagePlant.SewagePlantName
        //    //             other assignments
        //    //}); 
        //    //var allSide = from menulist in sidemenu
        //    //              join tree in menu on menu.FirstOrDefault().MenuID equals tree.MenuID
        //    //              join Roots in menuRoot on tree.TRootID equals Roots.TRootID
        //    //               .Select(s=> new
        //    //               { 
        //    //                   sidemenu.FirstOrDefault().
        //    //                   Roots.TRootName,
        //    //                   tree.ActionName,
        //    //                   tree.ControllerName
        //    //                   ,
        //    //                   tree.MenuOrder,
        //    //                   Roots.TRootOrder
        //    //               });
        //    //return allSide;
    }
}