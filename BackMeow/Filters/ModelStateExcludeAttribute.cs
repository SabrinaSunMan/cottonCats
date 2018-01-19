using System;
using System.Web.Http.Controllers; 
using System.Web.Mvc;

namespace BackMeow.Filters
{
    /// <summary>
    /// tryUpdateModel_排除某欄位不更新，其餘皆會更新,以ViewData回傳
    /// </summary>
    public class ModelStateExcludeAttribute : ActionFilterAttribute
    {
        public string [] Exclude { get; set; }
        public ModelStateExcludeAttribute()
        {
            Exclude = new string[]
            {
                "Id",
                "CreateTime" 
            };
        }

        public ModelStateExcludeAttribute(string exclude)
        {
            if (exclude != null)
            {
                if (exclude.Contains(","))
                    Exclude = exclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                else
                    Exclude = new string[] { exclude };
            }

        }
         
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            foreach (var item in Exclude)
            {
                //將排除欄位一一設定
                filterContext.Controller.ViewData.ModelState.Remove(item);
            }
            //設定完畢後將他利用 ViewData 傳到 Action 內
            filterContext.Controller.ViewData["Exclude"] = Exclude;
        }
    }
}