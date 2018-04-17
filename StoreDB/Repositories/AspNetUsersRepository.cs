using StoreDB.Interface;
using StoreDB.Strategy;
using System;
using StoreDB.Model.Partials;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
        /// 更新 AspNetUsers
        /// </summary>
        /// <param name="aspuser">The aspuser.</param>
        /// <param name="keyValues">The key values.</param>
        public void AspNetUserUpdate(AspNetUsers aspuser, params object[] keyValues)
        { 
            AspNetUsers ReadyUpdate = GetSingle(s => s.Id.Equals(aspuser.Id));
            ReadyUpdate.Email = aspuser.Email;
            ReadyUpdate.UserName = aspuser.UserName;
            ReadyUpdate.PhoneNumber = aspuser.PhoneNumber;
            ReadyUpdate.UpdateTime = DateTime.Now;
            Update(ReadyUpdate, aspuser.Id); 
        }
    }
}
