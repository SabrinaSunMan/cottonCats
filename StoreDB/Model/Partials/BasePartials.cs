using System;
using System.Collections.Generic;
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
        public DateTime CreateTime
        {
            get { return _createtime; }
            set { value = DateTime.Now; }
        }

        /// <summary>
        /// 更新日期.
        /// </summary>
        private DateTime _updatetime;
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { value = DateTime.Now; }
        }

        /// <summary>
        /// 建立者.
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 更新者.
        /// </summary>
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
