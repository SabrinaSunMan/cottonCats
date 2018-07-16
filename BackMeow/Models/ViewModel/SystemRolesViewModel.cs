using PagedList;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackMeow.Models.ViewModel
{
    /// <summary>
    /// 包括 表頭以及 PageList
    /// </summary>
    public class SystemRolesViewModel
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
        [DisplayName("使用者ID")]
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
    /// 呈現 [AspNetUsers] 檢視頁面 時
    /// </summary>
    public class AspNetUsersDetailViewModel
    {
        [DisplayName("使用者ID")]
        public string Id { get; set; }

        [Required(ErrorMessage = " {0} 為必填")]
        [StringLength(256)]
        [EmailAddress(ErrorMessage = "非E-Mail格式")]
        public string Email { get; set; }

        [DisplayName("舊密碼")]
        public string Old_Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相符。")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = " {0} 為必填")]
        [StringLength(256)]
        [DisplayName("使用者名稱")]
        public string UserName { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "電話格式不對，請輸入數字")]
        [DisplayName("電話號碼")]
        public string PhoneNumber { get; set; }

        [DisplayName("使用者建立時間")]
        public DateTime CreateTime { get; set; }

        [DisplayName("更新時間")]
        public DateTime UpdateTime { get; set; }
    }
}