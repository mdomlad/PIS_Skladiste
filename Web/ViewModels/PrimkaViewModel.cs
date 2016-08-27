using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.BirmiStorage;

namespace Web.ViewModels
{
    public class PrimkaViewModel: BaseViewModel
    {
        public const string _requiredMsg = "Polje je obavezno!";

        public bool IsClosed { get; set; }

        public bool IsCanceled { get; set; }

        public Primka Primka { get; set; }

        public StavkaPrimke StavkaPrimke { get; set; }       

        public PrimkaViewModel()
        {
        }

        public PrimkaViewModel(int primkaId)
        {
            Primka = db.Primka
                .Include(x => x.StavkePrimke)
                .Include(z => z.Djelatnik)
                .Include(w => w.Status)
                .Single(y => y.ID == primkaId);

            switch (Primka.Status.Label)
            {
                case StatusDokumenta.CANCELED:
                    IsCanceled = true;
                    break;
                case StatusDokumenta.CLOSED:
                    IsClosed = true;
                    break;
            }
        }
    }
}