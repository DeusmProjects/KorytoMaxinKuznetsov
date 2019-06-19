using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;

namespace KorytoWeb.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService reportService = Globals.ReportService;
        public IMainService Service = Globals.MainService;

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClientOrders()
        {
            return View(Globals.ModelReport);
        }

        [HttpPost]
        public ActionResult CreateReport()
        {
            Globals.ModelReport = Service.GetClientOrders(Globals.AuthClient.Id);
            return RedirectToAction("ClientOrders");
        }

        [HttpPost]
        public ActionResult SaveClientOrders()
        {
            reportService.SaveClientOrders(new ReportBindingModel
            {
                DateTo = DateTime.Now,
                DateFrom = new DateTime(2019, 6, 10),
                FileName = "D:\\client_orders.pdf"
            }, 
                Globals.AuthClient.Id);

            return RedirectToAction("Index");
        }
    }
}