using MvcRazorToPdf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "admin, supervisor")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> ProizvodReport()
        {
            return new PdfActionResult(await db.Proizvod
                .Include(x => x.SkladisteLokacija.JedinicaMjere)
                .ToListAsync());
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> PrimkeReport()
        {
            var primka = db.Primka
                .Include(p => p.Djelatnik)
                .Include(p => p.Proizvodi)
                .Include(p => p.Status);
            return new PdfActionResult(await primka.ToListAsync());
        }

        [Authorize(Roles = "admin, supervisor, worker")]
        public ActionResult PrimkaReport(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new PdfActionResult(new PrimkaViewModel(id.Value));
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> IzdatniceReport()
        {
            var izdatnica = db.Izdatnica
                .Include(p => p.Djelatnik)
                .Include(p => p.Proizvodi)
                .Include(p => p.Status);
            return new PdfActionResult(await izdatnica.ToListAsync());
        }

        [Authorize(Roles = "admin, supervisor, worker")]
        public ActionResult IzdatnicaReport(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new PdfActionResult(new IzdatnicaViewModel(id.Value));
        }
    }
}