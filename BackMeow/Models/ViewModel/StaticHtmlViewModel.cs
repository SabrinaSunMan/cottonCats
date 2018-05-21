using PagedList;
using StoreDB.Enum;
using StoreDB.Model.Partials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMeow.Models.ViewModel
{
    /// <summary>
    /// 靜態網頁 包括 表頭以及 PageList
    /// </summary>
    public class StaticHtmlViewModel
    {
        public StaticHtmlAction StaticHtmlActionType { get; set; }

        public StaticHtmlListHeaderViewModel Header { get; set; }

        public IPagedList<StaticHtmlListContentViewModel> Content_List { get; set; }

        public int page { get; set; }
    }

    /// <summary>
    /// 搜尋 [StaticHtml] 條件式
    /// </summary>
    public class StaticHtmlListHeaderViewModel
    {

        /// <summary>
        /// 建立日期.
        /// </summary>
        [DisplayName("建立日期")]
        public string CreateTime { get; set; }

        [DisplayName("排序")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "請輸入有效數字，一定是正整數")]
        public string sort { get; set; }

        /// <summary>
        /// 網頁內容文字.
        /// </summary> 
        [DisplayName("內容文字")]
        [MinLength(3, ErrorMessage = "最少不得輸入少於 {1}")]
        public string HtmlContext { get; set; }
    }

    /// <summary>
    /// 呈現 [StaticHtml] 搜尋結果
    /// </summary>
    public class StaticHtmlListContentViewModel
    {
        /// <summary>
        /// ID.
        /// </summary> 
        [DisplayName("ID")]
        public string StaticID { get; set; }
       
        /// <summary>
        /// 網頁內容文字.
        /// </summary> 
        [DisplayName("內容文字")]
        public string HtmlContext { get; set; }

        /// <summary>
        /// 狀態. False = 刪除,True = 啟用中
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 建立日期.
        /// </summary>
        private DateTime _createTime;
        public string CreateTime
    {
            get { return _createTime.ToString("yyyy/MM/dd"); }
            set { DateTime.TryParse(value, out _createTime); }
        }

    }

    /// <summary>
    /// 呈現 [StaticHtml] 檢視頁面 時
    /// </summary>
    public class StaticHtmlDetailViewModel
    {
        /// <summary>
        /// ID.
        /// </summary> 
        [DisplayName("StaticHtmlID")]
        public string StaticID { get; set; }

        /// <summary>
        /// 網頁內容文字.
        /// </summary> 
        [DisplayName("內容文字")]
        [StringLength(256,ErrorMessage =(" 不得超過長度 {1}"))]
        public string HtmlContext { get; set; }

        /// <summary>
        /// 靜態網頁類別ID. FK From HtmlSubject
        /// </summary> 
        [DisplayName("靜態網頁類別ID")]
        public string SubjectID { get; set; }

        /// <summary>
        /// 類別名稱.
        ///
        [DisplayName("類別名稱")]
        public string SubjectName { get; set; }

        public List<PictureInfo> picInfo { get; set; }
        
        [DisplayName("使用者建立時間")]
        public DateTime CreateTime { get; set; }

        [DisplayName("更新時間")]
        public DateTime UpdateTime { get; set; }

    }
    
}