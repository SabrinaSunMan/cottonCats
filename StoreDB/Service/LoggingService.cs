using StoreDB.Enum;
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

        public void CreateNLog(string actionName, string controllerName, string Action,
            string ParameterStr, string userid, LogLevel leve)
        {
            NLog_Error saveData = new NLog_Error()
            {
                ActionName = actionName,
                ControllersName = controllerName,
                Data_Action = Action,
                SaveData = ParameterStr,
                UserId = userid,
                LogLevel = leve.ToString()
            };
            _logRep.Create(saveData);
            _logRep.Commit();
        }

        /// <summary>
        /// Returns the log information.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="result">The result.</param>
        /// <param name="savedata">The savedata.</param>
        /// <param name="loglevel">The loglevel.</param>
        /// <param name="data_action">The data action.</param>
        /// <param name="controllersname">The controllersname.</param>
        /// <param name="actionname">The actionname.</param>
        /// <returns></returns>
        private NLog_Error ReturnLogInfo(string userid, string result,
            string savedata, string loglevel, string data_action,
            string controllersname, string actionname)
        {
            NLog_Error package = new NLog_Error()
            {
                ActionName = actionname,
                ControllersName = controllersname,
                Data_Action = data_action,
                LogLevel = loglevel,
                SaveData = savedata,
                Result = result,
                UserId = userid
            };
            return package;
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