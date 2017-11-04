using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
                var result = db.Staffs.OrderBy(a => a.Title).ToList();

                return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetStaffbyId(int id)
        {
            using (var db = new DBEntity())
            {
                var v = db.Staffs.Where(a => a.StaffID == id).FirstOrDefault();
                return Json(v);
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
                //get files

                if (Request.Files.AllKeys.Contains("StaffPhoto[]"))
                {
                    HttpPostedFileBase file = Request.Files["StaffPhoto[]"];
                    //Save file content goes here
                    string fileName = "";
                    Random random = new Random();
                    string randomname = random.Next().ToString();
                    if (file != null && file.ContentLength > 0)
                    {
                        var filePath = Server.MapPath("~/files/");
                        bool isExists = System.IO.Directory.Exists(filePath);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(filePath);
                        fileName = randomname + file.FileName;
                        var path = string.Format("{0}\\{1}", filePath, fileName);
                        file.SaveAs(path);
                        staff.StaffPhoto = "/files/" + fileName;
                    }
                }
                if (Request.Files.AllKeys.Contains("Insurance[]"))
                {
                    HttpPostedFileBase file = Request.Files["Insurance[]"];
                    //Save file content goes here
                    string fileName = "";
                    Random random = new Random();
                    string randomname = random.Next().ToString();
                    if (file != null && file.ContentLength > 0)
                    {
                        var filePath = Server.MapPath("~/files/");
                        bool isExists = System.IO.Directory.Exists(filePath);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(filePath);
                        fileName = randomname + file.FileName;
                        var path = string.Format("{0}\\{1}", filePath, fileName);
                        file.SaveAs(path);
                        staff.W9Form = "/files/" + fileName;
                    }
                }
                using (var db = new DBEntity())
                {
                    if (staff.StaffID > 0)
                    {
                        //Edit
                        var v = db.Staffs.Where(a => a.StaffID == staff.StaffID).FirstOrDefault();
                        if (v != null)
                        {
                            v.Address = staff.Address;
                            v.BirthDate = staff.BirthDate;
                            v.City = staff.City;
                            v.Contractor = staff.Contractor;
                            v.Country = staff.Country;
                            v.DepartmentID = staff.DepartmentID;
                            v.EmailID = staff.EmailID;
                            v.Extension = staff.Extension;
                            v.FirstName = staff.FirstName;
                            v.LastName = staff.LastName;
                            v.MobilePhone = staff.MobilePhone;
                            v.Password = staff.Password;
                            v.MobilePhone = staff.MobilePhone;
                            v.Photo = staff.Photo;
                            v.PostalCode = staff.PostalCode;
                            v.Region = staff.Region;
                            v.StaffNumber = staff.StaffNumber;
                            if (staff.StaffPhoto != null)
                                v.StaffPhoto = staff.StaffPhoto;
                            v.TerminationDate = staff.TerminationDate;
                            if (staff.W9Form != null)
                                v.W9Form = staff.W9Form;
                        }
                    }
                    else
                    {
                        db.Staffs.Add(staff);
                    }
                    db.SaveChanges();
                    status = true;
                }
            }
            return RedirectToAction("Index");
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


        [HttpPost]
        public JsonResult DeleteStaffByID(int id)
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