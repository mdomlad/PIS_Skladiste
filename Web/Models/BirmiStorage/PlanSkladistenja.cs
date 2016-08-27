using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models.BirmiStorage
{
    public class PlanSkladistenja
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime DatumOd { get; set; }

        public DateTime? DatumDo { get; set; }

        public int SkladisteLokacijaID { get; set; }

        [ForeignKey("SkladisteLokacijaID")]
        public SkladisteLokacija SkladisteLokacija { get; set; }
    }
}