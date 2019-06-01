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

        IStatisticService service = Globals.StatisticService;
        // GET: Statisics
        public ActionResult Index()
        {
            ViewBag.Service = service;
            return View();
        }
    }
}