using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models.BirmiStorage
{
    public class StavkaPrimke
    {
        public const string _requiredMsg = "Polje je obavezno!";

        [Column(Order = 0), Key, ForeignKey("Primka")]
        public int PrimkaID { get; set; }

        [Column(Order = 1), Key, ForeignKey("Proizvod")]
        [DisplayName("Proizvod")]
        [Required(ErrorMessage = _requiredMsg)]
        public int ProizvodID { get; set; }

        [DisplayName("Količina")]
        [Required(ErrorMessage = _requiredMsg)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0,00")]
        public double Kolicina { get; set; }

        public virtual Primka Primka { get; set; }

        public virtual Proizvod Proizvod { get; set; }
    }
}