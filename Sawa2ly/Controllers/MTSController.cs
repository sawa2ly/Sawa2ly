using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.Models;
using Sawa2ly.ViewModel;
using System.Data.Entity;

namespace Sawa2ly.Controllers
{
    public class MTSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MTS
        public ActionResult Index()
        {
            var Trainees = db.ProjectTrainees.Include(p => p.Project).ToList();

            return View(Trainees);
        }
    }
}