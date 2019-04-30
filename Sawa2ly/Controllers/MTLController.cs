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
                var userId = User.Identity.GetUserID();
                var requests = db.ProjectsJoinRequests.Where(c => c.RecieverId == userId).Include(p => p.Project).Include(m => m.Sender).ToList();
                return View(requests);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        public ActionResult EditProfile()
        {
            if (User.Identity.GetUserRule() == "3")
            {
                var userId = User.Identity.GetUserID();
                var user = db.Users.SingleOrDefault(c => c.Id == userId);
                return View(user);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        [HttpPost]
        public ActionResult AcceptRequest(int reqId, String MTLId, int proId)
        {
            var project = db.Project.First(a => a.Id == proId);
            project.MTLID = MTLId;
            db.SaveChanges();
            var PR = db.ProjectsJoinRequests.Single(a => a.Id == reqId);
            db.ProjectsJoinRequests.Remove(PR);
            db.SaveChanges();
            return RedirectToAction("Requests");

        }

        [HttpPost]
        public ActionResult DeleteRequest(int reqId)
        {
            var PR = db.ProjectsJoinRequests.Single(a => a.Id == reqId);
            db.ProjectsJoinRequests.Remove(PR);
            db.SaveChanges();
            return RedirectToAction("Requests");
        }


    }
}