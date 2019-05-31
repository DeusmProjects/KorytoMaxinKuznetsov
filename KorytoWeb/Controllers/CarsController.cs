using KorytoServiceDAL.Interfaces;
using System.Web.Mvc;

namespace KorytoWeb.Controllers
{
    public class CarsController : Controller
    {
        public ICarService service = Globals.CarService;

        // GET: Cars
        public ActionResult Index()
        {
            return View(service.GetList());
        }

        public ActionResult Filter()
        {
            return View(service.GetFilteredList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int id)
        {
            var car = service.GetElement(id);

            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }
    }
}
