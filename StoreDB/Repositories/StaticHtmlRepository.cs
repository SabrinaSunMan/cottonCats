using StoreDB.Enum;
using StoreDB.Interface;
using StoreDB.Model.Partials;
using StoreDB.Model.ViewModel.BackcottonCats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreDB.Repositories
{
    public class StaticHtmlRepository : Repository<StaticHtml>
    {
        private readonly IRepository<HtmlSubject> _HtmlSubject;
        private readonly IRepository<PictureInfo> _PictureInfo;
        private readonly IRepository<StaticHtml> _StaticHtml;
        private readonly IRepository<AspNetUsers> _AspNetUsersRep;

        public StaticHtmlRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _HtmlSubject = new Repository<HtmlSubject>(unitOfWork);
            _PictureInfo = new Repository<PictureInfo>(unitOfWork);
            _StaticHtml = new Repository<StaticHtml>(unitOfWork);
            _AspNetUsersRep = new Repository<AspNetUsers>(unitOfWork);
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
        public StaticHtmlDBViewModel GetSingle(StaticHtmlAction SelectTypes, string guid)
        {
            // 取得 靜態網頁主檔 Table
            List<StaticHtml> HtmlInfoList = _StaticHtml.GetAll().ToList();
            // 取得 圖片資訊
            List<PictureInfo> PicInfoList = _PictureInfo.GetAll().ToList();
            // 取得 靜態分類
            List<HtmlSubject> SubjectInfoList = _HtmlSubject.GetAll().ToList();

            List<AspNetUsers> AspNetUsersList = _AspNetUsersRep.GetAll().ToList();

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
                                                                     HtmlContext = HtmlInfo.HtmlContext,
                                                                     StaticID = HtmlInfo.StaticID.ToString(),
                                                                     SubjectID = SubjectInfo.SubjectID,
                                                                     SubjectName = SubjectInfo.SubjectName,
                                                                     PicGroupID = HtmlInfo.PicGroupID.ToString(),
                                                                     CreateTime = HtmlInfo.CreateTime,
                                                                     CreateUser = AspNetUsersList.Where(s => s.Id.Equals(HtmlInfo.CreateUser)).FirstOrDefault().UserName,
                                                                     sort = HtmlInfo.sort,
                                                                     Status = HtmlInfo.Status,
                                                                     UpdateTime = HtmlInfo.UpdateTime,
                                                                     UpdateUser = AspNetUsersList.Where(s => s.Id.Equals(HtmlInfo.UpdateUser)).FirstOrDefault().UserName,
                                                                     picInfo = PicInfoList.Where(s => s.PicGroupID.Equals(HtmlInfo.PicGroupID) && s.Status == true)
                                                                 };
            return ReturnViewModel.Where(s => s.StaticID == guid.ToLower()).FirstOrDefault();
            /*日後記得將此 string 與 Guid 做明顯區分避免會有資料因大小寫而找不到的問題產生*/
        }

        private IEnumerable<StaticHtmlDBViewModel> ReturnViewModel(StaticHtmlAction SelectTypes)
        {
            // 取得 靜態網頁主檔 Table
            List<StaticHtml> HtmlInfoList = _StaticHtml.GetAll().ToList();
            // 取得 圖片資訊
            List<PictureInfo> PicInfoList = _PictureInfo.GetAll().ToList();
            // 取得 靜態分類
            List<HtmlSubject> SubjectInfoList = _HtmlSubject.GetAll().ToList();

            List<AspNetUsers> AspNetUsersList = _AspNetUsersRep.GetAll().ToList();

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
                                                                     HtmlContext = HtmlInfo.HtmlContext.Length > 25 ? HtmlInfo.HtmlContext.Substring(0, 25) + "..." : HtmlInfo.HtmlContext.ToString(), /*只固定顯示 25個字 */
                                                                     StaticID = HtmlInfo.StaticID.ToString(),
                                                                     SubjectID = SubjectInfo.SubjectID,
                                                                     SubjectName = SubjectInfo.SubjectName,
                                                                     PicGroupID = HtmlInfo.PicGroupID.ToString(),
                                                                     CreateTime = HtmlInfo.CreateTime,
                                                                     CreateUser = AspNetUsersList.Where(s => s.Id.Equals(HtmlInfo.CreateUser)).FirstOrDefault().UserName,
                                                                     sort = HtmlInfo.sort,
                                                                     Status = HtmlInfo.Status,
                                                                     UpdateTime = HtmlInfo.UpdateTime,
                                                                     UpdateUser = AspNetUsersList.Where(s => s.Id.Equals(HtmlInfo.UpdateUser)).FirstOrDefault().UserName,
                                                                     picInfo = PicInfoList.Where(s => s.PicGroupID.Equals(HtmlInfo.PicGroupID) && s.Status == true)
                                                                 };

            if (SelectTypes != StaticHtmlAction.All)
            {
                ReturnViewModel = ReturnViewModel.Where(s => s.SubjectID.Equals(SelectTypes.ToString()));
            }

            return ReturnViewModel.OrderByDescending(s => s.CreateTime);
        }

        /// <summary>
        /// 更新 StaticHtml
        /// </summary>
        /// <param name="statichtml">The aspuser.</param>
        public void StaticHtmlUpdate(StaticHtml statichtml, string userName) //, params object[] keyValues
        {
            //1.取得目前使用者 ID
            AspNetUsers AspNetusers = _AspNetUsersRep.Query(s => s.UserName.Equals(userName)).FirstOrDefault();//登入的使用者帳號
            StaticHtml ReadyUpdate = GetSingle(s => s.StaticID.Equals(statichtml.StaticID));
            ReadyUpdate.HtmlContext = statichtml.HtmlContext;
            //ReadyUpdate.PicID = statichtml.PicID;
            ReadyUpdate.sort = statichtml.sort;
            ReadyUpdate.Status = statichtml.Status;
            ReadyUpdate.UpdateUser = AspNetusers.Id;
            ReadyUpdate.UpdateTime = DateTime.Now;
            Update(ReadyUpdate, statichtml.StaticID);
        }

        public void StaticHtmlInsertInto(StaticHtml statichtml, string userName)
        {
            //1.取得目前使用者 ID
            AspNetUsers AspNetusers = _AspNetUsersRep.Query(s => s.UserName.Equals(userName)).FirstOrDefault();//登入的使用者帳號
            statichtml.UpdateTime = DateTime.Now;
            statichtml.CreateTime = DateTime.Now;
            statichtml.CreateUser = AspNetusers.Id;
            statichtml.UpdateUser = AspNetusers.Id;
            _StaticHtml.Create(statichtml);
        }

        /// <summary>
        /// Tables the maximum count by identifier.
        /// </summary>
        /// <param name="selectTableName">Name of the select table.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public int TableMaxCountByID(TableName selectTableName, Guid guid)
        {
            switch (selectTableName)
            {
                case TableName.PictureInfo:
                    return _PictureInfo.Query(s => s.PicGroupID == guid).Count() + 1;

                case TableName.StaticHtml:
                    return _StaticHtml.Query(s => s.StaticID == guid).Count() + 1;

                default:
                    return 0;
            }
        }

        //public void SavePictureInfo(PictureInfo savedata)
        //{
        //    _PictureInfo.Create(savedata);
        //}
    }
}