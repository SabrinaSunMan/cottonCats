using StoreDB.Model.Partials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreDB.Model.ViewModel.BackcottonCats
{
    /// <summary>
    /// 統一藉由此ViewModel溝通將 兩張Table串接
    /// </summary>
    /// <seealso cref="StoreDB.Model.Partials.BasePartials" />
    public class ActitiesDBViewModel : BasePartials
    {
        /// <summary>
        /// 活動紀錄管理ID.
        /// </summary>
        [DisplayName("活動紀錄管理ID")]
        public string ActivityID { get; set; }

        /// <summary>
        /// 開始日期.
        /// </summary>
        [DisplayName("開始日期")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 結束日期.
        /// </summary>
        [DisplayName("結束日期")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 活動標題.
        /// </summary>
        [DisplayName("活動標題")]
        [StringLength(20, ErrorMessage = "長度不得超過{0}")]
        public string TitleName { get; set; }

        /// <summary>
        /// 活動內容.
        /// </summary>
        [DisplayName("活動內容")]
        public string HtmlContext { get; set; }

        /// <summary>
        /// 圖片資訊ID. FK From PictureInfo
        /// </summary>
        [DisplayName("靜態圖片Group_ID")]
        public string PicGroupID { get; set; }

        public IEnumerable<PictureInfo> picInfo { get; set; }
    }
}