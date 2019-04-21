using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.Models;
using System.Data.Entity;

namespace Sawa2ly.Controllers
{
    public class ProjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Project/1
        public ActionResult Index(int? id)
        {
            if (Request.IsAuthenticated) {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Project project = db.Project.Include(c => c.Customer).SingleOrDefault(p => p.Id == id);
                if (project == null)
                {
                    return HttpNotFound();
                }
                ViewBag.accessibility = "Customer,MD,MTL,MTS";
                return View(project);
            }
            else
            {
                return RedirectToAction("Index", "WelcomeHome");
            }
        }
    }
}