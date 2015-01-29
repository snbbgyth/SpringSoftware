using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpringSoftware.Web.DAL;
using SpringSoftware.Web.DAL.Manage;

namespace SpringSoftware.Web.Controllers
{
    public class JbimagesController : BaseController
    {
        //private readonly IPermissionService _permissionService;
        //private readonly IWebHelper _webHelper;

        public JbimagesController()
        {
            //this._permissionService = permissionService;
            //this._webHelper = webHelper;
        }

        [NonAction]
        protected virtual IList<string> GetAllowedFileTypes()
        {
            return new List<string>() { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
        }

        [HttpPost]
        public ActionResult Upload()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.HtmlEditorManagePictures))
            //{
            //    ViewData["resultCode"] = "failed";
            //    ViewData["result"] = "No access to this functionality";
            //    return View();
            //}

            if (Request.Files.Count == 0)
                throw new Exception("No file uploaded");

            var uploadFile = Request.Files[0];
            if (uploadFile == null)
            {
                ViewData["resultCode"] = "failed";
                ViewData["result"] = "No file name provided";
                return View();
            }

            var fileName = Path.GetFileName(uploadFile.FileName);
            if (String.IsNullOrEmpty(fileName))
            {
                ViewData["resultCode"] = "failed";
                ViewData["result"] = "No file name provided";
                return View();
            }

          
            var filePath = Path.Combine(ImageManage.UploadedPath, fileName);

            var fileExtension = Path.GetExtension(filePath);
            if (!GetAllowedFileTypes().Contains(fileExtension))
            {
                ViewData["resultCode"] = "failed";
                ViewData["result"] = string.Format("Files with {0} extension cannot be uploaded", fileExtension);
                return View();
            }

            uploadFile.SaveAs(filePath);

            ViewData["resultCode"] = "success";
            ViewData["result"] = "success";
            ViewData["filename"] = this.Url.Content(string.Format("{0}{1}", ImageManage.UploadedPath, fileName));
            return View();
        }
    }
}