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
        private Entities1 db = new Entities1();

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
            ViewBag.id_hab = new SelectList(db.Habitacion, "id_hab", "numeracion");
            ViewBag.id_usu = new SelectList(db.Usuario, "id_usu", "nombre");
            return View();
        }

        // POST: Reservacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_res,id_usu,id_hab,fecha_ent,fecha_sal,num_usuarios,estado,costo")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Reservacion.Add(reservacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_hab = new SelectList(db.Habitacion, "id_hab", "numeracion", reservacion.id_hab);
            ViewBag.id_usu = new SelectList(db.Usuario, "id_usu", "nombre", reservacion.id_usu);
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
            ViewBag.id_hab = new SelectList(db.Habitacion, "id_hab", "numeracion", reservacion.id_hab);
            ViewBag.id_usu = new SelectList(db.Usuario, "id_usu", "nombre", reservacion.id_usu);
            return View(reservacion);
        }

        // POST: Reservacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_res,id_usu,id_hab,fecha_ent,fecha_sal,num_usuarios,estado,costo")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_hab = new SelectList(db.Habitacion, "id_hab", "numeracion", reservacion.id_hab);
            ViewBag.id_usu = new SelectList(db.Usuario, "id_usu", "nombre", reservacion.id_usu);
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
