using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace KorytoWeb.Controllers
{
    public class ClientsController : Controller
    {
        public IClientService service = Globals.ClientService;

        // GET: Clients
        public ActionResult Index()
        {
            return View(service.GetList());
        }

        public ActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Auth([Bind(Include = "Login, Password")] Client client)
        {
            if (service.GetList().Any(rec => rec.Login == client.Login && rec.Password == client.Password))
            {
                RedirectToAction("Index", "Orders");
            }

            return View(client);
        }

        // GET: Clients/Details/5
        public ActionResult Details(int id)
        {
            var client = service.GetElement(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientFIO,Login,Password,Mail")] Client client)
        {
            if (ModelState.IsValid)
            {
                var clientDB = service.GetList().FirstOrDefault(rec =>
                        rec.ClientFIO == client.ClientFIO ||
                        rec.Login == client.Login ||
                        rec.Mail == client.Mail);

                if (clientDB == null)
                {
                    service.AddElement(new ClientBindingModel
                    {
                        Id = client.Id,
                        ClientFIO = client.ClientFIO,
                        Login = client.Login,
                        Password = client.Password,
                        Mail = client.Mail
                    });
                    return RedirectToAction("Auth", "Clients");
                }
            }

            return View(client);
        }
    }
}
