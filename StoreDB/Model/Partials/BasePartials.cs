using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Model.Partials
{
    public class BasePartials
    {
        /// <summary>
        /// 建立日期.
        /// </summary>
        private DateTime _createtime;

        [DisplayName("使用者建立時間")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime
        {
            get { return _createtime.Year == 1 ? DateTime.Now : _createtime; }
            set { _createtime = value; }
        }

        /// <summary>
        /// 更新日期.
        /// </summary>
        private DateTime _updatetime;

        [DisplayName("更新時間")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdateTime
        {
            get { return _updatetime.Year == 1 ? DateTime.Now : _updatetime; }
            set { _updatetime = value; }
        }

        /// <summary>
        /// 建立者.
        /// </summary>
        // 因為AspNetUsers預設與 AspNetUserClaims、AspNetUserLogins相關, 維持string不予更動該欄位型態
        public string CreateUser { get; set; }

        /// <summary>
        /// 更新者.
        /// </summary>
        // 因為AspNetUsers預設與 AspNetUserClaims、AspNetUserLogins相關, 維持string不予更動該欄位型態
        public string UpdateUser { get; set; }

        /// <summary>
        /// 狀態. False = 刪除,True = 啟用中
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 排序.
        /// </summary>
        public int sort { get; set; }
    }
}