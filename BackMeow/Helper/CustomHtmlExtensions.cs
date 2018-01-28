using System.Web.Mvc;

namespace BackMeow.Helper
{
    public static class CustomHtmlExtensions
    {
        /// <summary>
        /// 警告 - 星號開頭為必填項目
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns></returns>
        public static MvcHtmlString NoteSite(this HtmlHelper helper)
        {
            return MvcHtmlString.Create($"<Label class ='col-md-2 control-label' style='color:red'>*字號為必填項目</label >");
        }

        /// <summary>
        /// 必填 - 顯示星號並以紅色顯示.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="labelString">The label string.</param>
        /// <returns></returns>
        public static MvcHtmlString RequiredLabel(this HtmlHelper helper,string labelString)
        {  
            return MvcHtmlString.Create($"<Label class='col-md-2 control-label'><label style = 'color:red' >*</label >" + labelString+ "</Label >");
        }

        /// <summary>
        /// 日期顯示 yyyy-MM-dd 若空字串自動塞系統今日日期
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="DateString">The date string.</param>
        /// <returns></returns>
        //public static MvcHtmlString CustomerDateHelper(this HtmlHelper helper, string DateString,
        //    string name, string displayName)
        //{
        //    string datapickerInput = "<input class='form-control datepicker' name = '" + name + "' placeholder='請輸入 "
        //        + displayName + "' type='text' value='" + DateString + "'" + " onkeydown = 'return false' />";

        //    if (!string.IsNullOrWhiteSpace(DateString))
        //    {

        //    }
        //}
    }
}