using KorytoServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KorytoWeb.Controllers
{
    public class UserController : Controller
    {
        IStatistic service = Globals.StatisticService;
        // GET: User
        public ActionResult Index()
        {
            ViewBag.User = Globals.AuthClient;
            ViewBag.Service = service;
            return View();
        }
    }
}