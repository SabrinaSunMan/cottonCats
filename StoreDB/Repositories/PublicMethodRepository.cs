using StoreDB.Interface;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace StoreDB.Repositories
{
    public class PublicMethodRepository : IPublicMethod
    {
        /// <summary>
        /// SHA256 加密作業.
        /// </summary>
        /// <returns></returns>
        public string SHA256Pwd(string OriginalStr)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(OriginalStr);
            byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 取得 合約是否填寫 DropDownlist.
        /// </summary>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public List<SelectListItem> ContractCheckList(string DefaultValue)
        {
            List<SelectListItem> ContractCheck = new List<SelectListItem>();
            ContractCheck.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = ""
            });
            ContractCheck.Add(new SelectListItem()
            {
                Text = "是",
                Value = "1"
            });
            ContractCheck.Add(new SelectListItem()
            {
                Text = "否",
                Value = "0"
            });
            foreach (var item in ContractCheck)
            {
                if (item.Value == DefaultValue)
                {
                    item.Selected = true;
                    break;
                }
            }
            return ContractCheck;
        }
    }
}