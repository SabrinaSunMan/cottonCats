using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Enum
{
    /// <summary>
    /// 後台_普遍性使用頁數
    /// </summary>
    /// 
    public enum BackPageListSize 
    {
        /// <summary>
        /// The common size
        /// </summary>
        commonSize = 5
    }

    /// <summary>
    /// 後台_錯誤訊息 判斷式回應訊息
    /// </summary>
    public enum BackReturnMsg
    {
        /// <summary>
        /// 此帳號或是Email已有人申請，請重新輸入
        /// </summary> 
        Repeat = 0,

        /// <summary>
        /// 成功
        /// </summary> 
        Scuess = 1,

        /// <summary>
        /// 未知的錯誤
        /// </summary>
        Error = 2
    }
    
    public enum DataAction
    {
        /// <summary>
        /// 新增
        /// </summary>
        Create = 0,

        /// <summary>
        /// 更新
        /// </summary>
        Update = 1,
        
        Read = 2
    }
}
