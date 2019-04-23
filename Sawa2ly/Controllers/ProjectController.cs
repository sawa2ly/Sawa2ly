using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.ViewModels;
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
                Project project = db.Project
                    .Include(c => c.Customer)
                    .Include(c => c.MD)
                    .Include(c => c.MTL)
                    .SingleOrDefault(p => p.Id == id);

                if (project == null)
                {
                    return HttpNotFound();
                }
                var projectTrainees = db.ProjectTrainees.Where(p => p.ProjectId == project.Id).Include(t => t.MTS).ToList();
                var userId = User.Identity.GetUserID();
                if ( userId == project.CustomerId ||  userId == project.MDID || userId == project.MTLID)
                {
                    ViewBag.accessibility = "member";
                }else if ( db.ProjectTrainees.Any(p => p.ProjectId == project.Id && p.MTSID == userId) )
                {
                    ViewBag.accessibility = "member";
                }
                else
                {
                    ViewBag.accessibility = "visitor";
                }
                var model = new ProjectAndTrainees { Project = project , ProjectTrainees = projectTrainees};
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "WelcomeHome");
            }
        }

        // GET: Project/EditModule/1
        public ActionResult EditModule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/EditModule/5
        [HttpPost]
        public ActionResult EditModule(int proId,DateTime StartDate,DateTime EndDate,int status,Double price)
        {
            if (ModelState.IsValid)
            {
                var project = db.Project.First(a => a.Id == proId);
                project.StartDate = StartDate;
                project.EndDate = EndDate;
                project.Status = status;
                project.Price = price;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = proId });
            }
            return View();
        }

    }
}