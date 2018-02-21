using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StoreDB.Model.Partials
{ 
    /// <summary>
    /// Log資訊
    /// </summary>
    public partial class NLog_Error
    { 
        [Key] 
        public string LogId { get; set; } = Guid.NewGuid().ToString().ToUpper();

        //private string _LogId;
        //public string LogId
        //{ 
        //    get { return _LogId ?? Guid.NewGuid().ToString().ToUpper(); }
        //    set { _LogId = value; }
        //}

        public string UserId { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;
         
        public string Host { get; set; } = HttpContext.Current.Request.UserHostAddress;

        public string Result { get; set; }

        public string SaveData { get; set; }

        public string LogLevel { get; set; }

        public string Data_Action { get; set; }
        
        public string ControllersName { get; set; }

        public string ActionName { get; set; }
    }
}
