using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models.BirmiStorage
{
    public class JedinicaMjere
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Jed. Mj.")]
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Jed. Mj.")]
        public string Naziv { get; set; }
    }
}