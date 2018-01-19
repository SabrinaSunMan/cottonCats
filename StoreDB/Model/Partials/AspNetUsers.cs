namespace StoreDB.Model.Partials
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
     
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

        [DisplayName("使用者ID")]
        public string Id { get; set; }

        [Required(ErrorMessage = "帳號為為必輸入欄位")]
        [DisplayName("帳號")] //Add
        public string Account { get; set; }

        [StringLength(256)]
        [EmailAddress(ErrorMessage ="非E-Mail格式")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [DisplayName("密碼雜湊")]
        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        [DisplayName("電話號碼")]
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        [DisplayName("鎖定日期時間內")]
        public DateTime? LockoutEndDateUtc { get; set; }

        [DisplayName("是否要驗證錯誤次數")]
        public bool LockoutEnabled { get; set; }

        [DisplayName("輸入錯誤次數")]
        public int AccessFailedCount { get; set; }

        [DisplayName("建立時間")] //Add
        public DateTime CreateTime { get; set; }

        [DisplayName("更新時間")] //Add
        public DateTime UpdateTime { get; set; }

        [Required]
        [StringLength(256)]
        [DisplayName("使用者名稱")]
        public string UserName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
    }
}
