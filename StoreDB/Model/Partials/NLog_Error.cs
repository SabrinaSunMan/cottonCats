using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Model.Partials
{ 
    /// <summary>
    /// Log資訊
    /// </summary>
    public partial class NLog_Error
    {
        [Key] 
        public string LogId { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string Host { get; set; }

        public string Result { get; set; }

        public string SaveData { get; set; }

        public string LogLevel { get; set; }

        public string Data_Action { get; set; }

        public string Orignal_Page { get; set; }

        public string Statement { get; set; }

        public string ControllersName { get; set; }

        public string ActionName { get; set; }
    }
}
