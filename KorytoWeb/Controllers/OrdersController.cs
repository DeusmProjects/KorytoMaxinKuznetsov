using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System.Web.Mvc;

namespace KorytoWeb.Controllers
{
    public class OrdersController : Controller
    {
        public IMainService Service = Globals.MainService;
        // GET: Orders
        public ActionResult Index()
        {
            return View(Service.GetClientOrders(Globals.AuthClient.Id));
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            var order = Service.GetElement(id);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        public ActionResult Pay(int id)
        {
            Service.PayOrder(new OrderBindingModel {ClientId = Globals.AuthClient.Id, Id = id});
            return View();
        }
    }
}
