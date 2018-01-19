using StoreDB.Enum;
using System;

namespace StoreDB
{
    public class Utility
    {
        /// <summary>
        /// 對於資料使用的動作 String to Enum
        /// </summary>
        public Actions StringToEnumActions(string getActions)
        {
            Actions ActionsValue = (Actions)System.Enum.Parse(typeof(Actions), getActions);
            return ActionsValue;
        } 
    }
}
