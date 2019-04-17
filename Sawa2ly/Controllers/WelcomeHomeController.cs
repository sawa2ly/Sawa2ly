using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sawa2ly.Controllers
{
    public class WelcomeHomeController : Controller
    {
        // GET: WelcomeHome
        public ActionResult Index()
        {
            return View();
        }
    }
}