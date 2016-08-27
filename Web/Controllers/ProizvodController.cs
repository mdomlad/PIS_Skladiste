using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Models.BirmiStorage;
using Web.ViewModels;
using Web.Extensions;

namespace Web.Controllers
{
    [Authorize(Roles = "admin, worker, supervisor")]
    public class ProizvodController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.Proizvod.ToListAsync());
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = await db.Proizvod
                .Include(x => x.SkladisteLokacija.JedinicaMjere)
                .SingleAsync(y => y.ID == id.Value);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }
        
        public ActionResult Create()
        {
            ViewBag.JediniceMjere = new SelectList(db.JediniceMjere.ToList(), "ID", "Naziv");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Proizvod.Add(proizvod);
                await db.SaveChangesAsync();
                this.AddNotification("Proizvod je uspješno dodan", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }

            ViewBag.JediniceMjere = new SelectList(db.JediniceMjere.ToList(), "ID", "Naziv");
            return View(proizvod);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = await db.Proizvod.FindAsync(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            ViewBag.JediniceMjere = new SelectList(db.JediniceMjere.ToList(), "ID", "Naziv");
            return View(proizvod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Naziv,Opis,Cijena")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proizvod).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.JediniceMjere = new SelectList(db.JediniceMjere.ToList(), "ID", "Naziv");
            return View(proizvod);
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
