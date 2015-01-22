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
            return View(await GetByProductId(id));
        }

        private async Task<ProductViewModel> GetByProductId(int? id)
        {
            var productView = new ProductViewModel();
            if (id == null)
            {
                return productView;
            }
            productView.Product = await _productDal.QueryByIdAsync(id);
            if (productView.Product == null)
            {
                return productView;
            }
            productView.ProductPictureList = new List<ProductPicture>(await _productPictureDal.QueryByFunAsync(t => t.ProductId == id));
            if (productView.ProductPictureList.Any())
            {
                foreach (var productPicture in productView.ProductPictureList)
                {
                    var picture = await _pictureDal.QueryByIdAsync(productPicture.PictureId);
                    if (picture != null)
                        productView.PictureList.Add(picture);
                }
            }
            return productView;
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
                    await AddPictureToProduct(productView);
                }
                return RedirectToAction("Index");
            }
            return View(productView);
        }

        private async Task<int> AddPictureToProduct(ProductViewModel productView)
        {
            var picture = await AddUploadFile(productView);
            var productPicture = new ProductPicture();
            productPicture.PictureId = picture.Id;
            productPicture.ProductId = productView.Product.Id;
            InitInsert(productPicture);
            await _productPictureDal.InsertAsync(productPicture);
            productView.PictureList.Add(picture);
            productView.ProductPictureList.Add(productPicture);
            return 1;
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
            return View(await GetByProductId(id));
        }

        // POST: Products/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Product,PictureList,ProductPictureList,UploadFile")] ProductViewModel productView)
        {
            InitModify(productView.Product);
            await _productDal.ModifyAsync(productView.Product);
            if (productView.UploadFile.File != null)
            {
                await AddPictureToProduct(productView);
            }
            return View(productView);
        }


        public async Task<ActionResult> EditPicture(int id, int displayOrder)
        {
            var entity = _productPictureDal.QueryById(id);
            entity.DisplayOrder = displayOrder;
            InitModify(entity);
            _productPictureDal.Modify(entity);

            return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
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
