using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models.BirmiStorage
{
    public class SkladisteLokacija
    {

        [Column(Order = 0), Key, ForeignKey("Proizvod")]
        public int ProizvodID { get; set; }

        [Required(ErrorMessage = Proizvod._requiredMsg)]
        [Display(Name = "Min. kol.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0,00}")]
        public double MinimalnaKolicina { get; set; }

        [Required(ErrorMessage = Proizvod._requiredMsg)]
        [Display(Name = "Maks. kol.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0,00}")]
        public double MaksimalnaKolicina { get; set; }

        [Display(Name = "Jed. Mj.")]
        [Required(ErrorMessage = Proizvod._requiredMsg)]
        public int JedinicaMjereID { get; set; }

        [ForeignKey("JedinicaMjereID")]
        public JedinicaMjere JedinicaMjere { get; set; }

        public Proizvod Proizvod { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0,00}")]
        public double Stanje { get; set; }
    }
}