using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Models.BirmiStorage;

namespace Web.ViewModels
{
    public class BaseViewModel
    {
        public SelectList Djelatnici { get; set; }
        public SelectList Proizvodi { get; set; }
        public SelectList JediniceMjere { get; set; }
        public SelectList StatusiDokumenta { get; set; }

        public List<StatusDokumenta> StatusiDokumentaList { get; set; }

        protected ApplicationDbContext db = ApplicationDbContext.Create();

        public BaseViewModel()
        {
            StatusiDokumentaList = db.StatusDokumenta.OrderBy(x => x.ID).ToList();

            Djelatnici = new SelectList(db.Djelatnik.ToList(), "ID", "Fullname");
            Proizvodi = new SelectList(db.Proizvod.ToList(), "ID", "NazivStanje");
            JediniceMjere = new SelectList(db.JediniceMjere.ToList(), "ID", "Naziv");
            StatusiDokumenta = new SelectList(StatusiDokumentaList.OrderBy(x => x.Naziv), "ID", "Naziv");
        }
    }
}