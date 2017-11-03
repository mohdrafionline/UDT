using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class WorkTypesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            WorkType model = new WorkType();
            return View("Index", model);
            //return View();
        }

        public ActionResult GetWorkTypes(WorkType workType)
        {
            using (var db = new DBEntity())
            {              
                var employ = db.WorkTypes.OrderBy(a => a.WorkTypeName).ToList();

                return Json(new { data = employ }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (var db = new DBEntity())
            {
                var v = db.WorkTypes.Where(a => a.WorkTypeID == id).FirstOrDefault();
                return View(v);
            }
        }

        //[HttpPost]
        //[MultiButton(MatchFormKey = "action", MatchFormValue = "Save")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(WorkType workType)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var db = new DBEntity())
                {
                    if (workType.WorkTypeID > 0)
                    {
                        //Edit
                        var v = db.WorkTypes.Where(a => a.WorkTypeID == workType.WorkTypeID).FirstOrDefault();
                        if (v != null)
                        {
                            v.WorkTypeName = workType.WorkTypeName;
                           
                        }
                    }
                    else
                    {
                        //Save
                        db.WorkTypes.Add(workType);
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
                var v = db.WorkTypes.Where(a => a.WorkTypeID == id).FirstOrDefault();
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
        public ActionResult DeleteWorkType(int id)
        {
            bool status = false;
            using (var db = new DBEntity())
            {
                var v = db.WorkTypes.Where(a => a.WorkTypeID == id).FirstOrDefault();
                if (v != null)
                {
                    db.WorkTypes.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}