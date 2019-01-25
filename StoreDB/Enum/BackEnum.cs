using System.ComponentModel;

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

    public enum StaticHtmlAction
    {
        /// <summary>
        /// 關於我們
        /// </summary>
        About = 1,

        /// <summary>
        /// 空間介紹
        /// </summary>
        Space = 2,

        /// <summary>
        /// 線上認養合約書
        /// </summary>
        Contract = 3,

        /// <summary>
        /// 義工招募
        /// </summary>
        Joinus = 4,

        /// <summary>
        /// 全部
        /// </summary>
        All = 99
    }

    public enum TableName
    {
        /// <summary>
        /// 後台管理者
        /// </summary>
        [Description("後台管理者")]
        AspNetUsers = 2,

        /// <summary>
        /// 會員管理
        /// </summary>
        [Description("會員管理")]
        Memeber = 3,

        /// <summary>
        /// 靜態網頁主檔
        /// </summary>
        [Description("靜態網頁主檔")]
        StaticHtml = 0,

        /// <summary>
        /// 圖片資訊
        /// </summary>
        [Description("圖片資訊")]
        PictureInfo = 1
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

    //public enum DataAction
    //{
    //    /// <summary>
    //    /// 新增
    //    /// </summary>
    //    Create = 0,

    //    /// <summary>
    //    /// 建立成功
    //    /// </summary>
    //    [Description("建立成功")]
    //    CreateScuess = 1,

    //    /// <summary>
    //    /// 建立失敗
    //    /// </summary>
    //    [Description("建立失敗")]
    //    CreateFail = 2,

    //    /// <summary>
    //    /// 建立失敗
    //    /// </summary>
    //    [Description("已有重複資料，請重新輸入")]
    //    CreateFailReapet = 3,

    //    /// <summary>
    //    /// 更新
    //    /// </summary>
    //    Update = 4,

    //    /// <summary>
    //    /// 更新成功
    //    /// </summary>
    //    [Description("更新成功")]
    //    UpdateScuess = 5,

    //    /// <summary>
    //    /// 更新失敗
    //    /// </summary>
    //    [Description("更新失敗")]
    //    UpdateFail = 6,

    //    Read = 7,

    //    /// <summary>
    //    /// 刪除
    //    /// </summary>
    //    Delete = 8,

    //    /// <summary>
    //    /// 刪除成功
    //    /// </summary>
    //    [Description("刪除成功")]
    //    DeleteScuess = 9,

    //    /// <summary>
    //    /// 刪除失敗
    //    /// </summary>
    //    [Description("刪除失敗")]
    //    DeleteFail = 10,

    //    /// <summary>
    //    /// 查詢成功-尚無符合的資料
    //    /// </summary>
    //    [Description("查詢成功-尚無符合的資料")]
    //    QueryScuess = 11,

    //    /// <summary>
    //    /// 查詢失敗
    //    /// </summary>
    //    [Description("查詢失敗")]
    //    QueryFail = 12
    //}
}