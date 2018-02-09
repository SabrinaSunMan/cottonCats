using StoreDB.Interface;
using StoreDB.Model.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Strategy
{
    //public abstract class AspNetUsersStrategy
    //{
    //    private readonly IRepository<AspNetUsers> _AspNetUsersRep;

    //    protected string guId;
    //    protected string userName;
    //    protected string userEmail;

    //    /// <summary>
    //    /// 取得所有管理者 List.
    //    /// </summary>
    //    /// <returns></returns>
    //    public IEnumerable<AspNetUsers> GetAllAspNetUsers()
    //    {
    //        IEnumerable<AspNetUsers> GetAspNetUsers = _AspNetUsersRep.GetAll().OrderByDescending(s => s.UserName);
    //        return GetAspNetUsers;
    //    }

    //    /// <summary>
    //    /// 藉由參數取得管理者資訊 Single.
    //    /// </summary>
    //    /// <returns></returns>
    //    public AspNetUsers GetAspNetUserBySelectPramters()
    //    {
    //        AspNetUsers GetAspNetUsers =
    //        _AspNetUsersRep.GetAll().Where(s => s.UserName == (string.IsNullOrEmpty(userName) ? s.UserName : userName)
    //        && s.Email == (string.IsNullOrEmpty(userEmail) ? s.Email : userEmail)).First();
    //        return GetAspNetUsers;
    //    }

    //    /// <summary>
    //    /// 藉由 guid 取得管理者資訊 Single.
    //    /// </summary>
    //    /// <param name="guid">The unique identifier.</param>
    //    /// <returns></returns>
    //    public AspNetUsers GetAspNetUsersById()
    //    {
    //        return _AspNetUsersRep.GetSingle(s => s.Id == guId);
    //    }
    //}

    //internal sealed class GetAspNetUsersByParameters : AspNetUsersStrategy
    //{
    //    public GetAspNetUsersByParameters(string UserName, string UserEmail)
    //    { 
    //        userName = UserName;
    //        userEmail = UserEmail;
    //    } 
    //}
}
