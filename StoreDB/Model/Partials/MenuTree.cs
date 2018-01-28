using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public string MenuID { get; set; }

        /// <summary>
        /// 子功能名稱.
        /// </summary> 
        [DisplayName("子功能名稱")]
        [StringLength(10)]
        public string MenuName { get; set; }

        /// <summary>
        /// 子功能排序.
        /// </summary> 
        [DisplayName("子功能排序")]
        public int MenuOrder { get; set; }
         

        [StringLength(12)]
        public string ControllerName { get; set; }
         
        [StringLength(12)]
        public string ActionName { get; set; }

        //[ForeignKey("MenuTreeRoot")] 
        /// <summary>
        /// 根目錄 ID.
        /// </summary> 
        [DisplayName("根目錄 ID")]
        public string TRootID { get; set; }
    }
}
