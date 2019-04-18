using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.Models;

namespace Sawa2ly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            
                var project = db.Project.ToList();

                return View(project);
            
            
        }
            [HttpPost]


        public ActionResult Save(string ProjectName, string ProjectDesc)
        {

            var customerid = User.Identity.GetUserID();
            db.Project.Add(new Project { Name = ProjectName, Description = ProjectDesc, CustomerId = customerid });
            db.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
    }
    
}