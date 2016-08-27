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
    public class IzdatnicaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public async Task<ActionResult> Index()
        {
            var primka = db.Izdatnica
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

            return View(new IzdatnicaViewModel(id.Value));
        }
        
        public ActionResult Create()
        {
            return View(new IzdatnicaViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IzdatnicaViewModel izdatnicaVM)
        {
            if (ModelState.IsValid)
            {
                izdatnicaVM.Izdatnica.StatusID = izdatnicaVM.StatusiDokumentaList.OrderBy(x => x.ID).First().ID;
                db.Izdatnica.Add(izdatnicaVM.Izdatnica);
                await db.SaveChangesAsync();
                this.AddNotification("Izdatnica je kreirana. Molimo unesite stavke.", NotificationType.INFO);
                return RedirectToAction("AddStavkaIzdatnice", new { izdatnicaId = izdatnicaVM.Izdatnica.ID });
            }

            return View(new IzdatnicaViewModel());
        }
        
        public ActionResult AddStavkaIzdatnice(int? izdatnicaId)
        {
            if (!izdatnicaId.HasValue) return HttpNotFound();

            var izdatnicaVM = new IzdatnicaViewModel(izdatnicaId.Value);

            if (izdatnicaVM.IsClosed || izdatnicaVM.IsCanceled)
            {
                this.AddNotification("Izdatnica je završena. Nije moguća izmjena.", NotificationType.WARNING);
                return RedirectToAction("Index");
            }

            return View(izdatnicaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddStavkaIzdatnice(IzdatnicaViewModel izdatnicaVM)
        {
            if (ModelState.IsValid)
            {
                var stavkaIzdatnice = db.StavkaIzdatnice.Find(izdatnicaVM.Izdatnica.ID, izdatnicaVM.StavkaIzdatnice.ProizvodID);
                var skladiste = await db.SkladisteLokacija.FindAsync(izdatnicaVM.StavkaIzdatnice.ProizvodID);

                if (stavkaIzdatnice != null)
                {
                    var diff = izdatnicaVM.StavkaIzdatnice.Kolicina - stavkaIzdatnice.Kolicina;
                    stavkaIzdatnice.Kolicina = izdatnicaVM.StavkaIzdatnice.Kolicina;
                    skladiste.Stanje -= diff;
                    db.Entry(stavkaIzdatnice).State = EntityState.Modified;
                }
                else {
                    izdatnicaVM.StavkaIzdatnice.IzdatnicaID = izdatnicaVM.Izdatnica.ID;
                    db.StavkaIzdatnice.Add(izdatnicaVM.StavkaIzdatnice);
                    skladiste.Stanje -= izdatnicaVM.StavkaIzdatnice.Kolicina;
                }
                db.Entry(skladiste).State = EntityState.Modified;
                await db.SaveChangesAsync();
                this.AddNotification("Stavka je uspješno dodana", NotificationType.SUCCESS);
            }

            return View(new IzdatnicaViewModel(izdatnicaVM.Izdatnica.ID));
        }

        public async Task<ActionResult> DeleteStavkaIzdatnice(int? IzdatnicaID, int? ProizvodID, string ViewName = "StavkaIzdatnice")
        {
            if (IzdatnicaID.HasValue && ProizvodID.HasValue)
            {
                var izdatnicaVM = new IzdatnicaViewModel(IzdatnicaID.Value);
                if (izdatnicaVM.IsClosed || izdatnicaVM.IsCanceled)
                {
                    this.AddNotification("Izdatnica je završena. Nije moguća izmjena.", NotificationType.WARNING);
                    return RedirectToAction("Index");
                }
                var existing = db.StavkaIzdatnice.Find(IzdatnicaID.Value, ProizvodID.Value);
                var skladiste = await db.SkladisteLokacija.FindAsync(ProizvodID.Value);
                if (existing != null)
                {
                    skladiste.Stanje += existing.Kolicina;
                    db.StavkaIzdatnice.Remove(existing);
                    db.Entry(skladiste).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                this.AddNotification("Stavka je uspješno obrisana", NotificationType.SUCCESS);
                return View(ViewName, new IzdatnicaViewModel(IzdatnicaID.Value));
            }

            return new HttpNotFoundResult();
        }
        
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = new IzdatnicaViewModel(id.Value);
            if (viewModel.IsClosed || viewModel.IsCanceled)
            {
                this.AddNotification("Izdatnica je završena. Nije moguća izmjena.", NotificationType.WARNING);
                return RedirectToAction("Index");
            }
            ViewBag.DjelatnikID = new SelectList(db.Users, "Id", "Email", viewModel.Izdatnica.DjelatnikID);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(IzdatnicaViewModel izdatnicaVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(izdatnicaVM.Izdatnica).State = EntityState.Modified;
                await db.SaveChangesAsync();
                var msg = string.Format("Izdatnica broj {0} je uspješno uređena", izdatnicaVM.Izdatnica.ID);
                this.AddNotification(msg, NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            ViewBag.DjelatnikID = new SelectList(db.Users, "Id", "Email", izdatnicaVM.Izdatnica.DjelatnikID);
            return View(new IzdatnicaViewModel(izdatnicaVM.Izdatnica.ID));
        }
        public async Task<ActionResult> PromijeniStatus(int? izdatnicaId, string nextStatusLbl)
        {
            if (izdatnicaId.HasValue && !string.IsNullOrWhiteSpace(nextStatusLbl))
            {
                var izdatnica = await db.Izdatnica.FindAsync(izdatnicaId);
                var status = await db.StatusDokumenta.SingleAsync(x => x.Label == nextStatusLbl);
                if (izdatnica != null && status != null)
                {
                    izdatnica.StatusID = status.ID;
                    db.Entry(izdatnica).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    var msg = string.Format("Izdatnica broj {0} je uspješno prebačena u status {1}", izdatnica.ID, status.Naziv);
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
