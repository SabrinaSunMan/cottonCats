using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// Menu 子目錄 
    /// </summary>
    public partial class MenuTree
    {

        /// <summary>
        /// 子目錄ID.
        /// </summary> 
        [Key]
        [DisplayName("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MenuID { get; set; }

        /// <summary>
        /// 子功能名稱.
        /// </summary> 
        [DisplayName("子功能名稱")]
        [StringLength(10, ErrorMessage = "長度不得超過{0}")]
        public string MenuName { get; set; }

        /// <summary>
        /// 子功能排序.
        /// </summary> 
        [DisplayName("子功能排序")]
        public int MenuOrder { get; set; }
          
        [StringLength(20, ErrorMessage = "長度不得超過{0}")]
        public string ControllerName { get; set; }
         
        [StringLength(20,ErrorMessage = "長度不得超過{0}")]
        public string ActionName { get; set; }

        //[ForeignKey("MenuTreeRoot")] 
        /// <summary>
        /// 父目錄 ID.
        /// </summary> 
        [DisplayName("父目錄 ID")]
        public Guid TRootID { get; set; }
    }
}
