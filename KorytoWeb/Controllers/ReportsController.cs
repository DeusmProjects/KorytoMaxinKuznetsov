using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceImplementDataBase.Implementations;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

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
        public ActionResult CreateReport(DateTime dateFrom, DateTime dateTo)
        {

            Globals.ModelReport = new ClientOrders
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                Orders = Service.GetClientOrders(Globals.AuthClient.Id)
            };

            return RedirectToAction("ClientOrders");
        }

        [HttpPost]
        public ActionResult SaveClientOrders()
        {
            var fileName = "D:\\reports\\orders.pdf";

            reportService.SaveClientOrders(new ReportBindingModel
            {
                DateFrom = Globals.ModelReport.DateFrom,
                DateTo = Globals.ModelReport.DateTo,
                FileName = fileName
            }, 
                Globals.AuthClient.Id);

            MailService.SendEmail(Globals.AuthClient.Mail, "Отчет по заказам за период", null, new List<string> { fileName });

            return RedirectToAction("Index");
        }
    }
}