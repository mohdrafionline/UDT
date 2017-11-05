using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                        
            using (var db = new DBEntity())
            {
                var weekStartDates = db.TimeHeaders.Where(x => x.UserID == UserID).GroupBy(i => i.TimeDate.StartOfWeek(DayOfWeek.Sunday)).Select(g=> new { dt = g.Key});

                foreach ( var weekStartDate in weekStartDates)
                {
                   var timeheaders =  db.TimeHeaders.Where(x => DateTime.Compare(x.TimeDate, weekStartDate.dt) >= 0 && DateTime.Compare(x.TimeDate, weekStartDate.dt.AddDays(7)) <= 0);
                    var timesheetObj = new TimeSheet();
                    var timeDetail = new List<TimeDetail>();

                    foreach (var timeHeader in timeheaders)
                    {
                           timeDetail.AddRange( db.TimeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Year = weekStartDate.dt.Year;
                    timesheetObj.Period = GetWeekNumber(weekStartDate.dt);
                    timesheetObj.DateFrom = weekStartDate.dt;
                    timesheetObj.DateTo = weekStartDate.dt.AddDays(7);
                    timesheetObj.Status = ""; // This needs to be added
                    timesheetObj.Hours = GetTotalTime(timeDetail);
                    timesheetObj.DeadLine = DateTime.Now;  // need to ask

                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekStartDate.dt);
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(db.TimeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }
                    
                    timesheetObj.Day1 = GetTotalTime(timeDetail);

                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekStartDate.dt.AddDays(1) );
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(db.TimeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }


                    timesheetObj.Day2 = GetTotalTime(timeDetail);

                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekStartDate.dt.AddDays(2));
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(db.TimeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Day3 = GetTotalTime(timeDetail);

                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekStartDate.dt.AddDays(3));
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(db.TimeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Day4 = GetTotalTime(timeDetail);


                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekStartDate.dt.AddDays(4));
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(db.TimeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Day5 = GetTotalTime(timeDetail);

                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekStartDate.dt.AddDays(5));
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(db.TimeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Day6 = GetTotalTime(timeDetail);

                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekStartDate.dt.AddDays(6));
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(db.TimeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Day7 = GetTotalTime(timeDetail);

                    result.Add(timesheetObj);
                }
            }
            return new JsonResult { Data = result };
        }

        public int GetWeekNumber(DateTime dt)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            return weekNum;
        }

        private TimeSpan GetTotalTime(List<TimeDetail> timedetails) {

            var timespan = new TimeSpan();

            foreach ( var timedetail in timedetails)
            {
                timespan.Add(timedetail.TimeOut.Subtract(timedetail.TimeIn));
            }           
            return timespan;
        }

        private TimeSpan GetTotalTime(List<TimeDetail> timedetails, DateTime dt)
        {

            var timespan = new TimeSpan();

            foreach (var timedetail in timedetails)
            {
                timespan.Add(timedetail.TimeOut.Subtract(timedetail.TimeIn));
            }
            return timespan;
        }

    }


    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }
    }

}