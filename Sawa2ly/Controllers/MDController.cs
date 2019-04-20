using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.Models;

namespace Sawa2ly.Controllers
{
    public class MDController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MD
        public ActionResult Index()
        {
            var project = db.Project.ToList();

            return View(project);

        }
    }
}