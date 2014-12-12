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
    public class NewsTypesController : Controller
    {
        private INewsTypeDal _newsTypeDal;

        public NewsTypesController()
        {
            _newsTypeDal = DependencyResolver.Current.GetService<INewsTypeDal>();
        }

        // GET: NewsTypes
        public async Task<ActionResult> Index()
        {

            return View(await _newsTypeDal.QueryAllAsync());
        }

        // GET: NewsTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsType newsType = await _newsTypeDal.QueryByIdAsync(id.ToString());
            if (newsType == null)
            {
                return HttpNotFound();
            }
            return View(newsType);
        }

        // GET: NewsTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsTypes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] NewsType newsType)
        {
            if (ModelState.IsValid)
            {
               
                await _newsTypeDal.InsertAsync(newsType);
                return RedirectToAction("Index");
            }

            return View(newsType);
        }

        // GET: NewsTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsType newsType = await _newsTypeDal.QueryByIdAsync(id.ToString());
            if (newsType == null)
            {
                return HttpNotFound();
            }
            return View(newsType);
        }

        // POST: NewsTypes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] NewsType newsType)
        {
            if (ModelState.IsValid)
            {

                await _newsTypeDal.ModifyAsync(newsType);
                return RedirectToAction("Index");
            }
            return View(newsType);
        }

        // GET: NewsTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var  result = await _newsTypeDal.DeleteByIdAsync(id.ToString());
            if (result == 0)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        // POST: NewsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            await _newsTypeDal.DeleteByIdAsync(id.ToString());
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
