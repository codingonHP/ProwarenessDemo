using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProawarenessMeetupDemos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TrackingReaders()
        {
            return View();
        }

        public ActionResult GeoLocation()
        {
            return View();
        }

        public ActionResult ChatApp()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DrawingBoard()
        {
            return View();
        }
    }
}