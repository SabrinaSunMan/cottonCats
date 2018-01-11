using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// Menu 子目錄 
    /// </summary>
    public class MenuTree
    {
        [Key]
        [DisplayName("ID")]
        public string MenuID { get; set; }

        [DisplayName("子功能名稱")]
        public string MenuName { get; set; }

        [DisplayName("子功能排序")]
        public int MenuOrder { get; set; }
         
        public string ControllerName { get; set; }
         
        public string ActionName { get; set; }

        //[ForeignKey("MenuTreeRoot")]
        //[DisplayName("MenuTreeRootID")]
        public string TRootID { get; set; }
    }
}
