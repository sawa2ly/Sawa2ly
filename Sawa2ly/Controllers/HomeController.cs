using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.Models;

namespace Sawa2ly.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "WelcomeHome");
            }

            return View(db.Project.ToList());
            
        }

        [HttpPost]
        public ActionResult Save(string ProjectName, string ProjectDesc)
        {

            var customerid = User.Identity.GetUserID();
            db.Project.Add(new Project { Name = ProjectName, Description = ProjectDesc, CustomerId = customerid });
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RedirectToProfile(string returnUrl)
        {

            switch (User.Identity.GetUserRule())
            {
                case "1": // Customer
                    {
                        return RedirectToAction("Index", "Customer"); /* , new { area = "Admin" }*/
                        break;
                    }
                case "2": //Markting Director
                    {
                        return RedirectToAction("Index", "MD");
                        break;
                    }
                case "3": //Markting Team Leader
                    {
                        return RedirectToAction("Index", "MTL");
                        break;
                    }

                case "4": //Markting Trainee
                    {
                        return RedirectToAction("Index", "MTS");
                        break;
                    }
                default: // Admin
                    {
                        return RedirectToAction("Index", "Admin");
                        break;
                    }

            }

        }
    }
}