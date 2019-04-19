using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;

namespace Sawa2ly.Controllers
{
    public class MTSController : Controller
    {
        // GET: MTS
        public ActionResult Index()
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

        // GET: MTS/CurrentProjects
        public ActionResult CurrentProjects()
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

        // GET: MTS/DeliveredProjects
        public ActionResult PreviousProjects()
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