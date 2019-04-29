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
    public class MDController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: MD
        public ActionResult Index()
        {
            if (User.Identity.GetUserRule() == "2")
            {
                var userId = User.Identity.GetUserID();
                var project = db.Project.Where(I => I.MDID == userId).Include(p => p.Customer).ToList();
                return View(project);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        // GET: MD/DiscoverProjects
        public ActionResult DiscoverProjects()
        {
            if (User.Identity.GetUserRule() == "2")
            {
                return View(db.Project.Where(I => I.MDID == null).Include(p => p.Customer).ToList());
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        // GET: MD/CurrentProjects
        public ActionResult CurrentProjects()
        {
            if (User.Identity.GetUserRule() == "2")
            {
                var userId = User.Identity.GetUserID();
                var project = db.Project.Where(I => I.MDID == userId && I.Status != 1).Include(p => p.Customer).ToList();
                return View(project);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        // GET: MD/PreviousProjects
        public ActionResult PreviousProjects()
        {
            if (User.Identity.GetUserRule() == "2")
            {
                var userId = User.Identity.GetUserID();
                var project = db.Project.Where(I => I.MDID == userId && I.Status == 1).Include(p => p.Customer).ToList();
                return View(project);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

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