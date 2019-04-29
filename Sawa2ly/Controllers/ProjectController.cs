using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sawa2ly.Extensions;
using Sawa2ly.ViewModels;
using Sawa2ly.Models;
using System.Data.Entity;

namespace Sawa2ly.Controllers
{
    public class ProjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Project/1
        public ActionResult Index(int? id)
        {
            if (Request.IsAuthenticated) {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Project project = db.Project
                    .Include(c => c.Customer)
                    .Include(c => c.MD)
                    .Include(c => c.MTL)
                    .SingleOrDefault(p => p.Id == id);

                if (project == null)
                {
                    return HttpNotFound();
                }
                var projectTrainees = db.ProjectTrainees.Where(p => p.ProjectId == project.Id).Include(t => t.MTS).ToList();
                var posts = db.Posts.Where(p => p.ProjectId == project.Id).Include(t => t.User).ToList();
                var traineeevaluate = db.TraineeEvaluate.Where(p => p.ProjectId == project.Id).Include(t => t.MTS).ToList();
                var userId = User.Identity.GetUserID();
                if ( userId == project.CustomerId ||  userId == project.MDID || userId == project.MTLID)
                {
                    ViewBag.accessibility = "member";
                }else if ( db.ProjectTrainees.Any(p => p.ProjectId == project.Id && p.MTSID == userId) )
                {
                    ViewBag.accessibility = "member";
                }
                else
                {
                    ViewBag.accessibility = "visitor";
                }

                if (User.Identity.GetUserRule() == "2")
                {
                    var PRC = db.ProjectRequestsMD.Any(o => o.ProjectId == project.Id && o.MDID == userId);
                    
                    if (PRC)
                    {
                        var PR = db.ProjectRequestsMD.First(a => a.ProjectId == project.Id && a.MDID == userId);
                        ViewBag.requestid = PR.Id;
                        ViewBag.requested = "true";
                    }
                    else
                    {
                        ViewBag.requested = "false";
                    }
                    //ProjectRequestsMD ProjectRequestsMD = db.ProjectRequestsMD.Find();
                }

                var model = new ProjectAndTrainees { Project = project , ProjectTrainees = projectTrainees , Posts = posts , TraineeEvaluate = traineeevaluate};
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "WelcomeHome");
            }
        }

        // GET: Project/EditModule/1
        public ActionResult EditModule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/EditModule/5
        [HttpPost]
        public ActionResult EditModule(int proId,DateTime StartDate,DateTime EndDate,int status,Double price)
        {
            if (ModelState.IsValid)
            {
                var project = db.Project.First(a => a.Id == proId);
                project.StartDate = StartDate;
                project.EndDate = EndDate;
                project.Status = status;
                project.Price = price;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = proId });
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddRequest(int proId , String customerId)
        {

            var userId = User.Identity.GetUserID();
            db.ProjectRequestsMD.Add(new ProjectRequestsMD { ProjectId = proId, MDID = userId , CustomerId = customerId});
            db.SaveChanges();
            return RedirectToAction("Index", new { id = proId });
        }

        [HttpPost]
        public ActionResult DeleteRequest(int proId , int reqId)
        {
            var PR = db.ProjectRequestsMD.Single(a => a.Id == reqId);
            db.ProjectRequestsMD.Remove(PR);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = proId });
        }

        // GET: Project/AddMTL/1
        public ActionResult AddMTL(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.accessibility = id;
            var users = db.Users.Where(r => r.UserRule == "3").ToList();
            var requests = db.ProjectsJoinRequests.Where(p => p.ProjectId == id).ToList();
            var model = new UsersAndRequest { Users = users, Requests = requests };
            return View(model);
        }

        [HttpPost]
        public ActionResult MTLRequest(int proId, String recieverId)
        {
            if (db.Project.Any(o => o.Id == proId && o.MTLID == recieverId))
            {
                return RedirectToAction("Index", new { id = proId });
            }
            else
            {
                var userId = User.Identity.GetUserID();
                db.ProjectsJoinRequests.Add(new ProjectsJoinRequests { ProjectId = proId, RecieverId = recieverId, SenderId = userId });
                db.SaveChanges();
                return RedirectToAction("AddMTL", new { id = proId });
            }

        }

        [HttpPost]
        public ActionResult MTLDeleteRequest(int proId, int reqId)
        {
            var PR = db.ProjectsJoinRequests.Single(a => a.Id == reqId);
            db.ProjectsJoinRequests.Remove(PR);
            db.SaveChanges();
            return RedirectToAction("AddMTL", new { id = proId });
        }

        // GET: Project/AddMTS/1
        public ActionResult AddMTS(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.accessibility = id;
            var users = db.Users.Where(r => r.UserRule == "4").ToList();
            var requests = db.ProjectsJoinRequests.Where(p => p.ProjectId == id).ToList();
            var model = new UsersAndRequest { Users = users, Requests = requests };
            return View(model);
        }

        [HttpPost]
        public ActionResult MTSRequest(int proId, String recieverId)
        {
            if (db.ProjectTrainees.Any(o => o.ProjectId == proId && o.MTSID == recieverId))
            {
                return RedirectToAction("Index", new { id = proId });
            }
            else
            {
                var userId = User.Identity.GetUserID();
                db.ProjectsJoinRequests.Add(new ProjectsJoinRequests { ProjectId = proId, RecieverId = recieverId, SenderId = userId });
                db.SaveChanges();
                return RedirectToAction("AddMTS", new { id = proId });
            }

        }


        [HttpPost]
        public ActionResult MTSDeleteRequest(int proId, int reqId)
        {
            var PR = db.ProjectsJoinRequests.Single(a => a.Id == reqId);
            db.ProjectsJoinRequests.Remove(PR);
            db.SaveChanges();
            return RedirectToAction("AddMTS", new { id = proId });
        }

        
        [HttpPost]
        public ActionResult LeaveProject(int proId)
        {
            var userId = User.Identity.GetUserID();
            var userRole = User.Identity.GetUserRule();
            if (userRole == "2")//Md
            {
                var project = db.Project.First(a => a.Id == proId);
                project.MDID = null;
                project.MTLID = null;
                project.StartDate = null;
                project.EndDate = null;
                project.Status = null;
                project.Price = null;
                db.SaveChanges();
                db.ProjectTrainees.RemoveRange(db.ProjectTrainees.Where(c => c.ProjectId == proId));
                db.SaveChanges();
            }
            else if (userRole == "3")
            {
                var project = db.Project.First(a => a.Id == proId);
                project.MTLID = null;
                db.SaveChanges();
            }
            else
            {
                var pt = db.ProjectTrainees.First(a => a.ProjectId == proId && a.MTSID == userId);
                db.ProjectTrainees.Remove(pt);
                db.SaveChanges();
                var TE = db.TraineeEvaluate.Single(a => a.ProjectId == proId && a.MTSID == userId);
                db.TraineeEvaluate.Remove(TE);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { id = proId });
            
        }

        [HttpPost]
        public ActionResult DeleteMTL(int proId)
        {
            if (ModelState.IsValid)
            {
                var project = db.Project.First(a => a.Id == proId);
                project.MTLID = null;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = proId });
            }
            return RedirectToAction("Index", new { id = proId });
        }

        [HttpPost]
        public ActionResult DeleteMTS(int proId , int TaiId)
        {
            var PR = db.ProjectTrainees.Single(a => a.Id == TaiId);
            var MTSId = PR.MTSID;
            db.ProjectTrainees.Remove(PR);
            db.SaveChanges();
            var TE = db.TraineeEvaluate.Single(a => a.ProjectId == proId && a.MTSID == MTSId);
            db.TraineeEvaluate.Remove(TE);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = proId });
            
        }

        [HttpPost]
        public ActionResult Evaluate(int proId, int TEID, String message)
        {
            var TE = db.TraineeEvaluate.First(a => a.Id == TEID);
            TE.Message = message;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = proId });

        }

        [HttpPost]
        public ActionResult Post(int proId, String msg)
        {
            var userId = User.Identity.GetUserID();
            db.Posts.Add(new Posts { Message = msg , Date = DateTime.Now , ProjectId = proId , UserID = userId});
            db.SaveChanges();
            return RedirectToAction("Index", new { id = proId });
        }



    }
}