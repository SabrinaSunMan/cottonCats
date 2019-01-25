using StoreDB.Interface;
using StoreDB.Model.Partials;

namespace StoreDB.Repositories
{
    /// <summary>
    /// 只針對取資料的部份抽離
    /// </summary>
    /// <seealso cref="StoreDB.Interface.IAspNetUsers" />
    public class AspNetUsersRepository : Repository<AspNetUsers>
    {
        public AspNetUsersRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 更新 AspNetUsers.
        /// </summary>
        /// <param name="aspuser">The aspuser.</param>
        /// <param name="CreateUser">The create user.</param>
        public void AspNetUserUpdate(AspNetUsers aspuser, string CreateUser)
        {
            AspNetUsers ReadyUpdate = GetSingle(s => s.Id.Equals(aspuser.Id));
            ReadyUpdate.Email = aspuser.Email;
            ReadyUpdate.UserName = aspuser.UserName;
            ReadyUpdate.PhoneNumber = aspuser.PhoneNumber;
            Update(ReadyUpdate, CreateUser);
        }
    }
}