using BackMeow.BackEnum;
using StoreDB.Model.Partials;
using StoreDB.Repositories;

namespace BackMeow.Service
{
    //取得使用者資料 
    public class AspNetUsersService
    {
        Repository<AspNetUsers> AspNetUsers = new Repository<AspNetUsers>();
        private readonly int pageSize = (int)PageListSize.commonSize;

    }
}