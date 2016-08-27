using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.BirmiStorage
{
    public class Djelatnik: ApplicationUser
    {
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.", MinimumLength = 1)]
        public string Ime { get; set; }

        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string Prezime { get; set; }

        public ICollection<Izdatnica> Izdatnice { get; set; }
        public ICollection<Primka> Primke { get; set; }

        [Display(Name = "Korisnik")]
        public string Fullname => string.Format("{0}, {1}", Prezime, Ime);
    }
}