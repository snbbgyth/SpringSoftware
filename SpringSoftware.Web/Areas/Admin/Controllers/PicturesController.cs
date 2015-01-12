using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;
using SpringSoftware.Web.Areas.Admin.Models;
using SpringSoftware.Web.Models;

namespace SpringSoftware.Web.Areas.Admin.Controllers
{
    public class PicturesController : Controller
    {
        private IPictureDal _pictureDal;

        public PicturesController()
        {
            _pictureDal = DependencyResolver.Current.GetService<IPictureDal>();
        }

        // GET: Pictures
        public async Task<ActionResult> Index()
        {
            return View(await _pictureDal.QueryAllAsync());
        }

        // GET: Pictures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = await _pictureDal.QueryByIdAsync(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // GET: Pictures/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Upload()
        {

            return View();
        }

        private byte[] GetBytes(HttpPostedFileBase file)
        {
            var stream = file.InputStream;
            var fileBinary = new byte[stream.Length];
            stream.Read(fileBinary, 0, fileBinary.Length);
            return fileBinary;
        }

        private string GetContentType(HttpPostedFileBase file)
        {
            var contentType = file.ContentType;
            var fileExtension = Path.GetExtension(file.FileName);
            if (!String.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            //contentType is not always available 
            //that's why we manually update it here
            //http://www.sfsu.edu/training/mimetype.htm
            if (String.IsNullOrEmpty(contentType))
            {
                switch (fileExtension)
                {
                    case ".bmp":
                        contentType = "image/bmp";
                        break;
                    case ".gif":
                        contentType = "image/gif";
                        break;
                    case ".jpeg":
                    case ".jpg":
                    case ".jpe":
                    case ".jfif":
                    case ".pjpeg":
                    case ".pjp":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".tiff":
                    case ".tif":
                        contentType = "image/tiff";
                        break;
                    default:
                        break;
                }
            }
            return contentType;
        }

        private void GenerateTempThumbnail(HttpPostedFileBase file,int productId=0)
        {
            // create an image object, using the filename we just retrieved
            var image = Image.FromStream(file.InputStream);
            // create the actual thumbnail image
            var thumbnailImage = image.GetThumbnailImage(280, 280, ThumbnailCallback, IntPtr.Zero);
            string imageSavePath;
            if(productId>0)
                  imageSavePath = Path.Combine("~/Images/SaveUpload/Product/Thumbnails/",productId.ToString(),file.FileName);
            else
              imageSavePath = Path.Combine("~/Images/SaveUpload/Product/Thumbnails/Temp/",file.FileName);
            thumbnailImage.Save(imageSavePath,ImageFormat.Jpeg);
             
        }

        public bool ThumbnailCallback()
        {
            return true;
        }

        [HttpPost]
        public async Task<ActionResult> Upload(UploadFileViewModel uploadFileModel)
        {
            if (!ModelState.IsValid)
            {
                return View(uploadFileModel);
            }

            if (uploadFileModel.File == null)
                throw new ArgumentException("No file uploaded");
           
            var picture = new Picture();
            picture.FileName = uploadFileModel.File.FileName;
            picture.MimeType = GetContentType(uploadFileModel.File);
            picture.PictureBinary = GetBytes(uploadFileModel.File);
            GenerateTempThumbnail(uploadFileModel.File);

            //return Content("File Uploaded.");

            return View();
        }

        // POST: Pictures/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MimeType,PictureBinary,FileName,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] Picture picture)
        {
            if (ModelState.IsValid)
            {

                await _pictureDal.InsertAsync(picture);
                return RedirectToAction("Index");
            }

            return View(picture);
        }

        // GET: Pictures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = await _pictureDal.QueryByIdAsync(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // POST: Pictures/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MimeType,PictureBinary,FileName,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] Picture picture)
        {
            if (ModelState.IsValid)
            {

                await _pictureDal.ModifyAsync(picture);
                return RedirectToAction("Index");
            }
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = await _pictureDal.QueryByIdAsync(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            await _pictureDal.DeleteByIdAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
