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
using SpringSoftware.Web.Areas.Admin.Models;

namespace SpringSoftware.Web.Controllers
{
    public class OrderItemsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private IOrderItemDal _orderItemDal;

        public OrderItemsController()
        {
            _orderItemDal = DependencyResolver.Current.GetService<IOrderItemDal>();
        }

        // GET: OrderItems
        public async Task<ActionResult> Index()
        {

            return View(await _orderItemDal.QueryAllAsync());
        }

        // GET: OrderItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = await _orderItemDal.QueryByIdAsync(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // GET: OrderItems/Create
        public ActionResult Create()
        {
            //ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: OrderItems/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProductId,Count,Price,OrderId,Total,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {

                await _orderItemDal.InsertAsync(orderItem);
                return RedirectToAction("Index");
            }

            //ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: OrderItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = await _orderItemDal.QueryByIdAsync(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderItem.ProductId);
            return View(orderItem);
        }

        // POST: OrderItems/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProductId,Count,Price,OrderId,Total,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                await _orderItemDal.ModifyAsync(orderItem);
                return RedirectToAction("Index");
            }
            //ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: OrderItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = await _orderItemDal.QueryByIdAsync(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _orderItemDal.DeleteByIdAsync(id);
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
