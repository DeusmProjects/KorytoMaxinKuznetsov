using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using KorytoServiceImplementDataBase;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace KorytoWeb.Controllers
{
    public class OrdersController : Controller
    {
        public IMainService service = Globals.MainService;
        // GET: Orders
        public ActionResult Index()
        {
            return View(service.GetClientOrders(Globals.AuthClient.Id));
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            OrderViewModel order = service.GetElement(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        public ActionResult Pay(int id)
        {
            service.PayOrder(new OrderBindingModel
            {
                ClientId = Globals.AuthClient.Id,
                Id = id
            });
            return View();
        }
    }
}
