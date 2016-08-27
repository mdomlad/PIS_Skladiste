using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Models.BirmiStorage;

namespace Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
           : base("LocalConnection", throwIfV1Schema: false)
        {

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Izdatnica> Izdatnica { get; set; }
        public DbSet<Primka> Primka { get; set; }
        public DbSet<PlanSkladistenja> PlanSkladistenja { get; set; }
        public DbSet<SkladisteLokacija> SkladisteLokacija { get; set; }
        public DbSet<StavkaPrimke> StavkaPrimke { get; set; }
        public DbSet<StavkaIzdatnice> StavkaIzdatnice { get; set; }
        public DbSet<StatusDokumenta> StatusDokumenta { get; set; }
        public DbSet<Djelatnik> Djelatnik { get; set; }
        public DbSet<JedinicaMjere> JediniceMjere { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here

            base.OnModelCreating(modelBuilder);
        }
    }
}