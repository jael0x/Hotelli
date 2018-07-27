using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebHotelli.Models;

namespace WebHotelli.Controllers
{
    public class ReservacionController : Controller
    {
        private Entities db = new Entities();

        // GET: Reservacion
        public ActionResult Index()
        {
            var reservacion = db.Reservacion.Include(r => r.Habitacion).Include(r => r.Usuario);
            return View(reservacion.ToList());
        }

        // GET: Reservacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // GET: Reservacion/Create
        public ActionResult Create()
        {
            ViewBag.habitacion_id = new SelectList(db.Habitacion, "habitacion_id", "numeracion");
            ViewBag.usuario_id = new SelectList(db.Usuario, "usuario_id", "nombre");
            return View();
        }

        // POST: Reservacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reservacion_id,usuario_id,habitacion_id,fecha_entrada,fecha_salida,num_usuarios,estado,costo")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Reservacion.Add(reservacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.habitacion_id = new SelectList(db.Habitacion, "habitacion_id", "numeracion", reservacion.habitacion_id);
            ViewBag.usuario_id = new SelectList(db.Usuario, "usuario_id", "nombre", reservacion.usuario_id);
            return View(reservacion);
        }

        // GET: Reservacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.habitacion_id = new SelectList(db.Habitacion, "habitacion_id", "numeracion", reservacion.habitacion_id);
            ViewBag.usuario_id = new SelectList(db.Usuario, "usuario_id", "nombre", reservacion.usuario_id);
            return View(reservacion);
        }

        // POST: Reservacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reservacion_id,usuario_id,habitacion_id,fecha_entrada,fecha_salida,num_usuarios,estado,costo")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.habitacion_id = new SelectList(db.Habitacion, "habitacion_id", "numeracion", reservacion.habitacion_id);
            ViewBag.usuario_id = new SelectList(db.Usuario, "usuario_id", "nombre", reservacion.usuario_id);
            return View(reservacion);
        }

        // GET: Reservacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // POST: Reservacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacion reservacion = db.Reservacion.Find(id);
            db.Reservacion.Remove(reservacion);
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
