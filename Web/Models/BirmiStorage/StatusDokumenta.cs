using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models.BirmiStorage
{
    public class StatusDokumenta
    {
        public const string INIT = "INIT";
        public const string PENDING = "PENDING";
        public const string CLOSED = "CLOSED";
        public const string CANCELED = "CANCELED";

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Status")]
        public int ID { get; set; }

        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.", MinimumLength = 1)]
        [Display(Name = "Status")]
        public string Naziv { get; set; }

        [StringLength(50)]
        public string Label { get; set; }
    }
}