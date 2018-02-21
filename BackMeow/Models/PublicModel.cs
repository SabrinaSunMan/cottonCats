using StoreDB.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackMeow.Models
{
    public class PublicModel
    {

    }

    public class ReturnMsg
    {
        /// <summary>
        /// Enum代表
        /// </summary>
        /// <value>
        /// The enum MSG.
        /// </value>
        public BackReturnMsg enumMsg { get; set; }
        /// <summary>
        /// 僅字串
        /// </summary>
        /// <value>
        /// The MSG.
        /// </value>
        public string StrMsg { get; set; }
    }
}