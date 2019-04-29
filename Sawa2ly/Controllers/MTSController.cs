﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.Models;
using System.Data.Entity;

namespace Sawa2ly.Controllers
{
    public class MTSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: MTS
        public ActionResult Index()
        {
            if (User.Identity.GetUserRule() == "4")
            {
                var userId = User.Identity.GetUserID();
                var projectTrainees = db.ProjectTrainees.Where(I => I.MTSID == userId).Include(p => p.Project.Customer).ToList();
                return View(projectTrainees);
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
                var userId = User.Identity.GetUserID();
                var projectTrainees = db.ProjectTrainees.Where(I => I.MTSID == userId).Include(p => p.Project).ToList();
                return View(projectTrainees);
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
                var userId = User.Identity.GetUserID();
                var projectTrainees = db.ProjectTrainees.Where(I => I.MTSID == userId).Include(p => p.Project).ToList();
                return View(projectTrainees); ;
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
                var userId = User.Identity.GetUserID();
                var requests = db.ProjectsJoinRequests.Where(c => c.RecieverId == userId).Include(p => p.Project).Include(m => m.Sender).ToList();
                return View(requests);
            }
            else
            {
                return RedirectToAction("RedirectToProfile", "Home");
            }

        }

        [HttpPost]
        public ActionResult AcceptRequest(int reqId, String MTSId, int proId)
        {
            db.ProjectTrainees.Add(new ProjectTrainees { ProjectId = proId, MTSID = MTSId });
            db.SaveChanges();
            var PR = db.ProjectsJoinRequests.Single(a => a.Id == reqId);
            db.ProjectsJoinRequests.Remove(PR);
            db.SaveChanges();
            return RedirectToAction("Requests");

        }

        [HttpPost]
        public ActionResult DeleteRequest(int reqId)
        {
            var PR = db.ProjectsJoinRequests.Single(a => a.Id == reqId);
            db.ProjectsJoinRequests.Remove(PR);
            db.SaveChanges();
            return RedirectToAction("Requests");
        }

        public ActionResult EditProfile(string id)
        {

            var user = db.Users.SingleOrDefault(c => c.Id == id);


            return View(user);
        }


        [HttpPost]
        public ActionResult SaveEdit(string first_name, string last_name, string email, string phone, string currpassword, string imegurl)
        {
            var id = User.Identity.GetUserID();
            var user = db.Users.SingleOrDefault(c => c.Id == id);
            user.FName = first_name;
            user.LName = last_name;
            user.PhoneNumber = phone;
            user.Email = email;
            user.UserName = email;
            user.UserImageUrl = "~/Sources/" + imegurl;

            db.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }


    }
}