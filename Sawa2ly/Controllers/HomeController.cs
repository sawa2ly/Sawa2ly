using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;

namespace Sawa2ly.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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