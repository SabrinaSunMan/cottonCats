using PagedList;
using StoreDB.Enum;
using StoreDB.Model.Partials;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackMeow.Models.ViewModel
{
    /// <summary>
    /// 活動管理 包括 表頭以及 PageList
    /// </summary>
    public class MemberViewModel
    {
        public MemberListHeaderViewModel Header { get; set; }

        public IPagedList<MemberListContentViewModel> Content_List { get; set; }

        public PublicModel QueryBlock { get; set; }

        public int page { get; set; }
    }

    /// <summary>
    /// 搜尋 [Member] 條件式
    /// </summary>
    public class MemberListHeaderViewModel
    {
        /// <summary>
        /// 姓名.
        /// </summary>
        [DisplayName("姓名")]
        [StringLength(10)]
        public string Name { get; set; }

        /// <summary>
        /// 是否填寫合約書.
        /// </summary>
        [DisplayName("是否填寫合約書")]
        public string ContractCheck { get; set; }

        /// <summary>
        /// 手機號碼.
        /// </summary>
        [DisplayName("手機號碼")]
        public int PhoneNumber { get; set; }

        /// <summary>
        /// 縣市.
        /// </summary>
        [DisplayName("縣市")]
        public string CityDDL { get; set; }

        /// <summary>
        /// 鄉鎮市區.
        /// </summary>
        [DisplayName("鄉鎮區")]
        public string CountyDDL { get; set; }

        ///// <summary>
        ///// 建立日期.
        ///// </summary>
        //[DisplayName("建立日期")]
        //public string CreateTime { get; set; }

        ///// <summary>
        ///// 性別.
        ///// </summary>
        //[DisplayName("性別")]
        //public bool Sex { get; set; }
    }

    /// <summary>
    /// 呈現 [Member] 搜尋結果
    /// </summary>
    public class MemberListContentViewModel
    {
        /// <summary>
        /// 使用者序號.
        /// </summary>
        [DisplayName("使用者序號")]
        public Guid MemberID { get; set; }

        /// <summary>
        /// 使用者名稱.
        /// </summary>
        [DisplayName("使用者名稱")]
        [StringLength(10)]
        public string Name { get; set; }

        //private bool _sex = true;

        ///// <summary>
        ///// 性別.
        ///// </summary>
        //[DisplayName("性別")]
        //public string Sex
        //{
        //    get { return _sex == true ? "男" : "女"; }
        //    set { _sex = Convert.ToBoolean(value); }
        //}

        /// <summary>
        /// 手機號碼.
        /// </summary>
        [DisplayName("手機號碼")]
        public string PhoneNumber { get; set; }

        //[StringLength(30)]
        //public string Email { get; set; }

        [DisplayName("地區")]
        public string District { get; set; }

        ///// <summary>
        ///// 城市名稱. From ZipCode.
        ///// </summary>
        //[DisplayName("城市")]
        //public string City { get; set; }

        ///// <summary>
        ///// 鄉鎮區域. From ZipCode.
        ///// </summary>
        //[DisplayName("鄉鎮區域")]
        //public string County { get; set; }

        /// <summary>
        /// 建立日期.
        /// </summary>
        private DateTime _createTime;

        public string CreateTime
        {
            get
            {
                string tmpCreate = _createTime.ToString("yyyy/MM/dd");
                return tmpCreate;
            }
            set { DateTime.TryParse(value, out _createTime); }
        }
    }

    /// <summary>
    /// 呈現 [Member] 檢視頁面 時
    /// </summary>
    public class MemberDetailViewModel : Member
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相符。")]
        public string ConfirmPassword { get; set; }

        public string ChooseCity { get; set; }

        public string ChoosePostalCode { get; set; }

        public DataAction ActionType { get; set; }
    }
}