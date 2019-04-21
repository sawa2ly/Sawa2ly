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
    public class MTLController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: MTL
        public ActionResult Index()
        {
            if (User.Identity.GetUserRule() == "3")
            {
                var userId = User.Identity.GetUserID();
                var project = db.Project.Where(I => I.MTLID == userId).Include(p => p.Customer).ToList();
                return View(project);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }
            
        }

        // GET: MTL/CurrentProjects
        public ActionResult CurrentProjects()
        {
            if (User.Identity.GetUserRule() == "3")
            {
                var userId = User.Identity.GetUserID();
                var project = db.Project.Where(I => I.MTLID == userId && I.Status != 1).Include(p => p.Customer).ToList();
                return View(project);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        // GET: MTL/PreviousProjects
        public ActionResult PreviousProjects()
        {
            if (User.Identity.GetUserRule() == "3")
            {
                var userId = User.Identity.GetUserID();
                var project = db.Project.Where(I => I.MTLID == userId && I.Status == 1).Include(p => p.Customer).ToList();
                return View(project);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        // GET: MTL/Requests
        public ActionResult Requests()
        {
            if (User.Identity.GetUserRule() == "3")
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