using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// 後台_測邊功能列
    /// </summary>
    public partial class MenuSideList
    {
        /// <summary>
        /// MenuSideList ID.
        /// </summary> 
        [Key]
        [DisplayName("使用者功能列ID")]
        public string MenuSideListID { get; set; }

        /// <summary>
        /// 子目錄ID.
        /// </summary> 
        //public virtual ICollection<MenuTree> MenuID { get; set; }
        public Guid MenuID { get; set; }

        /// <summary>
        /// 使用者ID.
        /// </summary> 
        //public virtual ICollection<AspNetUsers> Id { get; set; } 
        public Guid Id { get; set; }
    }
}
