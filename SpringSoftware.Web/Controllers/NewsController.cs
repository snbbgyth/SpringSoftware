using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;
using SpringSoftware.Web.Models;

namespace SpringSoftware.Web.Controllers
{
    public class NewsController : Controller
    {
        private INewsDal _newsDal;

        public NewsController()
        {
            _newsDal = DependencyResolver.Current.GetService<INewsDal>();
        }
        // GET: /News/
        public ActionResult Index()
        {
            var entityList = _newsDal.QueryAll();
            return View(entityList);
        }

        // GET: /News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = _newsDal.QueryById( id.ToString());
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: /News/Create
        public ActionResult Create()
        {
            var news = new News();
           
            return View(news);
        }

        // POST: /News/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Title,Content,IsPublish,NewsTypeId,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] News news)
        {
            if (ModelState.IsValid)
            {
                _newsDal.Insert(news);
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: /News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsDal.QueryById(id.ToString());
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: /News/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Title,Content,IsPublish,NewsTypeId,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] News news)
        {
            if (ModelState.IsValid)
            {
               _newsDal.Modify(news);
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: /News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsDal.QueryById(id.ToString());
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: /News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _newsDal.DeleteById(id.ToString());
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
