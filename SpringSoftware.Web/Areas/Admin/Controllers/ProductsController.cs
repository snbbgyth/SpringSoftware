﻿using System;
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

        #region Product pictures

        public ActionResult ProductPictureAdd(int pictureId, int displayOrder, int productId)
        {
           

            if (pictureId == 0)
                throw new ArgumentException();

            //var product = _productService.GetProductById(productId);
            //if (product == null)
            //    throw new ArgumentException("No product found with the specified id");

            ////a vendor should have access only to his products
            //if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
            //    return RedirectToAction("List");

            //_productService.InsertProductPicture(new ProductPicture()
            //{
            //    PictureId = pictureId,
            //    ProductId = productId,
            //    DisplayOrder = displayOrder,
            //});

            //_pictureService.SetSeoFilename(pictureId, _pictureService.GetPictureSeName(product.Name));

            return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProductPictureList( int productId)
        {
 

            //if (_workContext.CurrentVendor != null)
            //{
            //    var product = _productService.GetProductById(productId);
            //    if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
            //    {
            //        return Content("This is not your product");
            //    }
            //}

            //var productPictures = _productService.GetProductPicturesByProductId(productId);
            //var productPicturesModel = productPictures
            //    .Select(x =>
            //    {
            //        return new ProductModel.ProductPictureModel()
            //        {
            //            Id = x.Id,
            //            ProductId = x.ProductId,
            //            PictureId = x.PictureId,
            //            PictureUrl = _pictureService.GetPictureUrl(x.PictureId),
            //            DisplayOrder = x.DisplayOrder
            //        };
            //    })
            //    .ToList();

            //var gridModel = new DataSourceResult
            //{
            //    Data = productPicturesModel,
            //    Total = productPicturesModel.Count
            //};

            //return Json(gridModel);

            return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProductPictureUpdate(ProductPicture model)
        {

            //var productPicture = _productService.GetProductPictureById(model.Id);
            //if (productPicture == null)
            //    throw new ArgumentException("No product picture found with the specified id");

            //if (_workContext.CurrentVendor != null)
            //{
            //    var product = _productService.GetProductById(productPicture.ProductId);
            //    if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
            //    {
            //        return Content("This is not your product");
            //    }
            //}

            //productPicture.DisplayOrder = model.DisplayOrder;
            //_productService.UpdateProductPicture(productPicture);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult ProductPictureDelete(int id)
        {
      
            //var productPicture = _productService.GetProductPictureById(id);
            //if (productPicture == null)
            //    throw new ArgumentException("No product picture found with the specified id");

            //var productId = productPicture.ProductId;
 
            //if (_workContext.CurrentVendor != null)
            //{
            //    var product = _productService.GetProductById(productId);
            //    if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
            //    {
            //        return Content("This is not your product");
            //    }
            //}
            //var pictureId = productPicture.PictureId;
            //_productService.DeleteProductPicture(productPicture);
            //var picture = _pictureService.GetPictureById(pictureId);
            //_pictureService.DeletePicture(picture);

            return new NullJsonResult();
        }

        #endregion
    }
}
