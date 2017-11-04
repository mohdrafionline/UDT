using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            tblEmployees model = new tblEmployees();
            return View("Index", model);
            //return View();
        }

        public ActionResult GetEmployees(tblEmployees employees)
        {
            using (var db = new DBEntity())
            {
                //var employ = (from s in db.TblEmployees where s.FirstName == employees.FirstName select s).ToList();
                var employ = db.TblEmployees.OrderBy(a => a.FirstName).ToList();

                return Json(new { data = employ }, JsonRequestBehavior.AllowGet);
            }
        }
     

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (var db = new DBEntity())
            {
                var v = db.TblEmployees.Where(a => a.Id == id).FirstOrDefault();
                return View(v);
            }
        }

        //[HttpPost]
        //[MultiButton(MatchFormKey = "action", MatchFormValue = "Save")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(tblEmployees employees)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var db = new DBEntity())
                {
                    if (employees.Id > 0)
                    {
                        //Edit
                        var v = db.TblEmployees.Where(a => a.Id == employees.Id).FirstOrDefault();
                        if (v != null)
                        {
                            v.FirstName = employees.FirstName;
                            v.LastName = employees.LastName;
                            v.EmailID = employees.EmailID;
                            v.City = employees.City;
                            v.Country = employees.Country;
                        }
                    }
                    else
                    {
                        //Save
                        db.TblEmployees.Add(employees);
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
                var v = db.TblEmployees.Where(a => a.Id == id).FirstOrDefault();
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
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (var db = new DBEntity())
            {
                var v = db.TblEmployees.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    db.TblEmployees.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}