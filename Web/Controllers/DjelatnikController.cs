using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Models.BirmiStorage;

namespace Web.Controllers
{
    [Authorize(Roles = "admin, supervisor")]
    public class DjelatnikController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public async Task<ActionResult> Index()
        {
            return View(await db.Djelatnik
                .Include(x => x.Roles)
                .ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
