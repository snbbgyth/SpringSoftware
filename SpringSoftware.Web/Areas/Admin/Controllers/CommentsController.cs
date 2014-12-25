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
using SpringSoftware.Web.Models;
using PagedList;

namespace SpringSoftware.Web.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        private ICommentDal _commentDal;

        public CommentsController()
        {
            _commentDal = DependencyResolver.Current.GetService<ICommentDal>();
        }

        // GET: Comments
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IEnumerable<Comment> entityList = await _commentDal.QueryAllAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                entityList = entityList.Where(s => s.UserName.Contains(searchString)
                                       || s.Content.Contains(searchString)
                                       ||s.Phone.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    entityList = entityList.OrderByDescending(s => s.UserName);
                    break;
                case "Date":
                    entityList = entityList.OrderBy(s => s.LastModifyDate);
                    break;
                case "date_desc":
                    entityList = entityList.OrderByDescending(s => s.LastModifyDate);
                    break;
                default:  // Name ascending 
                    entityList = entityList.OrderBy(s => s.UserName);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(entityList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Comments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await _commentDal.QueryByIdAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        private void InitAuthor(Comment comment)
        {
            if (User.Identity.IsAuthenticated)
            {
                //comment.Creater = User.Identity.Name;
                comment.LastModifier = User.Identity.Name;
            }
        }

        // GET: Comments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await _commentDal.QueryByIdAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Phone,UserName,Content,CreateDate,LastModifyDate,IsDelete,Creater,LastModifier")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                InitAuthor(comment);
                await _commentDal.ModifyAsync(comment);
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await _commentDal.QueryByIdAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            await _commentDal.DeleteByIdAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //_commentDal.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
