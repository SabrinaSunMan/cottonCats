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
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Configuration;

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
        private readonly AspNetUsersRepository _AspNetUsersRep;
        private readonly IUnitOfWork _unitOfWork;

        public StaticHtmlService(IUnitOfWork unitOfWork)
        {
            //_HtmlSubject = new Repository<HtmlSubject>(unitOfWork);
            //_PictureInfo = new Repository<PictureInfo>(unitOfWork);
            //_StaticHtml = new Repository<StaticHtml>(unitOfWork);
            _unitOfWork = unitOfWork;
            _StaticHtmlRep = new StaticHtmlRepository(unitOfWork);
            _AspNetUsersRep = new AspNetUsersRepository(unitOfWork);

        }

        private readonly int pageSize = (int)BackPageListSize.commonSize;

        /// <summary>
        /// Gets the static HTML ListView model.
        /// </summary>
        /// <param name="selectModel">The select model.</param>
        /// <param name="nowpage">The nowpage.</param>
        /// <returns></returns>
        public StaticHtmlViewModel GetStaticHtmlListViewModel(StaticHtmlListHeaderViewModel selectModel, StaticHtmlAction selectType, int nowpage = 1)
        {
            StaticHtmlViewModel returnSystemRolesListViewModel = new StaticHtmlViewModel();
            returnSystemRolesListViewModel.Header = selectModel; /*表頭*/

            IEnumerable<StaticHtmlListContentViewModel> GetStaticHtmlListViewModelResult = GetAllStaticHtmlListViewModel(selectModel, selectType);
            int currentPage = (nowpage < 1) && GetStaticHtmlListViewModelResult.Count() >= 1 ? 1 : nowpage;
            returnSystemRolesListViewModel.Content_List = GetStaticHtmlListViewModelResult.ToPagedList(currentPage, pageSize);/*內容*/

            returnSystemRolesListViewModel.StaticHtmlActionType = selectType;

            return returnSystemRolesListViewModel;
        }


        /// <summary>
        /// Gets all static HTML ListView model.
        /// </summary>
        private IEnumerable<StaticHtmlListContentViewModel> GetAllStaticHtmlListViewModel(StaticHtmlListHeaderViewModel selectModel, StaticHtmlAction selectType)
        {
            //StaticHtmlListContentViewModel = 網頁要顯示的欄位抓取
            //此動作目的在於不顯示過多的資訊至網頁上，進行欄位Mapping動作
            IEnumerable<StaticHtmlListContentViewModel> ReturnList =
                _StaticHtmlRep.GetViewModel(selectType).Select(s => new StaticHtmlListContentViewModel()
                {
                    CreateTime = s.CreateTime.ToString(),
                    HtmlContext = s.HtmlContext,
                    StaticID = s.StaticID,
                    Status = s.Status.ToString()
                });
            return ReturnList;
        }

        /// <summary>
        /// Get 統一藉由此ViewModel溝通將 三張Table串接
        /// </summary>
        /// <returns></returns>
        private IEnumerable<StaticHtmlDBViewModel> GetAllStaticHtml()
        {
            IEnumerable<StaticHtmlDBViewModel> GetHtmlViewModel = _StaticHtmlRep.GetViewModel().ToList();
            return GetHtmlViewModel;
        }

        public StaticHtmlDetailViewModel ReturnStaticHtmlDetail(StaticHtmlAction selectType, Actions ActionType, string guid)
        {
            StaticHtmlDetailViewModel DetailViewModel = new StaticHtmlDetailViewModel();
            StaticHtmlDBViewModel StaticHtmlDBViewModel = _StaticHtmlRep.GetSingle(selectType, guid);
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            DetailViewModel = mapper.Map<StaticHtmlDetailViewModel>(StaticHtmlDBViewModel);

            /*Enum手動綁進*/
            DetailViewModel.StaticHtmlActionType = selectType;
            return DetailViewModel;
        }

        /// <summary>
        /// Returns the picture information list.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public IQueryable<PictureInfo> ReturnPictureInfoList(string groupguid)
        {
            return _StaticHtmlRep.ReturnPictureInfoList(groupguid);
        }

        /// <summary>
        /// Pictures the information count.
        /// </summary>
        /// <param name="groupguid">The groupguid.</param>
        /// <returns></returns>
        private int PictureInfoCount(string groupguid)
        {
            return _StaticHtmlRep.ReturnPictureInfoList(groupguid).Count() + 1;
        }

        public void CreatePictureInfo(IEnumerable<HttpPostedFileBase> upload, string PicGroupID, string UserName)
        {
            //1.取得目前使用者 ID
            AspNetUsers AspNetusers = _AspNetUsersRep.Query(s => s.UserName.Equals(UserName)).FirstOrDefault();//登入的使用者帳號

            //2.編號PicGroupID
            Guid PicgroupId;
            if (string.IsNullOrWhiteSpace(PicGroupID))
            {
                PicgroupId = Guid.NewGuid();
            }
            else PicgroupId = Guid.Parse(PicGroupID);

            //3.取得 sort 累加
            int DbSaveCount = PictureInfoCount(PicGroupID);

            //4.存取路徑
            string FileUrl = WebConfigurationManager.AppSettings["UploadFileUrl"];

            foreach (var item in upload)
            {
                PictureInfo saveData = new PictureInfo()
                {
                    CreateUser = AspNetusers.Id,
                    FileExtension = System.IO.Path.GetExtension(item.FileName),
                    PicGroupID = PicgroupId,
                    PicID = Guid.NewGuid(),
                    PictureName = item.FileName,
                    sort = DbSaveCount,
                    Status = true,
                    PictureUrl = FileUrl,
                    UpdateUser = AspNetusers.Id
                };

                _StaticHtmlRep.SavePictureInfo(saveData);
                DbSaveCount++;
            }

        }

        public void Save()
        {
            //_AspNetUsersRep.Commit();
            _unitOfWork.Save();
        }
    }
}