using StoreDB.Interface;
using StoreDB.Model.Partials;
using StoreDB.Repositories;
using System;
using System.Web.Mvc;

namespace BackMeow.Filters
{
    public class ResultFillterAttribute : FilterAttribute, IResultFilter
    {
        //https://dotblogs.com.tw/jamis/2016/01/09/125624

        private readonly IRepository<NLog_Error> _logRep;
        private readonly IUnitOfWork _unitOfWork;

        public ResultFillterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logRep = new Repository<NLog_Error>(unitOfWork);
        }

        /// <summary>
        /// 在動作結果執行之後呼叫。
        /// </summary>
        /// <param name="filterContext">篩選內容。</param>
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}