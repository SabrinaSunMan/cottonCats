using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// 一般註冊使用者
    /// </summary>
    public class Users 
    {
        /*前台使用者資料*/ 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

    }
     
}
