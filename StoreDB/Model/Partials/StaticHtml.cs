using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// 靜態網頁管理 主檔
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
        //[ForeignKey("SubjectID")]
        //public ICollection<StaticHtml_Picture_Subject> SubjectInfo { get; set; }
        public string SubjectID { get; set; }

        /// <summary>
        /// 圖片資訊ID. FK From PictureInfo 
        /// </summary> 
        [DisplayName("靜圖片Group_ID")]
        //[ForeignKey("PicID")]
        //public ICollection<StaticHtml_Picture_Subject> PicInfo { get; set; }
        public Guid PicGroupID { get; set; }

        /// <summary>
        /// 網頁內容文字.
        /// </summary> 
        [DisplayName("內容文字")]
        public string HtmlContext { get; set; }
         
    }

    public class StaticHtml_Picture_Subject
    {
        /// <summary>
        /// 靜態網頁類別ID. FK From HtmlSubject
        /// </summary> 
        public string SubjectID { get; set; }

        /// <summary>
        /// 圖片資訊ID. FK From PictureInfo 
        /// </summary> 
        public Guid PicID { get; set; }
    }
}
