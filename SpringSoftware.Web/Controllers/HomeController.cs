using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpringSoftware.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "关于我们";
            return View();
        }

        public ActionResult WebSiteProductIntro()
        {
            ViewBag.Title = "企业简介.";
            return View();
        }

        public ActionResult SoftwareProductIntro()
        {
            ViewBag.Title = "企业简介.";
            return View();
        }

        public ActionResult TrainProductIntro()
        {
            ViewBag.Title = "企业简介.";
            return View();
        }

        public ActionResult CompanyIntro()
        {
            ViewBag.Title = "企业简介.";
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "联系我们.";
            return View();
        }
    }
}