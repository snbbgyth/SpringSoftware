using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImageResizer;
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

        private void GenerateThumbnail(Picture picture)
        {
            try
            {
 
                //var image = Image.FromStream(file.InputStream);
          
                //using (var thumbnailImage = image.GetThumbnailImage(280, 280, ThumbnailCallback, IntPtr.Zero))
                //{
                //    var imageSavePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "\\Images\\SaveUpload\\Product\\Thumbnails\\", pictureId.ToString() + ".jpg");
                //    thumbnailImage.Save(imageSavePath, ImageFormat.Jpeg);
                //}
                var thumbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "\\Images\\SaveUpload\\Product\\Thumbnails\\", picture.Id + ".jpg");
                if (!System.IO.File.Exists(thumbFilePath))
                {
                    using (var stream = new MemoryStream(picture.PictureBinary))
                    {
                        Bitmap b = null;
                        try
                        {
                            //try-catch to ensure that picture binary is really OK. Otherwise, we can get "Parameter is not valid" exception if binary is corrupted for some reasons
                            b = new Bitmap(stream);
                        }
                        catch (ArgumentException exc)
                        {
                           // _logger.Error(string.Format("Error generating picture thumb. ID={0}", picture.Id), exc);
                        }
                        if (b == null)
                        {
                            //bitmap could not be loaded for some reasons
                           // return url;
                        }

                        var newSize = CalculateDimensions(b.Size, 0);// targetSize);

                        var destStream = new MemoryStream();
                        ImageBuilder.Current.Build(b, destStream, new ResizeSettings()
                        {
                            Width = newSize.Width,
                            Height = newSize.Height,
                            Scale = ScaleMode.Both,
                            Quality = 0
                        });
                        var destBinary = destStream.ToArray();
                        System.IO.File.WriteAllBytes(thumbFilePath, destBinary);

                        b.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected virtual Size CalculateDimensions(Size originalSize, int targetSize,
           ResizeType resizeType = ResizeType.LongestSide, bool ensureSizePositive = true)
        {
            var newSize = new Size();
            switch (resizeType)
            {
                case ResizeType.LongestSide:
                    if (originalSize.Height > originalSize.Width)
                    {
                        // portrait 
                        newSize.Width = (int)(originalSize.Width * (float)(targetSize / (float)originalSize.Height));
                        newSize.Height = targetSize;
                    }
                    else
                    {
                        // landscape or square
                        newSize.Height = (int)(originalSize.Height * (float)(targetSize / (float)originalSize.Width));
                        newSize.Width = targetSize;
                    }
                    break;
                case ResizeType.Width:
                    newSize.Height = (int)(originalSize.Height * (float)(targetSize / (float)originalSize.Width));
                    newSize.Width = targetSize;
                    break;
                case ResizeType.Height:
                    newSize.Width = (int)(originalSize.Width * (float)(targetSize / (float)originalSize.Height));
                    newSize.Height = targetSize;
                    break;
                default:
                    throw new Exception("Not supported ResizeType");
            }

            if (ensureSizePositive)
            {
                if (newSize.Width < 1)
                    newSize.Width = 1;
                if (newSize.Height < 1)
                    newSize.Height = 1;
            }

            return newSize;
        }

        public enum ResizeType
        {
            LongestSide,
            Width,
            Height
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
            picture.Id = _pictureDal.Insert(picture);
            GenerateThumbnail(picture);

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
