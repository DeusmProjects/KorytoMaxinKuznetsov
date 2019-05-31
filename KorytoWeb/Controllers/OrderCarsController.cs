using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KorytoModel;
using KorytoServiceImplementDataBase;

namespace KorytoWeb.Controllers
{
    public class OrderCarsController : Controller
    {
        private KorytoDbContext db = new KorytoDbContext();

        // GET: OrderCars
        public ActionResult Index()
        {
            var orderCars = db.OrderCars.Include(o => o.Car).Include(o => o.Order);
            return View(orderCars.ToList());
        }

        // GET: OrderCars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderCar orderCar = db.OrderCars.Find(id);
            if (orderCar == null)
            {
                return HttpNotFound();
            }
            return View(orderCar);
        }

        // GET: OrderCars/Create
        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "Id", "CarName");
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id");
            return View();
        }

        // POST: OrderCars/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarId,OrderId,Amount")] OrderCar orderCar)
        {
            if (ModelState.IsValid)
            {
                db.OrderCars.Add(orderCar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "Id", "CarName", orderCar.CarId);
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", orderCar.OrderId);
            return View(orderCar);
        }

        // GET: OrderCars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderCar orderCar = db.OrderCars.Find(id);
            if (orderCar == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "CarName", orderCar.CarId);
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", orderCar.OrderId);
            return View(orderCar);
        }

        // POST: OrderCars/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarId,OrderId,Amount")] OrderCar orderCar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderCar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "CarName", orderCar.CarId);
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", orderCar.OrderId);
            return View(orderCar);
        }

        // GET: OrderCars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderCar orderCar = db.OrderCars.Find(id);
            if (orderCar == null)
            {
                return HttpNotFound();
            }
            return View(orderCar);
        }

        // POST: OrderCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderCar orderCar = db.OrderCars.Find(id);
            db.OrderCars.Remove(orderCar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
