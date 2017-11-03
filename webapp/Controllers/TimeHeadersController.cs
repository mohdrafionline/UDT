using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class TimeHeadersController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            TimeHeader model = new TimeHeader();
            return View("Index", model);
            //return View();
        }

        public ActionResult GetTimeHeader(TimeHeader timeHeader)
        {
            using (var db = new DBEntity())
            {               
                var result = db.TimeHeaders.ToList();
                return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            using (var db = new DBEntity())
            {
                var v = db.TimeHeaders.Where(a => a.TimeHeaderID == id).FirstOrDefault();
                return View(v);
            }
        }

        //[HttpPost]
        //[MultiButton(MatchFormKey = "action", MatchFormValue = "Save")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(TimeHeader timeHeader)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var db = new DBEntity())
                {
                    if (timeHeader.TimeHeaderID > 0)
                    {
                        //Edit
                        var v = db.TimeHeaders.Where(a => a.TimeHeaderID == timeHeader.TimeHeaderID).FirstOrDefault();
                        if (v != null)
                        {
                            v.UserID = timeHeader.UserID;
                            v.DivisonID = timeHeader.DivisonID;
                            v.WorkRoleID = timeHeader.WorkRoleID;
                            v.Aggrement = timeHeader.Aggrement;
                            v.TimeDate = timeHeader.TimeDate;
                            v.TimeDetailID = timeHeader.TimeDetailID;
                            v.AggrementTypeID = timeHeader.AggrementTypeID;
                        } }
                    else
                    {
                        //Save
                        db.TimeHeaders.Add(timeHeader);
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
                var v = db.TimeHeaders.Where(a => a.TimeHeaderID == id).FirstOrDefault();
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
        public ActionResult DeleteTimeHeader(int id)
        {
            bool status = false;
            using (var db = new DBEntity())
            {
                var v = db.TimeHeaders.Where(a => a.UserID == id).FirstOrDefault();
                if (v != null)
                {
                    db.TimeHeaders.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}