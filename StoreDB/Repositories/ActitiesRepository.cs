using StoreDB.Interface;
using StoreDB.Model.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using StoreDB.Model.ViewModel.BackcottonCats;

namespace StoreDB.Repositories
{
    public class ActitiesRepository : Repository<Activity>
    {
        private readonly IRepository<AspNetUsers> _AspNetUsersRep;
        private readonly IRepository<Activity> _Activity;
        private readonly IRepository<PictureInfo> _PictureInfo;

        public ActitiesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _Activity = new Repository<Activity>(unitOfWork);
            _PictureInfo = new Repository<PictureInfo>(unitOfWork);
            _AspNetUsersRep = new Repository<AspNetUsers>(unitOfWork);
        }

        public void ActitiesInsertInto(Activity activity, string userName)
        {
            //1.取得目前使用者 ID
            AspNetUsers AspNetusers = _AspNetUsersRep.Query(s => s.UserName.Equals(userName)).FirstOrDefault();//登入的使用者帳號
            activity.UpdateTime = DateTime.Now;
            activity.CreateTime = DateTime.Now;
            activity.CreateUser = AspNetusers.Id;
            activity.UpdateUser = AspNetusers.Id;
            _Activity.Create(activity);
        }

        /// <summary>
        /// 藉由 guid 搜尋該ViewModel
        /// </summary>
        /// <returns></returns>
        public ActitiesDBViewModel GetSingle(string guid)
        {
            // 取得 圖片資訊
            List<PictureInfo> PicInfoList = _PictureInfo.GetAll().ToList();
            // 取得 活動明細
            List<Activity> ActivitiesList = _Activity.GetAll().ToList();

            List<AspNetUsers> AspNetUsersList = _AspNetUsersRep.GetAll().ToList();

            IEnumerable<ActitiesDBViewModel> ReturnViewModel = from ActivitiestInfo in ActivitiesList
                                                               orderby ActivitiestInfo.CreateTime
                                                               select new
                                                               ActitiesDBViewModel
                                                               {
                                                                   HtmlContext = ActivitiestInfo.HtmlContext.Length > 25 ? ActivitiestInfo.HtmlContext.Substring(0, 25) + "..." : ActivitiestInfo.HtmlContext.ToString(), /*只固定顯示 25個字 */
                                                                   ActivityID = ActivitiestInfo.ActivityID,
                                                                   StartDate = ActivitiestInfo.StartDate,
                                                                   EndDate = ActivitiestInfo.EndDate,
                                                                   TitleName = ActivitiestInfo.TitleName,
                                                                   PicGroupID = ActivitiestInfo.PicGroupID,
                                                                   CreateTime = ActivitiestInfo.CreateTime,
                                                                   CreateUser = AspNetUsersList.Where(s => s.Id.Equals(ActivitiestInfo.CreateUser)).FirstOrDefault().UserName,
                                                                   sort = ActivitiestInfo.sort,
                                                                   Status = ActivitiestInfo.Status,
                                                                   UpdateTime = ActivitiestInfo.UpdateTime,
                                                                   UpdateUser = AspNetUsersList.Where(s => s.Id.Equals(ActivitiestInfo.UpdateUser)).FirstOrDefault().UserName,
                                                                   picInfo = PicInfoList.Where(s => s.PicGroupID.Equals(ActivitiestInfo.PicGroupID) && s.Status == true)
                                                               };
            return ReturnViewModel.Where(s => s.ActivityID == guid.ToLower()).FirstOrDefault();
            /*日後記得將此 string 與 Guid 做明顯區分避免會有資料因大小寫而找不到的問題產生*/
        }
    }
}