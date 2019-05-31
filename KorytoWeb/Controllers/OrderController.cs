using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using KorytoServiceImplementDataBase;

namespace KorytoWeb.Controllers
{
    public class OrderController : Controller
    {
        private IMainService service = Globals.MainService;
        private ICarService carService = Globals.CarService;

        // GET: Vouchers
        public ActionResult Index()
        {
            if (Session["Order"] == null)
            {
                var order = new OrderViewModel();
                order.OrderCars = new List<OrderCarViewModel>();
                Session["Order"] = order;
            }
            return View((OrderViewModel)Session["Order"]);
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
    }
}
