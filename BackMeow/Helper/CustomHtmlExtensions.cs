using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using StoreDB.Enum;

namespace BackMeow.Helper
{
    //public static class CustomHtmlExtensions
    //{
    //    /// <summary>
    //    /// 警告 - 星號開頭為必填項目
    //    /// </summary>
    //    /// <param name="helper">The helper.</param>
    //    /// <returns></returns>
    //    public static MvcHtmlString NoteSite(this HtmlHelper helper)
    //    {
    //        return MvcHtmlString.Create($"<Label class ='col-md-2 control-label' style='color:red'>*字號為必填項目</label >");
    //    }

    //    /// <summary>
    //    /// 必填 - 顯示星號並以紅色顯示.
    //    /// </summary>
    //    /// <param name="helper">The helper.</param>
    //    /// <param name="labelString">The label string.</param>
    //    /// <returns></returns>
    //    public static MvcHtmlString RequiredLabel(this HtmlHelper helper, string labelString)
    //    {
    //        return MvcHtmlString.Create($"<Label class='col-md-2 control-label'><label style = 'color:red' >*</label >" + labelString + "</Label >");
    //    }

    //    /// <summary>
    //    /// 日期顯示 yyyy-MM-dd 若空字串自動塞系統今日日期
    //    /// </summary>
    //    /// <param name="helper">The helper.</param>
    //    /// <param name="TextBoxValue">The text box value.</param>
    //    /// <returns></returns>
    //    //public static MvcHtmlString CustomerDateHelper(this HtmlHelper helper, string DateString,
    //    //    string name, string displayName)
    //    //{
    //    //    string datapickerInput = "<input class='form-control datepicker' name = '" + name + "' placeholder='請輸入 "
    //    //        + displayName + "' type='text' value='" + DateString + "'" + " onkeydown = 'return false' />";
    //    //    if (!string.IsNullOrWhiteSpace(DateString))
    //    //    {
    //    //    }
    //    //}

    //    /// <summary>
    //    /// 控制項 int 若是空，不顯示0，而是空白.
    //    /// </summary>
    //    /// <param name="helper">The helper.</param>
    //    /// <param name="TextBoxValue">The text box value.</param>
    //    /// <returns></returns>
    //    public static MvcHtmlString FormatIntTextBox(this HtmlHelper helper, string ColumnId,
    //                                                string TextBoxplaceholder, string TextBoxValue)
    //    {
    //        string StrTextBox = string.IsNullOrWhiteSpace(TextBoxValue) == true ? "" : TextBoxValue.ToString();
    //        return MvcHtmlString.Create($"<input class='form-control' id='select_" + ColumnId + "' name='" + ColumnId + "' placeholder='請輸入 " + TextBoxplaceholder + "' type='number' value='" + StrTextBox + "' >");
    //    }

    //    /// <summary>
    //    /// 控制項 是否通過 驗證.
    //    /// </summary>
    //    /// <param name="helper">The helper.</param>
    //    /// <param name="TextBoxValue">The text box value.</param>
    //    /// <returns></returns>
    //    public static MvcHtmlString CheckLabel(this HtmlHelper helper, string Check, string DataAction)
    //    {
    //        DataAction action = (DataAction)Enum.Parse(typeof(DataAction), DataAction, true);
    //        string StrTextBox = "";
    //        if (action != StoreDB.StoreEnum.DataAction.Create)
    //        {
    //            StrTextBox = Convert.ToBoolean(Check) == true ? "已通過驗證" : "尚未通過驗證";
    //        }
    //        return MvcHtmlString.Create($"<Label class='col-md-4 control-label'>" + StrTextBox);
    //    }

    //    /// <summary>
    //    /// 一般 TextBox 只允許在 新增模式 下編輯.
    //    /// </summary>
    //    /// <param name="helper">The helper.</param>
    //    /// <param name="ColumnId">The column identifier.</param>
    //    /// <param name="TextBoxplaceholder">The text boxplaceholder.</param>
    //    /// <param name="TextBoxValue">The text box value.</param>
    //    /// <param name="DataAction">The data action.</param>
    //    /// <returns></returns>
    //    public static MvcHtmlString OnlyCreateCanEditTextBox(this HtmlHelper helper, string ColumnId,
    //                                                        string TextBoxplaceholder, string TextBoxValue, string DataAction)
    //    {
    //        DataAction action = (DataAction)Enum.Parse(typeof(DataAction), DataAction, true);
    //        if (action == StoreDB.StoreEnum.DataAction.Create)
    //        {
    //            return MvcHtmlString.Create($"<input class='form-control' id='select_" + ColumnId +
    //                                            "' name='" + ColumnId +
    //                                            "' placeholder='請輸入 " +
    //                                            TextBoxplaceholder +
    //                                            "' type='text' value='" +
    //                                            TextBoxValue + "'" + " >");
    //        }
    //        else
    //        {
    //            return MvcHtmlString.Create($"<input class='form-control' id='select_" + ColumnId +
    //                                            "' name='" + ColumnId +
    //                                            "' type='text' value='" +
    //                                            TextBoxValue + "'" +
    //                                            " readonly= 'readonly' >");
    //        }
    //    }

    //    /// <summary>
    //    /// 一般 TextBox - DatePicker 只允許在 新增模式 下編輯.
    //    /// </summary>
    //    /// <param name="helper">The helper.</param>
    //    /// <param name="ColumnId">The column identifier.</param>
    //    /// <param name="TextBoxplaceholder">The text boxplaceholder.</param>
    //    /// <param name="TextBoxValue">The text box value.</param>
    //    /// <param name="DataAction">The data action.</param>
    //    /// <returns></returns>
    //    public static MvcHtmlString OnlyCreateCanEditDatePicker(this HtmlHelper helper, string ColumnId,
    //                                                        string TextBoxplaceholder, string TextBoxValue, string DataAction)
    //    {
    //        DataAction action = (DataAction)Enum.Parse(typeof(DataAction), DataAction, true);
    //        if (action == StoreDB.StoreEnum.DataAction.Create)
    //        {
    //            return MvcHtmlString.Create($"<input class='form-control  datepicker' id='select_" + ColumnId +
    //                                            "' name='" + ColumnId +
    //                                            "' placeholder='請輸入 " +
    //                                            TextBoxplaceholder +
    //                                            "' type='text' value='" +
    //                                            TextBoxValue + "'" + " >");
    //        }
    //        else
    //        {
    //            return MvcHtmlString.Create($"<input class='form-control' id='select_" + ColumnId +
    //                                            "' name='" + ColumnId +
    //                                            "' type='text' value='" +
    //                                            TextBoxValue + "'" +
    //                                            " readonly= 'readonly' >");
    //        }
    //    }

    //    /// <summary>
    //    /// 下拉式選單.
    //    /// </summary>
    //    /// <param name="htmlHelper">The HTML helper.</param>
    //    /// <param name="linkText">The link text.</param>
    //    /// <param name="actionName">Name of the action.</param>
    //    /// <param name="controllerName">Name of the controller.</param>
    //    /// <returns></returns>
    //    public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
    //    {
    //        var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
    //        var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

    //        var builder = new TagBuilder("li")
    //        {
    //            InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString()
    //        };

    //        if (controllerName == currentController && actionName == currentAction)
    //            builder.AddCssClass("active");

    //        return new MvcHtmlString(builder.ToString());
    //    }

    //    //public static MvcHtmlString ModelType(this HtmlHelper htmlHelper,
    //    //    DataAction ActionType, bool Required)
    //    //{
    //    //    if (ActionType == DataAction.Create) // 新建立資料，Textbox
    //    //    {
    //    //    }
    //    //    else
    //    //    {
    //    //        // Lable 格式
    //    //    }
    //    //}
    //}
}