﻿using System;
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
    }
}