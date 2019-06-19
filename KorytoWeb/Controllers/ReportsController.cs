using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using System;
using System.Linq;
using System.Web.Mvc;
using KorytoServiceImplementDataBase.Implementations;

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
        public ActionResult CreateReport([Bind(Include = "DateFrom, DateTo")] ReportBindingModel report)
        {

            Globals.ModelReport = new ClientOrders
            {
                DateFrom = report.DateFrom,
                DateTo = report.DateTo,
                Orders = Service.GetClientOrders(Globals.AuthClient.Id)
            };

            return RedirectToAction("ClientOrders");
        }

        [HttpPost]
        public ActionResult SaveClientOrders()
        {
            var fileName = "D:\\client_orders.pdf";

            reportService.SaveClientOrders(new ReportBindingModel
            {
                DateTo = DateTime.Now,
                DateFrom = new DateTime(2019, 6, 10),
                FileName = fileName
            }, 
                Globals.AuthClient.Id);

            MailService.SendEmail(Globals.AuthClient.Mail, "Отчет по заказам за период", null, fileName);

            return RedirectToAction("Index");
        }
    }
}