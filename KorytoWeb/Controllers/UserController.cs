using KorytoServiceDAL.Interfaces;
using System.Web.Mvc;

namespace KorytoWeb.Controllers
{
    public class UserController : Controller
    {
        readonly IStatisticService service = Globals.StatisticService;
        // GET: User
        public ActionResult Index()
        {
            ViewBag.User = Globals.AuthClient;
            ViewBag.Service = service;
            return View();
        }

        public ActionResult SaveDataBaseClient()
        {
            Globals.MainService.SaveDataBaseClient();
            return RedirectToAction("Index");
        }
    }
}