using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    ///// <summary>
    ///// 後台_錯誤訊息 判斷式回應訊息
    ///// </summary>
    //public enum BackReturnMsg
    //{
    //    /// <summary>
    //    /// 此帳號或是Email已有人申請，請重新輸入
    //    /// </summary> 
        
    //    Repeat = 0,

    //    /// <summary>
    //    /// 成功
    //    /// </summary> 
    //    Scuess = 1,

    //    /// <summary>
    //    /// 未知的錯誤
    //    /// </summary>
    //    Error = 2
    //}
    
    public enum DataAction
    {
        /// <summary>
        /// 新增
        /// </summary>
        Create = 0,

        /// <summary>
        /// 建立成功
        /// </summary>
        [Description("建立成功")]
        CreateScuess = 1,

        /// <summary>
        /// 建立失敗
        /// </summary>
        [Description("建立失敗")]
        CreateFail = 2,

        /// <summary>
        /// 建立失敗
        /// </summary>
        [Description("已有重複資料，請重新輸入")]
        CreateFailReapet = 3,

        /// <summary>
        /// 更新
        /// </summary>
        Update = 4,

        /// <summary>
        /// 更新成功
        /// </summary>
        [Description("更新成功")]
        UpdateScuess = 5,

        /// <summary>
        /// 更新失敗
        /// </summary>
        [Description("更新失敗")]
        UpdateFail = 6,

        Read = 7
    }
}
