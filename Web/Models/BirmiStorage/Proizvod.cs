using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models.BirmiStorage
{
    public class Proizvod
    {
        public const string _requiredMsg = "Polje je obavezno!";

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = _requiredMsg)]
        public string Naziv { get; set; }

        [DataType(DataType.MultilineText)]
        public string Opis { get; set; }

        [Required(ErrorMessage = _requiredMsg)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0,00}")]
        public double Cijena { get; set; }

        public virtual SkladisteLokacija SkladisteLokacija { get; set; }

        public string NazivStanje => string.Format("{0} (stanje: {1})", Naziv, SkladisteLokacija.Stanje);
    }
}