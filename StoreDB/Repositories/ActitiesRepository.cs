using StoreDB.Interface;
using StoreDB.Model.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}