﻿using System;
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
        /// 圖片資訊ID. FK From PictureInfo 
        /// </summary> 
        [Key]
        [DisplayName("靜圖片資訊ID")]
        public Guid PicID { get; set; }
         
        /// <summary>
        /// 圖片名稱.
        /// </summary> 
        [StringLength(20, ErrorMessage = "長度不得超過{0}")]
        public string PictureName { get; set; }

        /// <summary>
        /// 圖片網址.
        /// </summary> 
        [StringLength(100, ErrorMessage = "長度不得超過{0}")]
        public string PictureUrl { get; set; }
    }
}