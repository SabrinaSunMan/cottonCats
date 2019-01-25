using StoreDB.Enum;
using StoreDB.Model.ViewModel;
using StoreDB.Repositories;
using System.IO;

namespace BackMeow.Service
{
    /// <summary>
    /// 後台 各種共用 方法取得.
    /// </summary>
    public class PublicService
    {
        private readonly MemeberRepository _MemberRep;
        private readonly PublicMethodRepository _PublicRep;
        public PublicMethodResult ReturnModel;

        public PublicService()
        {
            _PublicRep = new PublicMethodRepository();
            ReturnModel = new PublicMethodResult();
        }

        /// <summary>
        /// Gets the sh a256 password.
        /// </summary>
        /// <param name="OriginalStr">The original string.</param>
        /// <returns></returns>
        public PublicMethodResult GetSHA256Pwd(string OriginalStr)
        {
            try
            {
                ReturnModel.ResultBool = true;
                ReturnModel.ActionResult = DataAction.CreateScuess;
                ReturnModel.Result = _PublicRep.SHA256Pwd(OriginalStr);
            }
            catch (IOException ex)
            {
                ReturnModel.ResultBool = false;
                ReturnModel.ActionResult = DataAction.CreateFail;
                ReturnModel.Result = ex.ToString();
            }
            return ReturnModel;
        }

        /// <summary>
        /// Gets the contract check list.
        /// </summary>
        /// <param name="DefaultValue">The default value.</param>
        /// <returns></returns>
        public PublicMethodResult GetContractCheckList(string DefaultValue)
        {
            ReturnModel.ResultBool = true;
            ReturnModel.ActionResult = DataAction.CreateScuess;
            ReturnModel.Result = _PublicRep.ContractCheckList(DefaultValue);
            return ReturnModel;
        }
    }
}