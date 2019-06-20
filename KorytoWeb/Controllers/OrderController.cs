using System;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KorytoWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMainService service = Globals.MainService;
        private readonly ICarService carService = Globals.CarService;
        private readonly IStatisticService statistic = Globals.StatisticService;

        // GET: Vouchers
        public ActionResult Index()
        {
            if (Session["Order"] == null)
            {
                var order = new OrderViewModel {OrderCars = new List<OrderCarViewModel>()};
                Session["Order"] = order;
            }
            ViewBag.Service = statistic;
            return View((OrderViewModel)Session["Order"]);
        }

        public ActionResult Reserve()
        {
            return View();
        }

        public ActionResult AddCar()
        {
            var cars = new SelectList(carService.GetList(), "Id", "CarName");
            ViewBag.Cars = cars;
            return View();
        }

        [HttpPost]
        public ActionResult AddCarPost()
        {
            var order = (OrderViewModel)Session["Order"];
            var car = new OrderCarViewModel
            {
                CarId = int.Parse(Request["Id"]),
                CarName = carService.GetElement(int.Parse(Request["Id"])).CarName,
                Amount = int.Parse(Request["Amount"])
            };
            order.OrderCars.Add(car);
            Session["Order"] = order;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateOrderPost()
        {
            var order = (OrderViewModel)Session["Order"];
            var orderCars = new List<OrderCarBindingModel>();
            for (int i = 0; i < order.OrderCars.Count; ++i)
            {
                orderCars.Add(new OrderCarBindingModel
                {
                    Id = order.OrderCars[i].Id,
                    OrderId = order.OrderCars[i].OrderId,
                    CarId = order.OrderCars[i].CarId,
                    Amount = order.OrderCars[i].Amount
                });
            }

            service.CreateOrder(new OrderBindingModel
            {
                ClientId = Globals.AuthClient.Id,
                TotalSum = orderCars.Sum(rec => rec.Amount * carService.GetElement(rec.CarId).Price),
                OrderCars = orderCars
            });
            Session.Remove("Order");
            return RedirectToAction("Index", "Orders");
        }

        [HttpPost]
        public ActionResult ReservePost()
        {
            var order = (OrderViewModel)Session["Order"];
            order.DateCreate = DateTime.Now.ToShortDateString();
            order.Id = Globals.DbContext.Orders.Max(rec => rec.Id) + 1;
            order.ClientId = Globals.AuthClient.Id;
            order.ClientFIO = Globals.AuthClient.ClientFIO;

            var orderCars = new List<OrderCarBindingModel>();
            for (int i = 0; i < order.OrderCars.Count; ++i)
            {
                orderCars.Add(new OrderCarBindingModel
                {
                    Id = order.OrderCars[i].Id,
                    OrderId = order.OrderCars[i].OrderId,
                    CarId = order.OrderCars[i].CarId,
                    Amount = order.OrderCars[i].Amount
                });
            }

            service.ReserveOrder(new OrderBindingModel
            {
                ClientId = Globals.AuthClient.Id,
                TotalSum = orderCars.Sum(rec => rec.Amount * carService.GetElement(rec.CarId).Price),
                OrderCars = orderCars
            });

            order.TotalSum = orderCars.Sum(rec => rec.Amount * carService.GetElement(rec.CarId).Price);

            Globals.ReportService.SaveClientReserveWord(order, "D:\\reserve.doc");

            Session.Remove("Order");
            return RedirectToAction("Index", "Orders");
        }
    }
}
