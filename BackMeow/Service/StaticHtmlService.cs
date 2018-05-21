using BackMeow.App_Start;
using BackMeow.Models.ViewModel;
using PagedList;
using StoreDB.Enum;
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
    /// 後台 靜態網頁 管理.
    /// </summary>
    public class StaticHtmlService
    {
        //private readonly IRepository<HtmlSubject> _HtmlSubject;
        //private readonly IRepository<PictureInfo> _PictureInfo;
        //private readonly IRepository<StaticHtml> _StaticHtml;
        private readonly StaticHtmlRepository _StaticHtmlRep;
        private readonly IUnitOfWork _unitOfWork;

        public StaticHtmlService(IUnitOfWork unitOfWork)
        {
            //_HtmlSubject = new Repository<HtmlSubject>(unitOfWork);
            //_PictureInfo = new Repository<PictureInfo>(unitOfWork);
            //_StaticHtml = new Repository<StaticHtml>(unitOfWork);
            _unitOfWork = unitOfWork;
            _StaticHtmlRep = new StaticHtmlRepository(unitOfWork);
        }

        private readonly int pageSize = (int)BackPageListSize.commonSize;

        /// <summary>
        /// Gets the static HTML ListView model.
        /// </summary>
        /// <param name="selectModel">The select model.</param>
        /// <param name="nowpage">The nowpage.</param>
        /// <returns></returns>
        public StaticHtmlViewModel GetStaticHtmlListViewModel(StaticHtmlListHeaderViewModel selectModel, StaticHtmlAction selectType,int nowpage = 1)
        {
            StaticHtmlViewModel returnSystemRolesListViewModel = new StaticHtmlViewModel();
            returnSystemRolesListViewModel.Header = selectModel; /*表頭*/
            IEnumerable<StaticHtmlListContentViewModel> GetStaticHtmlListViewModelResult = GetAllStaticHtmlListViewModel(selectModel, selectType);
            int currentPage = (nowpage < 1) && GetStaticHtmlListViewModelResult.Count() >= 1 ? 1 : nowpage;
            returnSystemRolesListViewModel.Content_List = GetStaticHtmlListViewModelResult.ToPagedList(currentPage, pageSize);/*內容*/
            return returnSystemRolesListViewModel;
        }
        

        /// <summary>
        /// Gets all static HTML ListView model.
        /// </summary>
        private IEnumerable<StaticHtmlListContentViewModel> GetAllStaticHtmlListViewModel(StaticHtmlListHeaderViewModel selectModel,StaticHtmlAction selectType)
        {
            //StaticHtmlListContentViewModel = 網頁要顯示的欄位抓取
            //此動作目的在於不顯示過多的資訊至網頁上，進行欄位Mapping動作
            IEnumerable<StaticHtmlListContentViewModel> ReturnList =
                _StaticHtmlRep.GetViewModel(selectType).Select(s => new StaticHtmlListContentViewModel()
                {
                    CreateTime = s.CreateTime.ToString(),
                    HtmlContext = s.HtmlContext,
                    StaticID = s.StaticID,
                    Status = s.Status
                });
            return ReturnList;
        }

        /// <summary>
        /// Get 統一藉由此ViewModel溝通將 三張Table串接
        /// </summary>
        /// <returns></returns>
        private IEnumerable<StaticHtmlDBViewModel> GetAllStaticHtml()
        {
            IEnumerable<StaticHtmlDBViewModel> GetHtmlViewModel =_StaticHtmlRep.GetViewModel().ToList();
            return GetHtmlViewModel;
        }


        //public AspNetUsersDetailViewModel ReturnAspNetUsersDetail(Actions ActionType, string guid)
        //{
        //    AspNetUsersDetailViewModel DetailViewModel = new AspNetUsersDetailViewModel();
        //    AspNetUsers AspNetUsersViewModel = GetAspNetUsersById(guid);
        //    var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        //    DetailViewModel = mapper.Map<AspNetUsersDetailViewModel>(AspNetUsersViewModel);
        //    //DetailViewModel = _aspnetMapping.MapperAspNetUsersDetailViewModel(AspNetUsersViewModel); 
        //    return DetailViewModel;
        //}
    }
}