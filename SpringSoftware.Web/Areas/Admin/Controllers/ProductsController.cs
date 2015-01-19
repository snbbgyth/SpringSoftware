using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;
using SpringSoftware.Core.Model;
using SpringSoftware.Web.Areas.Admin.Models;
using SpringSoftware.Web.DAL;
using SpringSoftware.Web.Help;
using SpringSoftware.Web.Models;

namespace SpringSoftware.Web.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        private IProductDal _productDal;
        private IPictureDal _pictureDal;
        private IProductPictureDal _productPictureDal;

        public ProductsController()
        {
            _productDal = DependencyResolver.Current.GetService<IProductDal>();
            _pictureDal = DependencyResolver.Current.GetService<IPictureDal>();
            _productPictureDal = DependencyResolver.Current.GetService<IProductPictureDal>();
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {
            return View(await _productDal.QueryAllAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _productDal.QueryByIdAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var model = new ProductViewModel();
            return View(model);
        }

        // POST: Products/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel productView)
        {
            if (ModelState.IsValid)
            {
                InitInsert(productView.Product);
                productView.Product.Id = await _productDal.InsertAsync(productView.Product);
                if (productView.UploadFile.File != null)
                {
                   AddPictureToProduct(productView);
                }
                return RedirectToAction("Index");
            }
            return View(productView);
        }

        private async void AddPictureToProduct(ProductViewModel productView)
        {
            var picture = await AddUploadFile(productView);
            var productPicture = new ProductPicture();
            productPicture.Picture.Id = picture.Id;
            productPicture.Product.Id = productView.Product.Id;
            InitInsert(productPicture);
            await _productPictureDal.InsertAsync(productPicture);
            productView.PictureList.Add(picture);
            productPicture.Product.Id = productView.Product.Id;
            productView.ProductPictureList.Add(productPicture);
        }

        private async Task<Picture> AddUploadFile(ProductViewModel productView)
        {
            var picture = new Picture();
            picture.FileName = productView.UploadFile.File.FileName;
            picture.MimeType = ImageHelper.GetContentType(productView.UploadFile.File);
            InitInsert(picture);
            picture.Id = await _pictureDal.InsertAsync(picture);
            productView.UploadFile.File.SaveAs(ImageHelper.GetOriginalImagePath(picture));
            HandleQueue.Instance.Add(picture);
            return picture;
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _productDal.QueryByIdAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Discrption,Price,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productDal.ModifyAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _productDal.QueryByIdAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _productDal.DeleteByIdAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
