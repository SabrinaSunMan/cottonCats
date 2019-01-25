using StoreDB.Enum;
using System;

namespace StoreDB
{
    public class Utility
    {
        /// <summary>
        /// 對於資料使用的動作 String to Enum
        /// </summary>
        public DataAction StringToEnumActions(string getActions)
        {
            DataAction ActionsValue = (DataAction)System.Enum.Parse(typeof(DataAction), getActions);
            return ActionsValue;
        }
    }
}