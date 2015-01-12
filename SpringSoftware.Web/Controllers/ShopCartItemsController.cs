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
using SpringSoftware.Web.Models;

namespace SpringSoftware.Web.Controllers
{
    public class ShopCartItemsController : Controller
    {
        private IShopCartItemDal _shopCartItemDal;

        public ShopCartItemsController()
        {
            _shopCartItemDal = DependencyResolver.Current.GetService<IShopCartItemDal>();
        }

        // GET: ShopCartItems
        public async Task<ActionResult> Index()
        {
            return View(await _shopCartItemDal.QueryAllAsync());
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

        // GET: ShopCartItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopCartItems/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomerId,Count,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] ShopCartItem shopCartItem)
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomerId,Count,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] ShopCartItem shopCartItem)
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
