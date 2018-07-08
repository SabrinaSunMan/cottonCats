using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using PagedList;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using StoreDB.Model.Partials;

namespace BackMeow.Models.ViewModel
{
    /// <summary>
    /// 活動管理 包括 表頭以及 PageList
    /// </summary>
    public class ActitiesViewModel
    {
        public ActitiesListHeaderViewModel Header { get; set; }

        public IPagedList<ActitiesListContentViewModel> Content_List { get; set; }

        public int page { get; set; }
    }

    /// <summary>
    /// 搜尋 [Actities] 條件式
    /// </summary>
    public class ActitiesListHeaderViewModel
    {
        /// <summary>
        /// 建立日期.
        /// </summary>
        [DisplayName("建立日期")]
        public string CreateTime { get; set; }

        /// <summary>
        /// 標題名稱.
        /// </summary>
        [DisplayName("標題名稱")]
        public string TitleName { get; set; }

        /// <summary>
        /// 內容文字.
        /// </summary>
        [DisplayName("內容文字")]
        [MinLength(3, ErrorMessage = "最少不得輸入少於 {1}")]
        public string HtmlContext { get; set; }

        /// <summary>
        /// 上架日期.
        /// </summary>
        [DisplayName("上架日期")]
        public string StartDate { get; set; }

        /// <summary>
        /// 下架日期.
        /// </summary>
        [DisplayName("下架日期")]
        public string EndDate { get; set; }
    }

    /// <summary>
    /// 呈現 [Actities] 搜尋結果
    /// </summary>
    public class ActitiesListContentViewModel
    {
        /// <summary>
        /// ID.
        /// </summary>
        [DisplayName("ID")]
        public string ActitiesID { get; set; }

        [DisplayName("標題")]
        public string TitleName { get; set; }

        /// <summary>
        /// 網頁內容文字.
        /// </summary>
        [DisplayName("內容文字")]
        public string HtmlContext { get; set; }

        /// <summary>
        /// 狀態. False = 刪除,True = 啟用中
        /// </summary>
        public string _HtmlStatus;

        [DisplayName("上架狀態")]
        public string Status //{ get; set; }
        {
            get { return Convert.ToBoolean(_HtmlStatus) == true ? "上架中" : "下架"; }
            set { _HtmlStatus = value; }
        }

        /// <summary>
        /// 上架日期.
        /// </summary>
        private DateTime _sTime;

        public string STime
        {
            get { return _sTime.ToString("yyyy/MM/dd"); }
            set { DateTime.TryParse(value, out _sTime); }
        }

        /// <summary>
        /// 下架日期.
        /// </summary>
        private DateTime _eTime;

        public string ETime
        {
            get { return _eTime.ToString("yyyy/MM/dd"); }
            set { DateTime.TryParse(value, out _eTime); }
        }

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
    /// 呈現 [Actity] 檢視頁面 時
    /// </summary>
    public class ActitiesDetailViewModel
    {
        /// <summary>
        /// ID.
        /// </summary>
        [DisplayName("ID")]
        public string ActivityID { get; set; }

        /// <summary>
        /// 網頁內容文字.
        /// </summary>
        [DisplayName("內容文字")]
        //[StringLength(256, ErrorMessage = (" 不得超過長度 {1}"))]
        [DataType(DataType.MultilineText)]
        public string HtmlContext { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        [DisplayName("標題")]
        public string TitleName { get; set; }

        private DateTime _Sdate;

        /// <summary>
        /// 上架日期.
        /// </summary>
        [DisplayName("上架日期")]
        public string Sdate
        {
            get
            {
                string SdateResult = "";
                if (_Sdate == DateTime.MinValue)
                {
                    SdateResult = DateTime.Now.ToString("yyyy/MM/dd");
                }
                else SdateResult = _Sdate.ToString("yyyy/MM/dd");
                return SdateResult;
            }
            set { DateTime.TryParse(value, out _Sdate); }
        }

        private DateTime _Edate;

        /// <summary>
        /// 上架日期.
        /// </summary>
        [DisplayName("上架日期")]
        public string Edate
        {
            get
            {
                string EdateResult = "";
                if (_Edate == DateTime.MinValue)
                {
                    EdateResult = DateTime.Now.ToString("yyyy/MM/dd");
                }
                else EdateResult = _Edate.ToString("yyyy/MM/dd");
                return EdateResult;
            }
            set { DateTime.TryParse(value, out _Edate); }
        }

        /// <summary>
        /// 狀態. False = 刪除,True = 啟用中
        /// </summary>
        [DisplayName("上架狀態")]
        public bool Status { get; set; }

        public IEnumerable<PictureInfo> picInfo { get; set; }

        private DateTime _CreateTime;

        [DisplayName("使用者建立時間")]
        public DateTime CreateTime
        {
            get
            {
                if (_CreateTime == DateTime.MinValue)
                {
                    return DateTime.Now;
                }
                else return _CreateTime;
            }
            set { _CreateTime = value; }
        }

        [DisplayName("建立者")]
        public string CreateUser { get; set; }

        private DateTime _UpdateTime;

        /// <summary>
        /// 更新時間.
        /// </summary>
        [DisplayName("更新時間")]
        public DateTime UpdateTime
        {
            get
            {
                if (_UpdateTime == DateTime.MinValue)
                {
                    return DateTime.Now;
                }
                else return _UpdateTime;
            }
            set { _UpdateTime = value; }
        }

        [DisplayName("更新者")]
        public string UpdateUser { get; set; }

        /// <summary>
        /// 排序.
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 圖片資訊ID. FK From StaticHtml.StaticID
        /// </summary>
        [DisplayName("靜態圖片Group_ID")]
        public string PicGroupID { get; set; }
    }
}