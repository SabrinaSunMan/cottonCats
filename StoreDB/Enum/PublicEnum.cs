using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Enum
{
    /// <summary>
    /// 對於資料使用的動作
    /// </summary>
    public enum Actions
    {
        /// <summary>
        /// 對於資料使用的動作_新增
        /// </summary>
        Create = 0,

        /// <summary>
        /// 對於資料使用的動作_修改
        /// </summary>
        Update = 1,

        /// <summary>
        /// 對於資料使用的動作_讀取
        /// </summary>
        Read = 2,

        /// <summary>
        /// 對於資料使用的動作_刪除
        /// </summary>
        Delete = 3
    }
}
