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
    public class PrimkaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            var primka = db.Primka
                .Include(p => p.Djelatnik)
                .Include(p => p.Status);
            return View(await primka.ToListAsync());
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(new PrimkaViewModel(id.Value));
        }

        public ActionResult Create()
        {
            return View(new PrimkaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PrimkaViewModel primkaVM)
        {
            if (ModelState.IsValid)
            {
                primkaVM.Primka.StatusID = primkaVM.StatusiDokumentaList.OrderBy(x => x.ID).First().ID;
                db.Primka.Add(primkaVM.Primka);
                await db.SaveChangesAsync();
                this.AddNotification("Primka je kreirana. Molimo unesite stavke.", NotificationType.INFO);
                return RedirectToAction("AddStavkaPrimke", new { primkaId = primkaVM.Primka.ID });
            }

            return View(new PrimkaViewModel());
        }

        public ActionResult AddStavkaPrimke(int? primkaId)
        {
            if (!primkaId.HasValue) return HttpNotFound();

            var primkaVM = new PrimkaViewModel(primkaId.Value);

            if (primkaVM.IsClosed || primkaVM.IsCanceled)
            {
                this.AddNotification("Primka je završena. Nije moguća izmjena.", NotificationType.WARNING);
                return RedirectToAction("Index");
            }

            return View(primkaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddStavkaPrimke(PrimkaViewModel primkaVM)
        {
            if (ModelState.IsValid)
            {
                var stavkaPrimke = db.StavkaPrimke.Find(primkaVM.Primka.ID, primkaVM.StavkaPrimke.ProizvodID);
                var skladiste = await db.SkladisteLokacija.FindAsync(primkaVM.StavkaPrimke.ProizvodID);

                if (stavkaPrimke != null)
                {
                    skladiste.Stanje += primkaVM.StavkaPrimke.Kolicina;
                    stavkaPrimke.Kolicina += primkaVM.StavkaPrimke.Kolicina;
                    db.Entry(stavkaPrimke).State = EntityState.Modified;
                }
                else {
                    primkaVM.StavkaPrimke.PrimkaID = primkaVM.Primka.ID;
                    db.StavkaPrimke.Add(primkaVM.StavkaPrimke);
                    skladiste.Stanje += primkaVM.StavkaPrimke.Kolicina;
                }
                db.Entry(skladiste).State = EntityState.Modified;

                if (skladiste.Stanje >= 0)
                {
                    await db.SaveChangesAsync();
                    this.AddNotification("Stavka je uspješno dodana", NotificationType.SUCCESS);
                }
                else {
                    this.AddNotification("Stavka nije uspješno dodana. Nema dovoljno na stanju.", NotificationType.SUCCESS);
                }
            }

            return View(new PrimkaViewModel(primkaVM.Primka.ID));
        }

        public async Task<ActionResult> DeleteStavkaPrimke(int? PrimkaID, int? ProizvodID, string ViewName = "StavkaPrimke")
        {
            var primkaVM = new PrimkaViewModel(PrimkaID.Value);
            if (primkaVM.IsClosed || primkaVM.IsCanceled)
            {
                this.AddNotification("Primka je završena. Nije moguća izmjena.", NotificationType.WARNING);
                return RedirectToAction("Index");
            }
            if (PrimkaID.HasValue && ProizvodID.HasValue)
            {
                var existing = await db.StavkaPrimke.FindAsync(PrimkaID.Value, ProizvodID.Value);
                var skladiste = await db.SkladisteLokacija.FindAsync(ProizvodID.Value);
                if (existing != null)
                {
                    skladiste.Stanje -= existing.Kolicina;
                    if (skladiste.Stanje < 0)
                    {
                        this.AddNotification("Stavka nije uspješno obrisana. Stanje ne može otići u minus.", NotificationType.ERROR);
                    }
                    else {
                        db.StavkaPrimke.Remove(existing);
                        db.Entry(skladiste).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        this.AddNotification("Stavka je uspješno obrisana", NotificationType.SUCCESS);
                    }
                }
                return View(ViewName, new PrimkaViewModel(PrimkaID.Value));
            }

            return new HttpNotFoundResult();
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = new PrimkaViewModel(id.Value);
            if (viewModel.IsClosed || viewModel.IsCanceled)
            {
                this.AddNotification("Primka je završena. Nije moguća izmjena.", NotificationType.WARNING);
                return RedirectToAction("Index");
            }
            ViewBag.DjelatnikID = new SelectList(db.Users, "Id", "Email", viewModel.Primka.DjelatnikID);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PrimkaViewModel primkaVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(primkaVM.Primka).State = EntityState.Modified;
                await db.SaveChangesAsync();
                var msg = string.Format("Primka broj {0} je uspješno uređena", primkaVM.Primka.ID);
                this.AddNotification(msg, NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            ViewBag.DjelatnikID = new SelectList(db.Users, "Id", "Email", primkaVM.Primka.DjelatnikID);
            return View(new PrimkaViewModel(primkaVM.Primka.ID));
        }

        public async Task<ActionResult> PromijeniStatus(int? primkaId, string nextStatusLbl)
        {
            if (primkaId.HasValue && !string.IsNullOrWhiteSpace(nextStatusLbl))
            {
                var primka = await db.Primka.FindAsync(primkaId);
                var status = await db.StatusDokumenta.SingleAsync(x => x.Label == nextStatusLbl);
                if (primka != null && status != null)
                {
                    primka.StatusID = status.ID;
                    db.Entry(primka).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    var msg = string.Format("Primka broj {0} je uspješno prebačena u status {1}", primka.ID, status.Naziv);
                    this.AddNotification(msg, NotificationType.SUCCESS);
                }
                return RedirectToAction("Index");
            }
            return new HttpNotFoundResult();
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
