using KorytoServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KorytoWeb.Controllers
{
    public class StatisticsController : Controller
    {

        IStatistic service = Globals.StatisticService;
        // GET: Statisics
        public ActionResult Index()
        {
            ViewBag.MostPopularCar = service.GetMostPopularCar();
            return View();
        }
    }
}