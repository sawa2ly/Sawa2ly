using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.Models;

namespace Sawa2ly.Controllers
{
    public class CustomerController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            if (User.Identity.GetUserRule() == "1")
            {
                var userId = User.Identity.GetUserID();
                var project = db.Project.Where( I => I.CustomerId ==  userId).Include(p => p.Customer).ToList();
                return View(project);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        [HttpPost]
        public ActionResult Save(string ProjectName, string ProjectDesc)
        {
            var customerid = User.Identity.GetUserID();
            db.Project.Add(new Project { Name = ProjectName, Description = ProjectDesc, CustomerId = customerid });
            db.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        // GET: Customer/unassignedProjects
        public ActionResult unassignedProjects()
        {
            if (User.Identity.GetUserRule() == "1")
            {
                var userId = User.Identity.GetUserID();
                var project = db.Project.Where(I => I.CustomerId == userId && I.MDID == null).Include(p => p.Customer).ToList();
                return View(project);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        // GET: Customer/DeliveredProjects
        public ActionResult DeliveredProjects()
        {
            if (User.Identity.GetUserRule() == "1")
            {
                var userId = User.Identity.GetUserID();
                var project = db.Project.Where(I => I.CustomerId == userId && I.MDID != null).Include(p => p.Customer).ToList();
                return View(project);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        // GET: Customer/Requests
        public ActionResult Requests()
        {
            if (User.Identity.GetUserRule() == "1")
            {
                var userId = User.Identity.GetUserID();
                var requests = db.ProjectRequestsMD.Where(c=>c.CustomerId == userId).Include(p=>p.Project).Include(m=>m.MD).ToList();
                return View(requests);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        [HttpPost]
        public ActionResult AcceptRequest (int reqId, String MDId , int proId)
        {
            var project = db.Project.First(a => a.Id == proId);
            project.MDID = MDId;
            db.SaveChanges();
            var PR = db.ProjectRequestsMD.Single(a => a.Id == reqId);
            db.ProjectRequestsMD.Remove(PR);
            db.SaveChanges();
            return RedirectToAction("Requests");

        }

        [HttpPost]
        public ActionResult DeleteRequest(int reqId)
        {
            var PR = db.ProjectRequestsMD.Single(a => a.Id == reqId);
            db.ProjectRequestsMD.Remove(PR);
            db.SaveChanges();
            return RedirectToAction("Requests");
        }

        public ActionResult EditProfile(string id)
        {

            var user = db.Users.SingleOrDefault(c => c.Id == id);


            return View(user);
        }


        [HttpPost]
        public ActionResult SaveEdit(string first_name, string last_name, string email, string phone, string currpassword, string imegurl)
        {
            var id = User.Identity.GetUserID();
            var user = db.Users.SingleOrDefault(c => c.Id == id);
            user.FName = first_name;
            user.LName = last_name;
            user.PhoneNumber = phone;
            user.Email = email;
            user.UserName = email;
            user.UserImageUrl = "~/Sources/" + imegurl;
            db.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

    }
}