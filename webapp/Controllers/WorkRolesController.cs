using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class WorkRolesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            WorkRole model = new WorkRole();
            return View("Index", model);
            //return View();
        }

        public ActionResult GetWorkRole(WorkRole workRole)
        {
            using (var db = new DBEntity())
            {
                //var employ = (from s in db.TblEmployees where s.FirstName == employees.FirstName select s).ToList();
                var employ = db.WorkRoles.OrderBy(a => a.WorkRoleName).ToList();

                return Json(new { data = employ }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            using (var db = new DBEntity())
            {
                var v = db.WorkRoles.Where(a => a.WorkRoleID == id).FirstOrDefault();
                return View(v);
            }
        }

        //[HttpPost]
        //[MultiButton(MatchFormKey = "action", MatchFormValue = "Save")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(WorkRole workRole)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var db = new DBEntity())
                {
                    if (workRole.WorkRoleID > 0)
                    {
                        //Edit
                        var v = db.WorkRoles.Where(a => a.WorkRoleID == workRole.WorkRoleID).FirstOrDefault();
                        if (v != null)
                        {
                            v.WorkRoleName = workRole.WorkRoleName;
                        }
                    }
                    else
                    {
                        //Save
                        db.WorkRoles.Add(workRole);
                    }
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var db = new DBEntity())
            {
                //var employ = (from s in db.TblEmployees where s.FirstName == employees.FirstName select s).ToList();
                var v = db.WorkRoles.Where(a => a.WorkRoleID == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteWorkRole(int id)
        {
            bool status = false;
            using (var db = new DBEntity())
            {
                var v = db.WorkRoles.Where(a => a.WorkRoleID == id).FirstOrDefault();
                if (v != null)
                {
                    db.WorkRoles.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}