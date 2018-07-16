using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackMeow.Models.ViewModel
{
    public class MemberViewModel
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

        /// <summary>
        /// 性別.
        /// </summary>
        [DisplayName("性別")]
        public bool Sex { get; set; }

        /// <summary>
        /// 手機號碼.
        /// </summary>
        [DisplayName("手機號碼")]
        public int PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "非E-Mail格式")]
        [StringLength(30)]
        public string Email { get; set; }

        /// <summary>
        /// 地址.
        /// </summary>
        [DisplayName("地址")]
        public string Address { get; set; }

        /// <summary>
        /// 郵遞區號.
        /// </summary>
        [DisplayName("郵遞區號")]
        public string PostalCode { get; set; }

        /// <summary>
        /// 生日.
        /// </summary>
        [DisplayName("郵遞區號")]
        public DateTime Birthday { get; set; }
    }
}