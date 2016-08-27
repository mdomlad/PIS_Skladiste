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
    public class IzdatnicaViewModel : BaseViewModel
    {
        public const string _requiredMsg = "Polje je obavezno!";

        public bool IsClosed { get; set; }

        public bool IsCanceled { get; set; }

        public Izdatnica Izdatnica { get; set; }

        public StavkaIzdatnice StavkaIzdatnice { get; set; }       

        public IzdatnicaViewModel()
        {
        }

        public IzdatnicaViewModel(int izdatnicaId)
        {
            Izdatnica = db.Izdatnica
                .Include(x => x.StavkeIzdatnice)
                .Include(z => z.Djelatnik)
                .Include(w => w.Status)
                .Single(y => y.ID == izdatnicaId);

            switch (Izdatnica.Status.Label)
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