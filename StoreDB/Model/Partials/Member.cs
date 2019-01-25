using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// 前台使用者資料
    /// </summary>
    public partial class Member : BasePartials
    {
        /// <summary>
        /// 使用者序號.
        /// </summary>
        [Key]
        [DisplayName("使用者序號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MemberID { get; set; }

        /// <summary>
        /// 使用者名稱.
        /// </summary>
        [DisplayName("使用者名稱")]
        [StringLength(10)]
        public string Name { get; set; }

        /// <summary>
        /// 密碼雜湊.
        /// </summary>
        //[Required]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        /// <summary>
        /// 性別.
        /// </summary>
        [DisplayName("性別")]
        public bool Sex { get; set; }

        /// <summary>
        /// 手機號碼.
        /// </summary>
        [DisplayName("手機號碼")]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "非E-Mail格式")]
        [StringLength(30)]
        public string Email { get; set; }

        /// <summary>
        /// 地址.
        /// </summary>
        [DisplayName("地址")]
        public string Address { get; set; }

        private DateTime _birthday;

        /// <summary>
        /// 生日.
        /// </summary>
        [DisplayName("生日")]
        [StringLength(10)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Birthday
        {
            get
            {
                return _birthday.Year == 1 ? DateTime.Now.AddYears(-18).ToString("yyyy/MM/dd") : _birthday.ToString("yyyy/MM/dd");
            }
            //return _birthday.ToString("yyyy/MM/dd"); }
            set { DateTime.TryParse(value, out _birthday); }
        }

        /// <summary>
        /// ZipCodeID. FK From ZipCode.ID
        /// </summary>
        [DisplayName("縣市區域")]
        public int ZipCodeID { get; set; }

        /// <summary>
        /// 是否填寫合約書.
        /// </summary>
        [DisplayName("是否填寫合約書")]
        public bool ContractCheck { get; set; }

        /// <summary>
        /// Email 驗證.
        /// </summary>
        [DisplayName("Email 驗證")]
        public bool EmailCheck { get; set; }
    }
}