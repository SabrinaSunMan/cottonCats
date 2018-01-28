using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// Menu 根目錄 
    /// </summary>
    public class MenuTreeRoot
    {
        /// <summary>
        /// 根目錄 ID.
        /// </summary> 
        [Key]
        [DisplayName("MenuTreeRootID")]
        public string TRootID { get; set; }

        /// <summary>
        /// 根目錄功能名稱.
        /// </summary> 
        [DisplayName("根目錄功能名稱")]
        [StringLength(10)]
        public string TRootName { get; set; }

        /// <summary>
        /// 根目錄排序.
        /// </summary> 
        [DisplayName("根目錄排序")]
        public int TRootOrder { get; set; }
          
    }
}
