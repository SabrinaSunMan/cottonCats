using StoreDB.Interface;
using System;
using StoreDB.Model.Partials;
using System.Linq;

namespace StoreDB.Repositories
{
    public class MemeberRepository : Repository<Member>
    {
        private readonly IRepository<AspNetUsers> _AspNetUsersRep;

        public MemeberRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _AspNetUsersRep = new Repository<AspNetUsers>(unitOfWork);
        }

        /// <summary>
        /// 建立使用者.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="userName"></param>
        public void MemberInsertInto(Member member, string userName)
        {
            //1.取得目前使用者 ID
            AspNetUsers AspNetusers = _AspNetUsersRep.Query(s => s.UserName.Equals(userName)).FirstOrDefault();//登入的使用者帳號
            member.UpdateTime = DateTime.Now;
            member.CreateTime = DateTime.Now;
            member.CreateUser = AspNetusers.Id;
            member.UpdateUser = AspNetusers.Id;
            Create(member);
        }

        /// <summary>
        /// 更新使用者.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="userName"></param>
        public void MemberUpdate(Member member, string userName) //, params object[] keyValues
        {
            //1.取得目前使用者 ID
            AspNetUsers AspNetusers = _AspNetUsersRep.Query(s => s.UserName.Equals(userName)).FirstOrDefault();//登入的使用者帳號
            Member ReadyUpdate = GetSingle(s => s.MemberID.Equals(member.MemberID));
            ReadyUpdate.Address = member.Address;
            ReadyUpdate.PhoneNumber = member.PhoneNumber;
            ReadyUpdate.City = member.City;
            ReadyUpdate.County = member.County;
            ReadyUpdate.Status = member.Status;
            ReadyUpdate.UpdateUser = AspNetusers.Id;
            ReadyUpdate.UpdateTime = DateTime.Now;
            Update(ReadyUpdate, member.MemberID);
        }
    }
}