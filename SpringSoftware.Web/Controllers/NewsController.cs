using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;
using SpringSoftware.Web.Models;
using PagedList;

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
        public async Task<ActionResult> CompanyIndex(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            IEnumerable<News> entityList = await _newsDal.QueryByFunAsync(t => t.NewsType.Id == 1);
            if (entityList.Any())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    entityList = entityList.Where(s => s.Content.Contains(searchString)
                                                       || s.Title.Contains(searchString)
                                                       || s.Creater.Contains(searchString)
                                                       || s.LastModifier.Contains(searchString));
                }
                    entityList = entityList.OrderByDescending(s => s.LastModifyDate);
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(entityList.ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> IndustryIndex(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            IEnumerable<News> entityList = await _newsDal.QueryByFunAsync(t=>t.NewsType.Id==2);
            if (entityList.Any())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    entityList = entityList.Where(s => s.Content.Contains(searchString)
                                                       || s.Title.Contains(searchString)
                                                       || s.Creater.Contains(searchString)
                                                       || s.LastModifier.Contains(searchString));
                }
                entityList = entityList.OrderByDescending(s => s.LastModifyDate);
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(entityList.ToPagedList(pageNumber, pageSize));
        }

        // GET: /News/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = await _newsDal.QueryByIdAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
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
