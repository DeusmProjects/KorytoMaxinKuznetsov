using KorytoServiceDAL.Interfaces;
using System.Web.Mvc;

namespace KorytoWeb.Controllers
{
    public class CarsController : Controller
    {
        public ICarService Service = Globals.CarService;

        // GET: Cars
        public ActionResult Index()
        {
            return View(Service.GetList());
        }

        public ActionResult Filter()
        {
            return View(Service.GetFilteredList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int id)
        {
            var car = Service.GetElement(id);

            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }
    }
}
