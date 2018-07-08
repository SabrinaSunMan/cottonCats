using System.Web.Mvc;
using System.Web.Mvc.Html;

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
        public static MvcHtmlString RequiredLabel(this HtmlHelper helper, string labelString)
        {
            return MvcHtmlString.Create($"<Label class='col-md-2 control-label'><label style = 'color:red' >*</label >" + labelString + "</Label >");
        }

        /// <summary>
        /// 日期顯示 yyyy-MM-dd 若空字串自動塞系統今日日期
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="TextBoxValue">The text box value.</param>
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

        /// <summary>
        /// 搜尋列 int 若是空，不顯示0，而是空白.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="TextBoxValue">The text box value.</param>
        /// <returns></returns>
        public static MvcHtmlString FormatIntTextBox(this HtmlHelper helper, string ColumnId, string TextBoxplaceholder, int TextBoxValue)
        {
            string StrTextBox = "";
            if (TextBoxValue == 0)
            {
            }
            return MvcHtmlString.Create($"<input class='form-control' id='select_" + ColumnId + "' name='Header." + ColumnId + "' placeholder='請輸入 '" + TextBoxplaceholder + "' type='text' value='" + StrTextBox + "' >");
            //return MvcHtmlString.Create($"<Label class='col-md-2 control-label'><label style = 'color:red' >*</label >" + labelString + "</Label >");
        }

        /// <summary>
        /// 下拉式選單.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns></returns>
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            var builder = new TagBuilder("li")
            {
                InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString()
            };

            if (controllerName == currentController && actionName == currentAction)
                builder.AddCssClass("active");

            return new MvcHtmlString(builder.ToString());
        }
    }
}