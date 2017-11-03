using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            Department model = new Department();
            return View("Index", model);
            //return View();
        }

        public ActionResult GetDepartments(Department department)
        {
            using (var db = new DBEntity())
            {
                //var employ = (from s in db.TblEmployees where s.FirstName == employees.FirstName select s).ToList();
                var result = db.Departments.OrderBy(a => a.DepartmentName).ToList();
                return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            using (var db = new DBEntity())
            {
                var v = db.Departments.Where(a => a.DepartmentID == id).FirstOrDefault();
                return View(v);
            }
        }

        //[HttpPost]
        //[MultiButton(MatchFormKey = "action", MatchFormValue = "Save")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(Department department)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var db = new DBEntity())
                {
                    if (department.DepartmentID > 0)
                    {
                        //Edit
                        var v = db.Departments.Where(a => a.DepartmentID == department.DepartmentID).FirstOrDefault();
                        if (v != null)
                        {
                            v.DepartmentName = department.DepartmentName;                          
                        }
                    }
                    else
                    {
                        //Save
                        db.Departments.Add(department);
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
                var v = db.Departments.Where(a => a.DepartmentID == id).FirstOrDefault();
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
        public ActionResult DeleteDepartment(int id)
        {
            bool status = false;
            using (var db = new DBEntity())
            {
                var v = db.Departments.Where(a => a.DepartmentID == id).FirstOrDefault();
                if (v != null)
                {
                    db.Departments.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}