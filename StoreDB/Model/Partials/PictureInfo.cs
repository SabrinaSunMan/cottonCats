using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Model.Partials
{
    public partial class PictureInfo : BasePartials
    {
        /// <summary>
        /// 圖片資訊ID.
        /// </summary>
        [Key]
        [DisplayName("靜圖片資訊ID")]
        public Guid PicID { get; set; }

        /// <summary>
        /// 圖片名稱.
        /// </summary>
        [StringLength(20, ErrorMessage = "{0}長度不得超過{1}")]
        public string PictureName { get; set; }

        /// <summary>
        /// 圖片網址.
        /// </summary>
        [StringLength(100, ErrorMessage = "{0}長度不得超過{1}")]
        public string PictureUrl { get; set; }

        /// <summary>
        /// 圖片副檔名.
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 圖片資訊ID. FK From StaticHtml.StaticID
        /// </summary>
        [DisplayName("靜圖片Group_ID")]
        public Guid PicGroupID { get; set; }
    }
}