using StoreDB.Model.Partials;
using System.Collections.Generic;
using System.ComponentModel;

namespace StoreDB.Model.ViewModel
{
    /// <summary>
    /// 顯示所有功能列表(含根目錄,階層式)
    /// </summary>
    public class MenuTreeRootStratumViewModel
    { 
        #region 第一層
        /// <summary>
        /// 父目錄 ID.
        /// </summary>  
        [DisplayName("父目錄 ID")]
        public string TRootID { get; set; }

        /// <summary>
        /// 父目錄功能名稱.
        /// </summary> 
        [DisplayName("父目錄功能名稱")]
        public string TRootName { get; set; }

        /// <summary>
        /// 父目錄排序.
        /// </summary> 
        [DisplayName("父目錄排序")]
        public int TRootOrder { get; set; }

        /// <summary>
        /// Gets or sets the URL icon.
        /// </summary> 
        public string UrlIcon { get; set; }

        /// <summary>
        /// 現在選擇
        /// </summary> 
        public string nowPicker { get; set; }

        #region 當父目錄沒有子節點的時候,就使用它自己的Controller和ActionName
        public string ControllerName { get; set; }

        public string ActionName { get; set; }
        #endregion

        #endregion

        #region 第二層 - 藉由第一層決定第二層要長成怎樣

        public List<MenuTree> tree { get; set; }

        #endregion
    }

    /// <summary>
    /// 顯示所有功能列表(含根目錄)_單純用來判斷是否可訪問用
    /// </summary>
    public class MenuSideContentViewModel
    {
        /// <summary>
        /// 父目錄 ID.
        /// </summary>  
        [DisplayName("父目錄 ID")]
        public string TRootID { get; set; }

        /// <summary>
        /// 父目錄功能名稱.
        /// </summary> 
        [DisplayName("父目錄功能名稱")]
        public string TRootName { get; set; }

        /// <summary>
        /// 父目錄排序.
        /// </summary> 
        [DisplayName("父目錄排序")]
        public int TRootOrder { get; set; }

        /// <summary>
        /// 子功能名稱.
        /// </summary>
        [DisplayName("子功能名稱")]
        public string MenuName { get; set; }

         
        public string ActionName { get; set; }

         
        public string ControllerName { get; set; }

        /// <summary>
        /// 子功能排序.
        /// </summary>
        [DisplayName("子功能排序")]
        public int MenuOrder { get; set; }
          
        /// <summary>
        /// Gets or sets the URL icon.
        /// </summary> 
        public string UrlIcon { get; set; }
    }
}