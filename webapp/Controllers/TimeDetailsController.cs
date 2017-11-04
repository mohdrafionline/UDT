using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class TimeDetailsController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            TimeDetail model = new TimeDetail();
            return View("Index", model);
            //return View();
        }

        public ActionResult GetEmployees(TimeDetail timeDetail)
        {
            using (var db = new DBEntity())
            {
                //var employ = (from s in db.TblEmployees where s.FirstName == employees.FirstName select s).ToList();
                var result = db.TimeDetails.ToList();
                return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (var db = new DBEntity())
            {
                var v = db.TimeDetails.Where(a => a.TimeDetailID == id).FirstOrDefault();
                return View(v);
            }
        }

        //[HttpPost]
        //[MultiButton(MatchFormKey = "action", MatchFormValue = "Save")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(TimeDetail timeDetail)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var db = new DBEntity())
                {
                    if (timeDetail.TimeDetailID > 0)
                    {
                        //Edit
                        var v = db.TimeDetails.Where(a => a.TimeDetailID == timeDetail.TimeDetailID).FirstOrDefault();
                        if (v != null)
                        {
                            v.TimeIn = timeDetail.TimeIn;
                            v.TimeOut = timeDetail.TimeOut;
                            v.TimeDeduct = timeDetail.TimeDeduct;
                            v.WorkTypeID = timeDetail.WorkTypeID;
                            v.BillableID = timeDetail.BillableID;
                            v.Notes = timeDetail.Notes;
                        }
                        else
                        {
                            //Save
                            db.TimeDetails.Add(timeDetail);
                        }
                        db.SaveChanges();
                        status = true;
                    }
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
                var v = db.TimeDetails.Where(a => a.TimeDetailID == id).FirstOrDefault();
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
        public ActionResult DeleteTimeDetail(int id)
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

        [HttpGet]        
        public ActionResult GetTimeSheetDetails(int UserID)
        {
            var result = new List<TimeSheet>();

            result.Add(new TimeSheet() {
                Year = 2017,
                Period = 43,
                DateFrom = new DateTime(2017, 10, 22),
                DateTo = new DateTime(2017, 10, 28),
                Status =  "Approaved by Tier 1",
                Hours =  45.50,
                DeadLine =  new DateTime(2017,10,20),
                Day2 = new TimeSpan(9,5,0) 
        });


            result.Add(new TimeSheet()
            {
                Year = 2017,
                Period = 43,
                DateFrom = new DateTime(2017, 10, 15),
                DateTo = new DateTime(2017, 10, 21),
                Status = "Approaved by Tier 1",
                Hours = 45.50,
                DeadLine = new DateTime(2017, 10, 20),
                Day3 = new TimeSpan(9, 5, 0)
            });

            return new JsonResult { Data = result };
        }

    }
}