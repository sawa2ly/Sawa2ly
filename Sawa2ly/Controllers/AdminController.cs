using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Sawa2ly.Extensions;
using Sawa2ly.Models;


namespace Sawa2ly.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
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
                var projects = _context.Project.ToList();
                return View(projects);
            }

            return RedirectToAction("ListProjects", "Admin");

        }
        public ActionResult ListUsers()
        {
            if (User.Identity.GetUserRule() == "5")
            {
                var users = _context.Users.ToList();
                return View(users);
            }

            return RedirectToAction("ListUsers", "Admin");

        }

        public ActionResult DeleteProject(int id)
        {
            if (User.Identity.GetUserRule() == "5")
            {
                var projDel = _context.Project.SingleOrDefault(m => m.Id == id);
                if (projDel == null)
                {
                    return HttpNotFound();
                }

                _context.Project.Remove(projDel);
            }

                 _context.SaveChanges();
                return RedirectToAction("ListProjects", "Admin");
            
        }
        
       
        public ActionResult EditProfile()
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

    }
}
