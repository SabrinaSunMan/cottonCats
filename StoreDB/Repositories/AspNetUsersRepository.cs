using StoreDB.Interface;
using StoreDB.Strategy;
using System;

namespace StoreDB.Repositories
{
    /// <summary>
    /// 只針對取資料的部份抽離
    /// </summary>
    /// <seealso cref="StoreDB.Interface.IAspNetUsers" />
    //public class AspNetUsersRepository : IAspNetUsers
    //{
    //    /// <summary>
    //    /// Gets the ASP net user.
    //    /// </summary>
    //    /// <param name="UserName">Name of the user.</param>
    //    /// <param name="UserEmail">The user email.</param>
    //    /// <returns></returns>
    //    /// <exception cref="NotImplementedException"></exception>
    //    public AspNetUsersRepository GetAspNetUser(string UserName, string UserEmail)
    //    {
    //        return new GetAspNetUsersByParameters(UserName, UserEmail);
    //    }
    //}
}
