using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// 靜態網頁管理 
    /// </summary>
    public partial class StaticHtml : BasePartials
    {
        /// <summary>
        /// ID.
        /// </summary> 
        [Key]
        [DisplayName("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StaticID { get; set; }

        /// <summary>
        /// 靜態網頁類別ID. FK From HtmlSubject
        /// </summary> 
        [DisplayName("靜態網頁類別ID")]
        public string SubjectID { get; set; }

        /// <summary>
        /// 圖片資訊ID. FK From PictureInfo 
        /// </summary> 
        [DisplayName("圖片資訊ID")]
        public Guid PicID { get; set; }

        /// <summary>
        /// 網頁內容文字.
        /// </summary> 
        [DisplayName("內容文字")]
        public string HtmlContext { get; set; }
         
    }
}
