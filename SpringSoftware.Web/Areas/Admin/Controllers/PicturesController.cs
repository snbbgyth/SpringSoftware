using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;
using SpringSoftware.Web.Areas.Admin.Models;
using SpringSoftware.Web.DAL;
using SpringSoftware.Web.DAL.Manage;
using SpringSoftware.Web.Help;
using SpringSoftware.Web.Models;

namespace SpringSoftware.Web.Areas.Admin.Controllers
{
    [MyAuthorize(Roles = "Admin")]
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
            picture.MimeType =ImageManage.GetContentType(uploadFileModel.File);
            picture.PictureBinary = ImageManage.GetBytes(uploadFileModel.File);
            picture.Id = _pictureDal.Insert(picture);
            uploadFileModel.File.SaveAs(ImageManage.GetOriginalImagePath(picture));
            HandleQueue.Instance.Add(picture);
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
