using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class StaffsController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            Staff model = new Staff();
            return View("Index", model);
            //return View();
        }

        public ActionResult GetStaff(Staff staff)
        {
            using (var db = new DBEntity())
            {
                //var employ = (from s in db.TblEmployees where s.FirstName == employees.FirstName select s).ToList();
                var result = db.Staffs.OrderBy(a => a.Title).ToList();

                return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetbyId(int id)
        {
            using (var db = new DBEntity())
            {
                var v = db.Staffs.Where(a => a.StaffID == id).FirstOrDefault();
                return View(v);
            }
        }

        //[HttpPost]
        //[MultiButton(MatchFormKey = "action", MatchFormValue = "Save")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(Staff staff)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var db = new DBEntity())
                {
                    if (staff.StaffID > 0)
                    {
                        //Edit
                        var v = db.Staffs.Where(a => a.StaffID == staff.StaffID).FirstOrDefault();
                        if (v != null)
                        {
                            v.StaffID = staff.StaffID;
                            v.UserID = staff.UserID;
                            v.DepartmentID = staff.DepartmentID;
                            v.UserID = staff.UserID;
                            v.DepartmentID = staff.DepartmentID;
                            v.PositionID = staff.PositionID;
                            v.Title = staff.Title;
                            v.HireDate = staff.HireDate;
                            v.Address = staff.Address;
                            v.City = staff.City;
                            v.Region = staff.Region;
                            v.PostalCode = staff.PostalCode;
                            v.Phone = staff.Phone;
                            v.Extension = staff.Extension;
                            v.TerminationDate = staff.TerminationDate;
                            v.Photo = staff.Photo;
                        }
                    }
                    else
                    {
                        //Save
                        db.Staffs.Add(staff);
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
                var v = db.Staffs.Where(a => a.StaffID == id).FirstOrDefault();
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
        public ActionResult DeleteStaff(int id)
        {
            bool status = false;
            using (var db = new DBEntity())
            {
                var v = db.Staffs.Where(a => a.StaffID == id).FirstOrDefault();
                if (v != null)
                {
                    db.Staffs.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}