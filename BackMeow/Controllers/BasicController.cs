using StoreDB.Interface;
using StoreDB.Model.ViewModel;
using StoreDB.Repositories;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BackMeow.Controllers
{
    public class BasicController : Controller
    {
        private IPublicMethod _publicMethod;

        public BasicController()
        {
            _publicMethod = new PublicMethodRepository();
        }

        public ActionResult FileUpload(List<string> IDList)
        {
            return View();
        }

        // GET: Basic
        [HttpPost]
        public JsonResult Uploaded(IEnumerable<HttpPostedFileBase> upload)
        {
            #region
            //if (upload != null)
            //{
            //    if (upload.Count() > 0)
            //    {
            //        foreach (var uploadFile in upload)
            //        {
            //            if (uploadFile.ContentLength > 0)
            //            {
            //                string savePath = Server.MapPath(FileUrl);
            //                uploadFile.SaveAs(savePath + uploadFile.FileName);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    //for (int i = 0; i < Request.Files.Count; i++)
            //    //{
            //    //    var file = Request.Files[i];
            //    //    var fileName = Path.GetFileName(file.FileName);
            //    //    var path = Path.Combine(Server.MapPath("~/Upload/TempImage/"), fileName);
            //    //    file.SaveAs(path);
            //    //}
            //}
            #endregion
            //RedirectToAction("Action", new { id = 99 });
            //IEnumerable<PictureInfo> PictureInfoList = _StaticHtmlService.ReturnPictureInfoList(PicGroupID);
            //return PartialView("_UploadFiles", PictureInfoList);
            return Json(new { data = "true" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult shaPWD(string OriginalStr)
        {
            string data = _publicMethod.SHA256Pwd(OriginalStr);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
    }
}