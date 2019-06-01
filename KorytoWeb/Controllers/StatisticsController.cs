using KorytoServiceDAL.Interfaces;
using System.Web.Mvc;

namespace KorytoWeb.Controllers
{
    public class StatisticsController : Controller
    {
        readonly IStatisticService service = Globals.StatisticService;
        // GET: Statisics
        public ActionResult Index()
        {
            ViewBag.Service = service;
            return View();
        }
    }
}