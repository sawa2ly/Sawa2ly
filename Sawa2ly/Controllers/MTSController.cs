using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.Models;
using System.Data.Entity;

namespace Sawa2ly.Controllers
{
    public class MTSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: MTS
        public ActionResult Index()
        {
            if (User.Identity.GetUserRule() == "4")
            {
                var userId = User.Identity.GetUserID();
                var projectTrainees = db.ProjectTrainees.Where(I => I.MTSID == userId).Include(p => p.Project.Customer).ToList();
                return View(projectTrainees);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        // GET: MTS/CurrentProjects
        public ActionResult CurrentProjects()
        {
            if (User.Identity.GetUserRule() == "4")
            {
                var userId = User.Identity.GetUserID();
                var projectTrainees = db.ProjectTrainees.Where(I => I.MTSID == userId).Include(p => p.Project).ToList();
                return View(projectTrainees);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        // GET: MTS/DeliveredProjects
        public ActionResult PreviousProjects()
        {
            if (User.Identity.GetUserRule() == "4")
            {
                var userId = User.Identity.GetUserID();
                var projectTrainees = db.ProjectTrainees.Where(I => I.MTSID == userId).Include(p => p.Project).ToList();
                return View(projectTrainees); ;
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        // GET: MTS/Requests
        public ActionResult Requests()
        {
            if (User.Identity.GetUserRule() == "4")
            {
                return View();
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

    }
}