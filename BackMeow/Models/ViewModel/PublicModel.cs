using StoreDB.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackMeow.Models
{
    public class PublicModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PublicModel"/> is result.
        /// </summary>
        /// <value>
        ///   <c>true</c> if result; otherwise, <c>false</c>.
        /// </value>
        public bool Result { get; set; }

        /// <summary>
        /// Gets or sets the result code.
        /// </summary>
        /// <value>
        /// The result code.
        /// </value>
        public string ResultCode { get; set; }

        /// <summary>
        /// Gets or sets the result message.
        /// </summary>
        /// <value>
        /// The result message.
        /// </value>
        public string ResultMessage { get; set; }
    }

    //public class ReturnMsg
    //{
    //    /// <summary>
    //    /// Enum代表
    //    /// </summary>
    //    /// <value>
    //    /// The enum MSG.
    //    /// </value>
    //    public BackReturnMsg enumMsg { get; set; }
    //    /// <summary>
    //    /// 僅字串
    //    /// </summary>
    //    /// <value>
    //    /// The MSG.
    //    /// </value>
    //    public string StrMsg { get; set; }
    //}
}