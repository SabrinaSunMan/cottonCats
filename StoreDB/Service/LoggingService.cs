using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Service
{ 
    public class LoggingService
    {
        private static Logger LogWriter = NLog.LogManager.GetCurrentClassLogger();

        public void TESTLog(bool ResultBool)
        {
            LogEventInfo theEvent = new LogEventInfo(ResultBool == true ? LogLevel.Info : LogLevel.Error,
                "", ResultBool.ToString());

            theEvent.Properties["result"] = ResultBool;
            theEvent.Properties["savedata"] = "Hi";
            theEvent.Properties["data_Action"] = "";
            theEvent.Properties["Orignal_Page"] = "";

            theEvent.Properties["Statement"] = "";
            theEvent.Properties["ControllersName"] = "";
            theEvent.Properties["ActionName"] = "";

            //theEvent.Properties["ResultAllStr"] = "123"; //Only For File 

            LogWriter.Log(theEvent);
            //LogEventInfo theEvent = new LogEventInfo(ResultBool == true ? LogLevel.Info : LogLevel.Error,
            //    "", ResultBool.ToString());

            //theEvent.Properties["result"] = ResultBool;
            //theEvent.Properties["savedata"] = SaveString;
            //theEvent.Properties["data_Action"] = actions.ToString();
            //theEvent.Properties["Orignal_Page"] = pages.ToString();

            //theEvent.Properties["Statement"] = Statement;
            //theEvent.Properties["ControllersName"] = _controllerName;
            //theEvent.Properties["ActionName"] = _actionName;

            //theEvent.Properties["ResultAllStr"] = LogStr; //Only For File 

            //LogWriter.Log(theEvent);

             
        }
            
    }
}
