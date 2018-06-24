using StoreDB.Interface;
using StoreDB.Model.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace StoreDB.Repositories
{
    public class PictureInfoRepository : Repository<PictureInfo>
    {
        private readonly IRepository<PictureInfo> _PictureInfo;
        private readonly IRepository<AspNetUsers> _AspNetUsersRep;
        private string FileUrl = WebConfigurationManager.AppSettings["UploadFileUrl"];

        public PictureInfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _PictureInfo = new Repository<PictureInfo>(unitOfWork);
            _AspNetUsersRep = new AspNetUsersRepository(unitOfWork);
        }

        /// <summary>
        /// Pictures the information insert into.
        /// </summary>
        /// <param name="upload">The upload.</param>
        /// <param name="StaticID">The static identifier.</param>
        /// <param name="nowsort">The nowsort.</param>
        /// <param name="userName">Name of the user.</param>
        public void PictureInfoInsertInto(IEnumerable<HttpPostedFileBase> upload, Guid groupID, int nowsort, string userName)
        {
            //1.取得目前使用者 ID
            AspNetUsers AspNetusers = _AspNetUsersRep.Query(s => s.UserName.Equals(userName)).FirstOrDefault();//登入的使用者帳號

            foreach (var item in upload)
            {
                PictureInfo saveData = new PictureInfo()
                {
                    CreateUser = AspNetusers.Id,
                    FileExtension = System.IO.Path.GetExtension(item.FileName).ToUpper(),
                    PicGroupID = groupID,
                    PicID = Guid.NewGuid(),
                    PictureName = item.FileName.Substring(0, item.FileName.IndexOf(".")),
                    sort = nowsort,
                    Status = true,
                    PictureUrl = FileUrl + groupID.ToString().ToUpper() + "/",
                    UpdateUser = AspNetusers.Id,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };
                nowsort++;
                _PictureInfo.Create(saveData);
            }
        }

        /// <summary>
        /// Pictures the information update.
        /// </summary>
        /// <param name="picInfo">The pic information.</param>
        public void PictureInfoUpdate(string picInfo, string userName)
        {
            //1.取得目前使用者 ID
            AspNetUsers AspNetusers = _AspNetUsersRep.Query(s => s.UserName.Equals(userName)).FirstOrDefault();//登入的使用者帳號
            Guid newa = Guid.Parse(picInfo);
            PictureInfo ReadyUpdate = _PictureInfo.GetSingle(s => s.PicID.Equals(newa));
            ReadyUpdate.Status = false;
            ReadyUpdate.UpdateUser = AspNetusers.Id;
            ReadyUpdate.UpdateTime = DateTime.Now;
            Update(ReadyUpdate, newa);
        }
    }
}