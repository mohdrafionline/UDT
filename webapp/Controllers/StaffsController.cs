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
                //get files

                if (Request.Files.AllKeys.Contains("StaffPhoto[]"))
                {
                    HttpPostedFileBase file = Request.Files["StaffPhoto[]"];
                    //Save file content goes here
                    var guid = Guid.NewGuid();
                    string profilePicName = guid + file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}userfiles\\", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "StaffPhoto");

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, guid + file.FileName);
                        file.SaveAs(path);
                        staff.StaffPhoto = System.IO.File.ReadAllBytes(path);
                    }
                }
                if (Request.Files.AllKeys.Contains("Insurance[]"))
                {
                    HttpPostedFileBase file = Request.Files["Insurance[]"];
                    //Save file content goes here
                    var guid = Guid.NewGuid();
                    string profilePicName = guid + file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}userfiles\\", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "W9Forms");

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, guid + file.FileName);
                        file.SaveAs(path);
                        staff.W9Form = System.IO.File.ReadAllBytes(path);
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
                            v.StaffPhoto = staff.StaffPhoto;
                            v.TerminationDate = staff.TerminationDate;
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

    }
}