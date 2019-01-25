using StoreDB.Interface;
using System;
using StoreDB.Model.Partials;
using System.Linq;
using System.Linq.Expressions;

namespace StoreDB.Repositories
{
    public class MemeberRepository : Repository<Member>
    {
        private readonly IRepository<Member> _MemberRep;

        public MemeberRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _MemberRep = new Repository<Member>(unitOfWork);
        }

        /// <summary>
        /// 建立使用者.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="Email"></param>
        public void MemberInsertInto(Member member, string CreateUser)
        {
            member.CreateUser = CreateUser;
            member.UpdateUser = CreateUser;
            Create(member);
        }

        /// <summary>
        /// 更新使用者.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="userName"></param>
        public void MemberUpdate(Member member, string UpdateUser) //, params object[] keyValues
        {
            //1.取得目前使用者 ID
            Member ReadyUpdate = GetSingle(s => s.MemberID.Equals(member.MemberID));
            ReadyUpdate.Address = member.Address;
            ReadyUpdate.PhoneNumber = member.PhoneNumber;
            ReadyUpdate.ZipCodeID = member.ZipCodeID;
            ReadyUpdate.Status = member.Status;
            ReadyUpdate.UpdateUser = UpdateUser;
            ReadyUpdate.UpdateTime = DateTime.Now;
            Update(ReadyUpdate, member.MemberID);
        }

        /// <summary>
        /// Gets the match bool.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        //public bool GetMatchBool(Expression<Func<Member, bool>> filter)
        //{
        //    Member memberInfo = GetSingle(filter);
        //    return memberInfo == null ? false : true;
        //}
    }
}