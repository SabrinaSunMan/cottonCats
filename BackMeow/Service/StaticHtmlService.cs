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
        private readonly StaticHtmlRepository _StaticHtmlRep;
        private readonly PictureInfoRepository _PicInfoRep;

        //private readonly AspNetUsersRepository _AspNetUsersRep;
        private readonly IUnitOfWork _unitOfWork;

        public StaticHtmlService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _StaticHtmlRep = new StaticHtmlRepository(unitOfWork);
            _PicInfoRep = new PictureInfoRepository(unitOfWork);
            //_AspNetUsersRep = new AspNetUsersRepository(unitOfWork);
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

        /// <summary>
        /// Returns the static HTML detail.
        /// </summary>
        /// <param name="selectType">Type of the select.</param>
        /// <param name="ActionType">Type of the action.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public StaticHtmlDetailViewModel ReturnStaticHtmlDetail(StaticHtmlAction selectType, Actions ActionType, string guid)
        {
            StaticHtmlDetailViewModel DetailViewModel = new StaticHtmlDetailViewModel();
            StaticHtmlDBViewModel StaticHtmlDBViewModel = _StaticHtmlRep.GetSingle(selectType, guid);
            if (StaticHtmlDBViewModel == null) StaticHtmlDBViewModel = new StaticHtmlDBViewModel();
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            DetailViewModel = mapper.Map<StaticHtmlDetailViewModel>(StaticHtmlDBViewModel);

            /*Enum手動綁進*/
            DetailViewModel.StaticHtmlActionType = selectType;
            return DetailViewModel;
        }

        /// <summary>
        /// ViewModel To DBModel
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private StaticHtml ReturnMappingStaticHtml(StaticHtmlDetailViewModel viewModel)
        {
            StaticHtml dbViewModel = new StaticHtml();
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            dbViewModel = mapper.Map<StaticHtml>(viewModel);
            return dbViewModel;
        }

        /// <summary>
        /// Create StaticHtml Info
        /// </summary>
        /// <param name="statichtml"></param>
        public string CreateStaticHtml(StaticHtmlDetailViewModel statichtml, string userName)
        {
            try
            {
                StaticHtml PartStaticHtml = ReturnMappingStaticHtml(statichtml);
                PartStaticHtml.SubjectID = statichtml.StaticHtmlActionType.ToString();
                _StaticHtmlRep.StaticHtmlInsertInto(PartStaticHtml, userName);

                return EnumHelper.GetEnumDescription(DataAction.CreateScuess);
            }
            catch
            {
                return EnumHelper.GetEnumDescription(DataAction.CreateFail);
            }
        }

        /// <summary>
        /// Update StaticHtml
        /// </summary>
        /// <param name="statichtml"></param>
        /// <returns></returns>
        public string UpdateStaticHtml(StaticHtmlDetailViewModel statichtml, string userName)
        {
            try
            {
                StaticHtml PartStaticHtml = ReturnMappingStaticHtml(statichtml);
                PartStaticHtml.SubjectID = statichtml.StaticHtmlActionType.ToString();
                _StaticHtmlRep.StaticHtmlUpdate(PartStaticHtml, userName);
                return EnumHelper.GetEnumDescription(DataAction.UpdateScuess);
            }
            catch
            {
                return EnumHelper.GetEnumDescription(DataAction.UpdateFail);
            }
        }

        /// <summary>
        /// Create Pic
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="PicGroupID"></param>
        /// <param name="UserName"></param>
        public void CreatePictureInfo(IEnumerable<HttpPostedFileBase> upload, Guid PicGroupID, string UserName)
        {
            //1.編號PicGroupID
            //Guid PicgroupId;
            //if (string.IsNullOrWhiteSpace(PicGroupID))
            //{
            //    PicgroupId = Guid.NewGuid();
            //}
            //else PicgroupId = Guid.Parse(PicGroupID);

            //2.取得 sort 累加
            int DbSaveCount = _StaticHtmlRep.TableMaxCountByID(TableName.PictureInfo, PicGroupID);

            _PicInfoRep.PictureInfoInsertInto(upload, PicGroupID, DbSaveCount, UserName);
        }

        /// <summary>
        /// Deletes the picture information.
        /// </summary>
        /// <param name="PicID">The pic identifier.</param>
        /// <returns></returns>
        public string DeletePictureInfo(string PicID, string userName)
        {
            try
            {
                _PicInfoRep.PictureInfoUpdate(PicID, userName);
                return EnumHelper.GetEnumDescription(DataAction.Update);
            }
            catch
            {
                return EnumHelper.GetEnumDescription(DataAction.UpdateFail);
            }
        }

        /// <summary>
        /// Deletes the static HTML.
        /// </summary>
        /// <param name="HtmlID">The HTML identifier.</param>
        /// <returns></returns>
        public string DeleteStaticHtml(string HtmlID, string userName)
        {
            try
            {
                Guid newGuid = Guid.Parse(HtmlID);
                StaticHtml statichtml = _StaticHtmlRep.GetSingle(s => s.StaticID.Equals(newGuid));
                _StaticHtmlRep.StaticHtmlUpdate(statichtml, userName);
                return EnumHelper.GetEnumDescription(DataAction.DeleteScuess);
            }
            catch
            {
                return EnumHelper.GetEnumDescription(DataAction.DeleteFail);
            }
        }

        /// <summary>
        /// Gets the picture information.
        /// </summary>
        /// <param name="PicID">The pic identifier.</param>
        /// <returns></returns>
        public IEnumerable<PictureInfo> GetPictureInfo(string PicID)
        {
            Guid newPicID = Guid.Parse(PicID);
            PictureInfo picGroupid = _PicInfoRep.GetSingle(s => s.PicID == newPicID);
            IEnumerable<PictureInfo> PictureInfoList = _PicInfoRep.Query(s => s.PicGroupID.Equals(picGroupid.PicGroupID) && s.Status == true);
            return PictureInfoList;
        }

        public void Save()
        {
            //_AspNetUsersRep.Commit();
            _unitOfWork.Save();
        }
    }
}