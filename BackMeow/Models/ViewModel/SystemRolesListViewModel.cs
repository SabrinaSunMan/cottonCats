using PagedList;
using StoreDB.Enum;
using StoreDB.Model.Partials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BackMeow.Models.ViewModel
{
    /// <summary>
    /// 包括 表頭以及 PageList
    /// </summary>
    public class SystemRolesListViewModel
    {
        public SystemRolesListHeaderViewModel Header { get; set; }

        public IPagedList<SystemRolesListContentViewModel> Content_List { get; set; }

        public int page { get; set; }
    }

    /// <summary>
    /// 搜尋 [AspNetUsers] 條件式
    /// </summary>
    public class SystemRolesListHeaderViewModel
    {
        [DisplayName("帳號")]
        public string Email { get; set; }

        [DisplayName("使用者名稱")]
        public string UserName { get; set; }
    }

    /// <summary>
    /// 呈現 [AspNetUsers] 搜尋結果
    /// </summary>
    public class SystemRolesListContentViewModel
    {
        public string Id { get; set; }

        [DisplayName("帳號")]
        public string Email { get; set; }

        [DisplayName("使用者名稱")]
        public string UserName { get; set; }

        [DisplayName("電話號碼")]
        public string PhoneNumber { get; set; }

        public bool LockoutEnabled { get; set; }

    }

    /// <summary>
    /// 呈現 [AspNetUsers] 詳細資訊
    /// </summary>
    public class AspNetUsersDetailViewModel : AspNetUsers
    {
        /// <summary>
        /// 針對資料要處理的動作
        /// </summary>
        public Actions ActionType { get; set; } 
    }
}