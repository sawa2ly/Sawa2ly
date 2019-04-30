using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.Models;
using System.IO;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace Sawa2ly.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
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
                var projects = db.Project.ToList();
                return View(projects);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        public ActionResult ListUsers()
        {
            if (User.Identity.GetUserRule() == "5")
            {
                var users = db.Users.ToList();
                return View(users);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        public ActionResult EditProfile()
        {
            if (User.Identity.GetUserRule() == "5")
            {
                var userId = User.Identity.GetUserID();
                var user = db.Users.SingleOrDefault(c => c.Id == userId);
                return View(user);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        public ActionResult DeleteProject(int id)
        {
            if (User.Identity.GetUserRule() == "5")
            {
                var projDel = db.Project.SingleOrDefault(m => m.Id == id);
                if (projDel == null)
                {
                    return HttpNotFound();
                }

                db.Project.Remove(projDel);
            }

            db.SaveChanges();
            return RedirectToAction("ListProjects", "Admin");

        }

        [HttpPost]
        public ActionResult DeleteUsers(string id)
        {

            var userDel = db.Users.Find(id);


            db.Users.Remove(userDel);


            db.SaveChanges();
            return RedirectToAction("ListUsers");

        }

        [HttpPost]
        public ActionResult SaveEdit(string first_name, string last_name, string email, string phone, string currpassword, HttpPostedFileBase file)
        {
            var id = User.Identity.GetUserID();
            var user = db.Users.SingleOrDefault(c => c.Id == id);
            user.FName = first_name;
            user.LName = last_name;
            user.PhoneNumber = phone;
            user.Email = email;
            user.UserName = email;
            var path = "";
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if(Path.GetExtension(file.FileName).ToLower()==".jpg")
                    {
                        path = Path.Combine(Server.MapPath("~/Sources/images"), user.Id)+ ".jpg";
                        file.SaveAs(path);
                        user.UserImageUrl = "~/Sources/images/"+user.Id + ".jpg";
                    }else if ( Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                    {
                        path = Path.Combine(Server.MapPath("~/Sources/images"), user.Id, ".jpeg");
                        file.SaveAs(path);
                        user.UserImageUrl = "~/Sources/images/"+ user.Id + ".jpeg";
                    }
                }
            }
            db.SaveChanges();

            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            Identity.RemoveClaim(Identity.FindFirst("FName"));
            Identity.AddClaim(new Claim("FName", user.FName));
            Identity.RemoveClaim(Identity.FindFirst("LName"));
            Identity.AddClaim(new Claim("LName", user.LName));
            Identity.RemoveClaim(Identity.FindFirst("PhoneNumber"));
            Identity.AddClaim(new Claim("PhoneNumber", user.PhoneNumber));
            Identity.RemoveClaim(Identity.FindFirst("Email"));
            Identity.AddClaim(new Claim("Email", user.Email));
            Identity.RemoveClaim(Identity.FindFirst("UserImageUrl"));
            Identity.AddClaim(new Claim("UserImageUrl", user.UserImageUrl));
            var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(Identity), new AuthenticationProperties() { IsPersistent = true });

            return RedirectToAction("RedirectToProfile", "Home");
        }

    }
}
