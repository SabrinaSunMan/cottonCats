using System.ComponentModel;

namespace StoreDB.Model.ViewModel
{
    /// <summary>
    /// 顯示所有功能列表(含根目錄)
    /// </summary>
    public class MenuSideViewModel
    {
        /// <summary>
        /// 根目錄 ID.
        /// </summary>
        [DisplayName("根目錄 ID")]
        public string TRootName { get; set; }

        /// <summary>
        /// 子功能名稱.
        /// </summary>
        [DisplayName("子功能名稱")]
        public string MenuName { get; set; }

         
        public string ActionName { get; set; }

         
        public string CrollerName { get; set; }

        /// <summary>
        /// 子功能排序.
        /// </summary>
        [DisplayName("子功能排序")]
        public int MenuOrder { get; set; }

        /// <summary>
        /// 根目錄排序.
        /// </summary>
        [DisplayName("根目錄排序")]
        public int TRootOrder { get; set; }

    }
}