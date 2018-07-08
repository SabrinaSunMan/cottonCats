using StoreDB.Model.Partials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreDB.Model.ViewModel.BackcottonCats
{
    /// <summary>
    /// 統一藉由此ViewModel溝通將 三張Table串接
    /// </summary>
    /// <seealso cref="StoreDB.Model.Partials.BasePartials" />
    public class StaticHtmlDBViewModel : BasePartials
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
        public string HtmlContext { get; set; }

        /// <summary>
        /// 靜態網頁類別ID. FK From HtmlSubject
        /// </summary>
        [DisplayName("靜態網頁類別ID")]
        public string SubjectID { get; set; }

        /// <summary>
        /// 類別名稱.
        [DisplayName("類別名稱")]
        [StringLength(20, ErrorMessage = "長度不得超過{0}")]
        public string SubjectName { get; set; }

        /// <summary>
        /// 圖片資訊ID. FK From StaticHtml.StaticID
        /// </summary>
        [DisplayName("靜態圖片Group_ID")]
        public string PicGroupID { get; set; }

        public IEnumerable<PictureInfo> picInfo { get; set; }

        ///// <summary>
        ///// 圖片資訊ID. FK From PictureInfo
        ///// </summary>
        //[DisplayName("圖片資訊ID")]
        //public string PicID { get; set; }

        ///// <summary>
        ///// 圖片名稱.
        ///// </summary>
        //[StringLength(20, ErrorMessage = "長度不得超過{0}")]
        //public string PictureName { get; set; }

        ///// <summary>
        ///// 圖片網址.
        ///// </summary>
        //[StringLength(100, ErrorMessage = "長度不得超過{0}")]
        //public string PictureUrl { get; set; }

        ///// <summary>
        ///// 圖片副檔名.
        ///// </summary>
        //public string FileExtension { get; set; }
    }
}