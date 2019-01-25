using System.Collections.Generic;
using System.Web.Mvc;

namespace StoreDB.Interface
{
    /// <summary>
    /// 共用 控制項
    /// </summary>
    public interface IPublicMethod
    {
        /// <summary>
        /// SHA256 加密作業.
        /// </summary>
        /// <returns></returns>
        string SHA256Pwd(string OriginalStr);

        /// <summary>
        /// 取得 合約是否填寫 DropDownlist.
        /// </summary>
        /// <param name="ChooseItem">The choose item.</param>
        /// <returns></returns>
        List<SelectListItem> ContractCheckList(string DefaultValue);

        /// <summary>
        /// 驗證 Email 是否已驗證.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        //bool CheckEmailCheck(string email);
    }
}