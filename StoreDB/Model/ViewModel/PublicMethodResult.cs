using StoreDB.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Model.ViewModel
{
    /// <summary>
    /// 統一回傳 共用 的容器
    /// </summary>
    public class PublicMethodResult
    {
        public bool ResultBool { get; set; }

        public object Result { get; set; }

        public DataAction ActionResult { get; set; }
    }
}