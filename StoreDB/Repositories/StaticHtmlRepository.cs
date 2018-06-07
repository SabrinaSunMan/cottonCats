using StoreDB.Enum;
using StoreDB.Interface;
using StoreDB.Model.Partials;
using StoreDB.Model.ViewModel.BackcottonCats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Repositories
{
    public class StaticHtmlRepository : Repository<StaticHtml>
    {
        private readonly IRepository<HtmlSubject> _HtmlSubject;
        private readonly IRepository<PictureInfo> _PictureInfo;
        private readonly IRepository<StaticHtml> _StaticHtml;

        public StaticHtmlRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _HtmlSubject = new Repository<HtmlSubject>(unitOfWork);
            _PictureInfo = new Repository<PictureInfo>(unitOfWork);
            _StaticHtml = new Repository<StaticHtml>(unitOfWork);
        }

        /// <summary>
        /// 統一藉由此ViewModel溝通將 三張Table串接 StaticHtmlViewModel
        /// </summary>
        /// <param name="SelectTypes">Html型態</param>
        /// <returns></returns>
        public IEnumerable<StaticHtmlDBViewModel> GetViewModel(StaticHtmlAction SelectTypes)
        {
            return ReturnViewModel(SelectTypes);
        }

        /// <summary>
        /// 統一藉由此ViewModel溝通將 三張Table串接 StaticHtmlViewModel
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StaticHtmlDBViewModel> GetViewModel()
        {
            return ReturnViewModel(StaticHtmlAction.All);
        }

        /// <summary>
        /// 藉由 guid 搜尋該ViewModel
        /// </summary>
        /// <returns></returns>
        public StaticHtmlDBViewModel GetSingle(StaticHtmlAction SelectTypes,string guid)
        {
            return ReturnViewModel(SelectTypes).Where(s=>s.StaticID.Equals(guid)).FirstOrDefault();
        }

        private IEnumerable<StaticHtmlDBViewModel> ReturnViewModel(StaticHtmlAction SelectTypes)
        {
            // 取得 靜態網頁主檔 Table
            List<StaticHtml> HtmlInfoList = _StaticHtml.GetAll().ToList();
            // 取得 圖片資訊
            List<PictureInfo> PicInfoList = _PictureInfo.GetAll().ToList();
            // 取得 靜態分類
            List<HtmlSubject> SubjectInfoList = _HtmlSubject.GetAll().ToList();
            IEnumerable<StaticHtmlDBViewModel> ReturnViewModel = from HtmlInfo in HtmlInfoList
                                                                 //join PicInfo in PicInfoList
                                                             //on HtmlInfo.PicGroupID equals PicInfo.PicGroupID
                                                                 join SubjectInfo in SubjectInfoList
                                                             on HtmlInfo.SubjectID equals SubjectInfo.SubjectID
                                                                 //where SubjectInfo.SubjectID == ""
                                                                 orderby HtmlInfo.CreateTime
                                                                 select new
                                                                 StaticHtmlDBViewModel
                                                                 {
                                                                     //FileExtension = PicInfo.FileExtension,
                                                                     HtmlContext = HtmlInfo.HtmlContext,
                                                                     //PicID = PicInfo.PicID.ToString(),
                                                                     //PictureName = PicInfo.PictureName,
                                                                     //PictureUrl = PicInfo.PictureUrl,
                                                                     StaticID = HtmlInfo.StaticID.ToString(),
                                                                     SubjectID = SubjectInfo.SubjectID,
                                                                     SubjectName = SubjectInfo.SubjectName,

                                                                     CreateTime = HtmlInfo.CreateTime,
                                                                     CreateUser = HtmlInfo.CreateUser,
                                                                     sort = HtmlInfo.sort,
                                                                     Status = HtmlInfo.Status,
                                                                     UpdateTime = HtmlInfo.UpdateTime,
                                                                     UpdateUser = HtmlInfo.UpdateUser,
                                                                     picInfo = PicInfoList.Where(s=>s.PicGroupID.Equals(HtmlInfo.PicGroupID))
                                                                 };
            if (SelectTypes!= StaticHtmlAction.All)
            {
                ReturnViewModel = ReturnViewModel.Where(s => s.SubjectID.Equals(SelectTypes.ToString()));
            }

            return ReturnViewModel.OrderByDescending(s=>s.CreateTime);
        }

        /// <summary>
        /// Returns the picture information list.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public IQueryable<PictureInfo> ReturnPictureInfoList(string groupguid)
        {
            return _PictureInfo.Query(s => s.PicGroupID.Equals(groupguid));
        }

        /// <summary>
        /// 更新 StaticHtml
        /// </summary>
        /// <param name="statichtml">The aspuser.</param>
        /// <param name="keyValues">The key values.</param>
        public void StaticHtmlUpdate(StaticHtml statichtml, params object[] keyValues)
        {
            StaticHtml ReadyUpdate = GetSingle(s => s.StaticID.Equals(statichtml.StaticID));
            ReadyUpdate.HtmlContext = statichtml.HtmlContext;
            //ReadyUpdate.PicID = statichtml.PicID;
            ReadyUpdate.sort = statichtml.sort;
            ReadyUpdate.Status = statichtml.Status;
            //ReadyUpdate.SubjectID = statichtml.SubjectID;
            ReadyUpdate.UpdateUser = statichtml.UpdateUser;
            ReadyUpdate.UpdateTime = DateTime.Now;
            Update(ReadyUpdate, statichtml.StaticID);
        }

        public void SavePictureInfo (PictureInfo savedata)
        {
            _PictureInfo.Create(savedata);
        }
    }
}
