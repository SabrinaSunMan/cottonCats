using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDB.Model.Partials
{

    /// <summary>
    /// 靜態網頁管理_類別
    /// </summary>
    public partial class HtmlSubject : BasePartials
    {
        /// <summary>
        /// 靜態網頁類別ID. 
        /// </summary> 
        [DisplayName("靜態網頁類別ID")]
        [Key]
        public Guid SubjectID { get; set; }

        /// <summary>
        /// 類別名稱.
        /// </summary> 
        [StringLength(20, ErrorMessage = "長度不得超過{0}")]
        public string SubjectName { get; set; }
    }
}
