using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// 地址區域
    /// </summary>
    public partial class ZipCode
    {
        /// <summary>
        /// ID.
        /// </summary>
        [DisplayName("ID")]
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 城市名稱.
        /// </summary>
        [DisplayName("城市")]
        [StringLength(4)]
        public string City { get; set; }

        /// <summary>
        /// 鄉鎮區域.
        /// </summary>
        [DisplayName("鄉鎮區域")]
        [StringLength(4)]
        public string County { get; set; }

        /// <summary>
        /// 郵遞區號.
        /// </summary>
        [DisplayName("郵遞區號")]
        public int PostalCode { get; set; }

        /// <summary>
        /// 排序.
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 城市排序.
        /// </summary>
        [DisplayName("城市排序")]
        public int CitySort { get; set; }

        /// <summary>
        /// 狀態.
        /// </summary>
        [DisplayName("狀態")]
        public bool IsEnabled { get; set; }
    }
}