﻿using System;
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
            ViewBag.Message = "关于我们";
            return View();
        }

        public ActionResult WebSiteProductIntro()
        {
            ViewBag.Message = "企业简介.";
            return View();
        }

        public ActionResult SoftwareProductIntro()
        {
            ViewBag.Message = "企业简介.";
            return View();
        }

        public ActionResult TrainProductIntro()
        {
            ViewBag.Message = "企业简介.";
            return View();
        }

        public ActionResult CompanyIntro()
        {
            ViewBag.Message = "企业简介.";
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}