using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.Models;

namespace Sawa2ly.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            if (User.Identity.GetUserRule() == "5")
            {
                return View();
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }
            
        }

        public ActionResult ListProjects()
        {
            if (User.Identity.GetUserRule() == "5")
            {
                return View();
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        public ActionResult ListUsers()
        {
            if (User.Identity.GetUserRule() == "5")
            {
                return View();
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
