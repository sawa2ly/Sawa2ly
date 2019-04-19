using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;

namespace Sawa2ly.Controllers
{
    public class MDController : Controller
    {
        // GET: MD
        public ActionResult Index()
        {
            if (User.Identity.GetUserRule() == "2")
            {
                return View();
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
                return View();
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
                return View();
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
                return View();
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }


    }
}