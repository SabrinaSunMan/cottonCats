using BackMeow.App_Start;
using BackMeow.Models.ViewModel;
using PagedList;
using StoreDB.Enum;
using StoreDB.Interface;
using StoreDB.Model.Partials;
using StoreDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackMeow.Service
{
    public class ActivitiesService
    {
        private readonly ActitiesRepository _ActityRep;
        private readonly IUnitOfWork _unitOfWork;
        private readonly int pageSize = (int)BackPageListSize.commonSize;

        public ActivitiesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ActityRep = new ActitiesRepository(unitOfWork);
        }

        /// <summary>
        /// 取得所有活動紀錄.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Activity> GetAllActivity()
        {
            IEnumerable<Activity> GetActivity = _ActityRep.GetAll().OrderByDescending(s => s.CreateTime).ToList();
            return GetActivity;
        }

        /// <summary>
        /// Gets the system roles ListView model.
        /// </summary>
        /// <param name="selectModel">The select model.</param>
        /// <param name="nowpage">The nowpage.</param>
        /// <returns></returns>
        public ActitiesViewModel GetActitiesListViewModel(ActitiesListHeaderViewModel selectModel, int nowpage = 1)
        {
            ActitiesViewModel returnActitiesListViewModel = new ActitiesViewModel();
            returnActitiesListViewModel.Header = selectModel; /*表頭*/
            IEnumerable<ActitiesListContentViewModel> GetAllActitiesListViewModelResult = GetAllActitiesListViewModel(selectModel);
            int currentPage = (nowpage < 1) && GetAllActitiesListViewModelResult.Count() >= 1 ? 1 : nowpage;
            returnActitiesListViewModel.Content_List = GetAllActitiesListViewModelResult.ToPagedList(currentPage, pageSize);/*內容*/
            return returnActitiesListViewModel;
        }

        /// <summary>
        /// Gets all system roles ListView model.
        /// </summary>
        /// <param name="selectModel">The select model.</param>
        /// <returns></returns>
        private IEnumerable<ActitiesListContentViewModel> GetAllActitiesListViewModel(ActitiesListHeaderViewModel selectModel)
        {
            //ActitiesListContentViewModel = 網頁要顯示的欄位抓取
            //此動作目的在於不顯示過多的資訊至網頁上，進行欄位Mapping動作
            IEnumerable<ActitiesListContentViewModel> ReturnList = GetAllActivity().
                Where(s => (!string.IsNullOrEmpty(selectModel.TitleName) ?
            s.TitleName.Contains(selectModel.TitleName) : s.TitleName == s.TitleName)

            && (!string.IsNullOrWhiteSpace(selectModel.HtmlContext) ?
            s.HtmlContext.Contains(selectModel.HtmlContext) : s.HtmlContext == s.HtmlContext)

            && (!string.IsNullOrWhiteSpace(selectModel.CreateTime) ?
            s.CreateTime.ToString() == selectModel.CreateTime : s.CreateTime == s.CreateTime
            )

            && (s.StartDate >= Convert.ToDateTime(selectModel.StartDate)
                    && s.EndDate <= Convert.ToDateTime(selectModel.EndDate)
            )).Select(s => new ActitiesListContentViewModel()
            {
                ActitiesID = s.ActivityID,
                TitleName = s.TitleName,
                HtmlContext = s.HtmlContext,
                CreateTime = s.CreateTime.ToString(),
                STime = s.StartDate.ToString(),
                ETime = s.EndDate.ToString(),
                Status = s.Status.ToString()
            });
            List<ActitiesListContentViewModel> a1 = ReturnList.ToList();
            return ReturnList;
            //return result;
        }

        public ActitiesDetailViewModel ReturnActitiesDetailViewModel(Actions ActionType, string guid)
        {
            ActitiesDetailViewModel DetailViewModel = new ActitiesDetailViewModel();
            Activity StaticHtmlDBViewModel = _ActityRep.GetSingle(s => s.ActivityID == guid);
            if (StaticHtmlDBViewModel == null) StaticHtmlDBViewModel = new Activity();
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            DetailViewModel = mapper.Map<ActitiesDetailViewModel>(StaticHtmlDBViewModel);
            return DetailViewModel;
        }

        /// <summary>
        /// Create StaticHtml Info
        /// </summary>
        /// <param name="statichtml"></param>
        public string Create(ActitiesDetailViewModel actity, string userName)
        {
            try
            {
                Activity PartStaticHtml = ReturnMappingActity(actity);
                _ActityRep.ActitiesInsertInto(PartStaticHtml, userName);

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
        public string Update(ActitiesDetailViewModel statichtml, string userName)
        {
            try
            {
                Activity PartStaticHtml = ReturnMappingActity(statichtml);
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

        /// <summary>
        /// ViewModel To DBModel
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private Activity ReturnMappingActity(ActitiesDetailViewModel viewModel)
        {
            Activity dbViewModel = new Activity();
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            dbViewModel = mapper.Map<Activity>(viewModel);
            return dbViewModel;
        }

        public void Save()
        {
            //_AspNetUsersRep.Commit();
            _unitOfWork.Save();
        }
    }
}