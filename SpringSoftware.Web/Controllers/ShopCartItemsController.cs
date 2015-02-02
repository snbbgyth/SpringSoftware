using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;
using SpringSoftware.Web.Areas.Admin.Models;
using SpringSoftware.Web.DAL;
using SpringSoftware.Web.Models;

namespace SpringSoftware.Web.Controllers
{
    public class ShopCartItemsController : BaseController
    {
        private IShopCartItemDal _shopCartItemDal;
        private IProductDal _productDal;

        public ShopCartItemsController()
        {
            _shopCartItemDal = DependencyResolver.Current.GetService<IShopCartItemDal>();
            _productDal = DependencyResolver.Current.GetService<IProductDal>();
        }

        // GET: ShopCartItems
        public async Task<ActionResult> Index()
        {
            var entityList = await _shopCartItemDal.QueryByFunAsync(t => t.CustomerName == User.Identity.Name);
            foreach (var entity in entityList)
            {
                entity.Product =await _productDal.QueryByIdAsync(entity.ProductId);
            }
            return View(entityList);
        }

        // GET: ShopCartItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopCartItem shopCartItem = await _shopCartItemDal.QueryByIdAsync(id);
            if (shopCartItem == null)
            {
                return HttpNotFound();
            }
            return View(shopCartItem);
        }

        [HttpPost]
        public async Task<ActionResult> AddShopCart(int productId, int count)
        {
            if (count <= 0)
                return Json(new { Result = false,Message="请选择数商品数量。" }, JsonRequestBehavior.AllowGet);
            try
            {
                var entity = new ShopCartItem
                {
                    Count = count,
                    ProductId = productId,
                    CustomerName = User.Identity.Name
                };
                InitInsert(entity);
                await _shopCartItemDal.InsertAsync(entity);
            }
            catch (Exception ex)
            {
            }
            return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: ShopCartItems/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomerName,Count,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] ShopCartItem shopCartItem)
        {
            if (ModelState.IsValid)
            {
                await _shopCartItemDal.InsertAsync(shopCartItem);
                return RedirectToAction("Index");
            }
            return View(shopCartItem);
        }

        // GET: ShopCartItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopCartItem shopCartItem = await _shopCartItemDal.QueryByIdAsync(id);
            if (shopCartItem == null)
            {
                return HttpNotFound();
            }
            return View(shopCartItem);
        }

        // POST: ShopCartItems/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomerName,Count,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] ShopCartItem shopCartItem)
        {
            if (ModelState.IsValid)
            {
                await _shopCartItemDal.ModifyAsync(shopCartItem);
                return RedirectToAction("Index");
            }
            return View(shopCartItem);
        }

        // GET: ShopCartItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopCartItem shopCartItem = await _shopCartItemDal.QueryByIdAsync(id);
            if (shopCartItem == null)
            {
                return HttpNotFound();
            }
            return View(shopCartItem);
        }

        // POST: ShopCartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _shopCartItemDal.DeleteByIdAsync(id);
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
