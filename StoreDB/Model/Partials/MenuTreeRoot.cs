using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// Menu 根目錄 
    /// </summary>
    public class MenuTreeRoot
    {
        [Key]
        [DisplayName("MenuTreeRootID")]
        public string TRootID { get; set; }

        [DisplayName("功能名稱")]
        public string TRootName { get; set; }
          
        [DisplayName("功能排序")]
        public int TRootOrder { get; set; }
          
    }
}
