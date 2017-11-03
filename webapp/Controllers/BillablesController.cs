using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class BillablesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            Billable model = new Billable();
            return View("Index", model);
            //return View();
        }

        public ActionResult GetBillable(Billable billable)
        {
            using (var db = new DBEntity())
            {
                //var employ = (from s in db.TblEmployees where s.FirstName == employees.FirstName select s).ToList();
                var employ = db.Billables.OrderBy(a => a.BillableName).ToList();

                return Json(new { data = employ }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            using (var db = new DBEntity())
            {
                var v = db.Billables.Where(a => a.BillableID == id).FirstOrDefault();
                return View(v);
            }
        }

        //[HttpPost]
        //[MultiButton(MatchFormKey = "action", MatchFormValue = "Save")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(Billable billable)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var db = new DBEntity())
                {
                    if (billable.BillableID > 0)
                    {
                        //Edit
                        var v = db.Billables.Where(a => a.BillableID == billable.BillableID).FirstOrDefault();
                        if (v != null)
                        {
                            v.BillableName = billable.BillableName;                          
                        }
                    }
                    else
                    {
                        //Save
                        db.Billables.Add(billable);
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
                var v = db.Billables.Where(a => a.BillableID == id).FirstOrDefault();
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
        public ActionResult DeleteBillable(int id)
        {
            bool status = false;
            using (var db = new DBEntity())
            {
                var v = db.Billables.Where(a => a.BillableID == id).FirstOrDefault();
                if (v != null)
                {
                    db.Billables.Remove(v);
                    db.SaveChanges();
                    status = true;  
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}