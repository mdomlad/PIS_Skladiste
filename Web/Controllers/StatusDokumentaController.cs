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

namespace Web.Controllers
{
    [Authorize(Roles = "admin, supervisor")]
    public class StatusDokumentaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StatusDokumenta
        public async Task<ActionResult> Index()
        {
            return View(await db.StatusDokumenta.ToListAsync());
        }

        // GET: StatusDokumenta/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusDokumenta statusDokumenta = await db.StatusDokumenta.FindAsync(id);
            if (statusDokumenta == null)
            {
                return HttpNotFound();
            }
            return View(statusDokumenta);
        }

        // GET: StatusDokumenta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusDokumenta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Naziv")] StatusDokumenta statusDokumenta)
        {
            if (ModelState.IsValid)
            {
                db.StatusDokumenta.Add(statusDokumenta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(statusDokumenta);
        }

        // GET: StatusDokumenta/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusDokumenta statusDokumenta = await db.StatusDokumenta.FindAsync(id);
            if (statusDokumenta == null)
            {
                return HttpNotFound();
            }
            return View(statusDokumenta);
        }

        // POST: StatusDokumenta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Naziv")] StatusDokumenta statusDokumenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusDokumenta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(statusDokumenta);
        }

        // GET: StatusDokumenta/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusDokumenta statusDokumenta = await db.StatusDokumenta.FindAsync(id);
            if (statusDokumenta == null)
            {
                return HttpNotFound();
            }
            return View(statusDokumenta);
        }

        // POST: StatusDokumenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StatusDokumenta statusDokumenta = await db.StatusDokumenta.FindAsync(id);
            db.StatusDokumenta.Remove(statusDokumenta);
            await db.SaveChangesAsync();
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
