using Microsoft.AspNet.SignalR;
using ProawarenessMeetupDemos.Business;
using ProawarenessMeetupDemos.Hubs;
using ProawarenessMeetupDemos.Models;
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

        public ActionResult LongWorkReporting()
        {
            return View();
        }

        public ActionResult BookDashboard()
        {
            BookAuthorBusiness bookAuthorBusiness = new BookAuthorBusiness();
            var allRecords = bookAuthorBusiness.GetAllBookAuthorModel();

            return View(allRecords);
        }

        [HttpPost]
        public JsonResult UploadBookAuthorData(BookAuthorModel bookAuthorVm)
        {
            BookAuthorBusiness bookAuthorBusiness = new BookAuthorBusiness();
            bookAuthorBusiness.SaveBookAuthorData(bookAuthorVm);

            var bookAuthorHubContext = GlobalHost.ConnectionManager.GetHubContext<BookAuthorHub>();
            bookAuthorHubContext.Clients.All.UpdateClientWithFreshData(bookAuthorVm);

            return Json("done");
        }
    }
}