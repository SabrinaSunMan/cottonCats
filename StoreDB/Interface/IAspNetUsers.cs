using StoreDB.Model.Partials;
using StoreDB.Repositories;
using System.Collections.Generic;
 
namespace StoreDB.Interface
{
    public interface IAspNetUsers : IRepository<AspNetUsers>
    {
        //AspNetUsersRepository Get
        //AspNetUsersRepository GetAspNetUser(string UserName, string UserEmail);
        //void UpdateAspNetUsers(AspNetUsers data);
    }
}
