namespace StoreDB.Model.Partials
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 管理者資料
    /// </summary>
    public partial class AspNetUsers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetRoles = new HashSet<AspNetRoles>();
        }
        /// <summary>
        /// 管理者 使用者ID.
        /// </summary> 
        [DisplayName("使用者ID")]
        public string Id { get; set; }
          
        [StringLength(256)]
        [EmailAddress(ErrorMessage ="非E-Mail格式")]
        [DisplayName("帳號")]
        public string Email { get; set; }


        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 密碼雜湊.
        /// </summary> 
        [DisplayName("密碼雜湊")]
        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        /// <summary>
        /// 電話號碼.
        /// </summary> 
        [DisplayName("電話號碼")]
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// 鎖定日期時間內.
        /// </summary> 
        [DisplayName("鎖定日期時間內")]
        public DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// 是否要驗證錯誤次數.
        /// </summary> 
        [DisplayName("是否要驗證錯誤次數")]
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// 輸入錯誤次數.
        /// </summary> 
        [DisplayName("輸入錯誤次數")]
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 建立時間.
        /// </summary> 
        [DisplayName("建立時間")] //Add
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新時間.
        /// </summary> 
        [DisplayName("更新時間")] //Add
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 使用者名稱.
        /// </summary> 
        [Required]
        [StringLength(256)]
        [DisplayName("使用者名稱")]
        public string UserName { get; set; }

        public virtual ICollection<Group> GroupID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
    }
}
