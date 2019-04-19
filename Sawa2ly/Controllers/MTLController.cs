using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;

namespace Sawa2ly.Controllers
{
    public class MTLController : Controller
    {
        // GET: MTL
        public ActionResult Index()
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

        // GET: MTL/CurrentProjects
        public ActionResult CurrentProjects()
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

        // GET: MTL/PreviousProjects
        public ActionResult PreviousProjects()
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