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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;
using SpringSoftware.Web.Areas.Admin.Models;
using SpringSoftware.Web.DAL;
using SpringSoftware.Web.DAL.Manage;
using SpringSoftware.Web.Models;

namespace SpringSoftware.Web.Controllers
{
    public class OrdersController : BaseController
    {
        private IOrderDal _orderDal;
        public UserManager<ApplicationUser> _userManager { get; private set; }
        private ApplicationDbContext context { get;  set; }

        private IOrderItemDal _orderItemDal;

        private static IShopCartItemDal _shopCartItemDal;

        public OrdersController()
        {
            context = new ApplicationDbContext();
            _orderDal = DependencyResolver.Current.GetService<IOrderDal>();
            _orderItemDal = DependencyResolver.Current.GetService<IOrderItemDal>();
            _shopCartItemDal = DependencyResolver.Current.GetService<IShopCartItemDal>();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            return View(await _orderDal.QueryAllAsync());
        }

        public async Task<ActionResult> Submit()
        {
            var user = _userManager.FindByName(User.Identity.Name);
            var orderView = new OrderViewModel();
            orderView.OrderItemViewList = await OrderManage.GetOrderItemsByUserName(User.Identity.Name);
            orderView.Order.CustomerName = User.Identity.Name;
            orderView.Order.CustomerPhone = user.PhoneNumber;
            orderView.Order.TotalPrice = orderView.OrderItemViewList.Sum(t => t.OrderItem.Total);
            return View(orderView);
        }

        [HttpPost]
        public async Task<ActionResult> Submit(OrderViewModel orderView)
        {
            var order = orderView.Order;
            InitInsert(order);
            OrderManage.GenerateOrderNumber(order);
            var orderId=await _orderDal.InsertAsync(order);
            foreach (var orderItemView in orderView.OrderItemViewList)
            {
                orderItemView.OrderItem.OrderId = orderId;
                InitInsert(orderItemView.OrderItem);
                await _orderItemDal.InsertAsync(orderItemView.OrderItem);
                await _shopCartItemDal.DeleteByIdAsync(orderItemView.ShopCartItem.Id);
            }
            return RedirectToAction("Index");
        }



        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderView = await OrderManage.GetOrderView(id.Value);
            if (orderView == null)
            {
                return HttpNotFound();
            }
            return View(orderView);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomerId,CustomerPhone,ReceiveAddress,TotalPrice,IsPay,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderDal.InsertAsync(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await _orderDal.QueryByIdAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomerId,CustomerPhone,ReceiveAddress,TotalPrice,IsPay,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderDal.ModifyAsync(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await _orderDal.QueryByIdAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _orderDal.DeleteByIdAsync(id);
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
