using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// Menu 父目錄 
    /// </summary>
    public class MenuTreeRoot
    { 
        /// <summary>
        /// 父目錄 ID.
        /// </summary> 
        [Key]
        [DisplayName("父目錄 ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TRootID { get; set; }

        /// <summary>
        /// 父目錄功能名稱.
        /// </summary> 
        [DisplayName("父目錄功能名稱")]
        [StringLength(10)]
        public string TRootName { get; set; }

        /// <summary>
        /// 父目錄排序.
        /// </summary> 
        [DisplayName("父目錄排序")]
        public int TRootOrder { get; set; }
         
        public string UrlIcon { get; set; }
          
    }
}
