using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;

namespace Sawa2ly.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            if (User.Identity.GetUserRule() == "1")
            {
                return View();
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        // GET: Customer/unassignedProjects
        public ActionResult unassignedProjects()
        {
            if (User.Identity.GetUserRule() == "1")
            {
                return View();
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
                return View();
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
                return View();
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }
    }
}