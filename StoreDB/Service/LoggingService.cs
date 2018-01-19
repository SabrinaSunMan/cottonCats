
using StoreDB.Interface;
using StoreDB.Model.Partials;
using StoreDB.Repositories;
using System;

namespace StoreDB.Service
{ 
    public class LoggingService
    {
        private readonly IRepository<NLog_Error> _logRep;
        private readonly IUnitOfWork _unitOfWork;

        public LoggingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logRep = new Repository<NLog_Error>(unitOfWork);
        }

        public void Add(string firstName, string lastName, string email, Guid orderId)
        {
            _logRep.Create(new NLog_Error
            {
                LogId = orderId.ToString().ToUpper(),
                SaveData = "TEST" 
            });
        }

        public void Save()
        {
            _unitOfWork.Save();
        }

        //private static Logger LogWriter = NLog.LogManager.GetCurrentClassLogger();

        //public void TESTLog(bool ResultBool)
        //{
        //    LogEventInfo theEvent = new LogEventInfo(ResultBool == true ? LogLevel.Info : LogLevel.Error,
        //        "", ResultBool.ToString());

        //    theEvent.Properties["result"] = ResultBool;
        //    theEvent.Properties["savedata"] = "Hi";
        //    theEvent.Properties["data_Action"] = "";
        //    theEvent.Properties["Orignal_Page"] = "";

        //    theEvent.Properties["Statement"] = "";
        //    theEvent.Properties["ControllersName"] = "";
        //    theEvent.Properties["ActionName"] = "";

        //    //theEvent.Properties["ResultAllStr"] = "123"; //Only For File 

        //    LogWriter.Log(theEvent);
        //    //LogEventInfo theEvent = new LogEventInfo(ResultBool == true ? LogLevel.Info : LogLevel.Error,
        //    //    "", ResultBool.ToString());

        //    //theEvent.Properties["result"] = ResultBool;
        //    //theEvent.Properties["savedata"] = SaveString;
        //    //theEvent.Properties["data_Action"] = actions.ToString();
        //    //theEvent.Properties["Orignal_Page"] = pages.ToString();

        //    //theEvent.Properties["Statement"] = Statement;
        //    //theEvent.Properties["ControllersName"] = _controllerName;
        //    //theEvent.Properties["ActionName"] = _actionName;

        //    //theEvent.Properties["ResultAllStr"] = LogStr; //Only For File 

        //    //LogWriter.Log(theEvent);

             
        //}
            
    }
}
