using Dapper;
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
            TimeViewModel model = new TimeViewModel();
            model.TimeDetail = new TimeDetail();
            model.TimeHeader = new TimeHeader();
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


        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveTime(TimeViewModel model)
        {
            bool status = false;

            using (var db = new DBEntity())
            {
                if (model.TimeHeader.UserID != null && model.TimeHeader.TimeDate != null)
                {

                    DateTime dt = model.TimeHeader.TimeDate.AddDays(model.DayNo);
                    if (dt != null)
                    {
                        model.TimeHeader.TimeDate = dt;
                        var x = db.TimeHeaders.Where(a => a.UserID == model.TimeHeader.UserID && a.TimeDate == model.TimeHeader.TimeDate).FirstOrDefault();
                        if (x != null)
                        {
                            var v = db.TimeDetails.Where(a => a.TimeDetailID == x.TimeDetailID).FirstOrDefault();
                            if (v != null)
                            {
                                v.TimeIn = model.TimeDetail.TimeIn;
                                v.TimeOut = model.TimeDetail.TimeOut;
                                v.TimeDeduct = model.TimeDetail.TimeDeduct;
                                v.WorkTypeID = model.TimeDetail.WorkTypeID;
                                v.BillableID = model.TimeDetail.BillableID;
                                v.Notes = model.TimeDetail.Notes;
                                x.Aggrement = model.TimeHeader.Aggrement;
                                x.AggrementTypeID = model.TimeHeader.AggrementTypeID;
                                x.CustomerID = model.TimeHeader.CustomerID;
                                x.DivisonID = model.TimeHeader.DivisonID;
                                x.Overnight = model.TimeHeader.Overnight;
                                x.TimeDate = model.TimeHeader.TimeDate;
                                x.UserID = model.TimeHeader.UserID;
                                x.WorkRoleID = model.TimeHeader.WorkRoleID;
                            }
                        }
                        else
                        {
                            db.TimeDetails.Add(model.TimeDetail);
                            db.SaveChanges();
                            model.TimeHeader.TimeDetailID = model.TimeDetail.TimeDetailID;
                            db.TimeHeaders.Add(model.TimeHeader);
                        }
                        db.SaveChanges();
                    }

                }
                else if (model.TimeDetail.TimeDetailID > 0 && model.TimeHeader.TimeHeaderID > 0)
                {
                    //Edit
                    var v = db.TimeDetails.Where(a => a.TimeDetailID == model.TimeDetail.TimeDetailID).FirstOrDefault();
                    var x = db.TimeHeaders.Where(a => a.TimeHeaderID == model.TimeHeader.TimeHeaderID).FirstOrDefault();
                    if (v != null)
                    {
                        v.TimeIn = model.TimeDetail.TimeIn;
                        v.TimeOut = model.TimeDetail.TimeOut;
                        v.TimeDeduct = model.TimeDetail.TimeDeduct;
                        v.WorkTypeID = model.TimeDetail.WorkTypeID;
                        v.BillableID = model.TimeDetail.BillableID;
                        v.Notes = model.TimeDetail.Notes;
                        x.Aggrement = model.TimeHeader.Aggrement;
                        x.AggrementTypeID = model.TimeHeader.AggrementTypeID;
                        x.CustomerID = model.TimeHeader.CustomerID;
                        x.DivisonID = model.TimeHeader.DivisonID;
                        x.Overnight = model.TimeHeader.Overnight;
                        x.TimeDate = model.TimeHeader.TimeDate;
                        x.TimeDetailID = model.TimeHeader.TimeDetailID;
                        x.TimeHeaderID = model.TimeHeader.TimeHeaderID;
                        x.UserID = model.TimeHeader.UserID;
                        x.WorkRoleID = model.TimeHeader.WorkRoleID;
                    }
                    else
                    {
                        //Save
                        db.TimeDetails.Add(model.TimeDetail);
                        db.SaveChanges();
                        model.TimeHeader.TimeDetailID = model.TimeDetail.TimeDetailID;
                        db.TimeHeaders.Add(model.TimeHeader);
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                    status = true;
                }
                else
                {
                    //Save
                    db.TimeDetails.Add(model.TimeDetail);
                    db.SaveChanges();
                    model.TimeHeader.TimeDetailID = model.TimeDetail.TimeDetailID;
                    db.TimeHeaders.Add(model.TimeHeader);
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
                var timeDetails = db.Database.Connection.Query<TimeDetail>("select * from TimeDetails");

                var weekStartDates = GetWeeklyDates(UserID);// db.TimeHeaders.Where(x => x.UserID == UserID).GroupBy(i => Extensions.StartOfWeek(i.TimeDate,DayOfWeek.Sunday)).Select(g=> new { dt = g.Key});


                foreach (var weekStartDate in weekStartDates)
                {
                    var weekDate = DateTime.Now;

                    var nextDate = weekStartDate.AddDays(7);
                    var timeheaders = db.TimeHeaders.Where(x => DateTime.Compare(x.TimeDate, weekStartDate) >= 0 && DateTime.Compare(x.TimeDate, nextDate) <= 0);

                    var timesheetObj = new TimeSheet();
                    var timeDetail = new List<TimeDetail>();

                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(timeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.UserID = UserID;
                    timesheetObj.Year = weekStartDate.Year;
                    timesheetObj.Period = GetWeekNumber(weekStartDate);
                    timesheetObj.DateFrom = weekStartDate;
                    timesheetObj.DateFromJ = timesheetObj.DateFrom.ToShortDateString();
                    timesheetObj.DateTo = weekStartDate.AddDays(7);
                    timesheetObj.DateToJ = timesheetObj.DateTo.ToShortDateString();
                    timesheetObj.Status = ""; // This needs to be added
                    timesheetObj.Hours = GetTotalTime(timeDetail);
                    timesheetObj.HoursJ = timesheetObj.Hours.ToString();
                    timesheetObj.DeadLine = DateTime.Now;  // need to ask
                    timesheetObj.DeadLineJ = timesheetObj.DeadLine.ToShortDateString();


                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekStartDate);
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(timeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Day1 = GetTotalTime(timeDetail);
                    timesheetObj.Day1J = timesheetObj.Day1.ToString();
                    weekDate = weekStartDate.AddDays(1);

                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekDate);


                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(timeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Day2 = GetTotalTime(timeDetail);
                    timesheetObj.Day2J = timesheetObj.Day2.ToString();
                    weekDate = weekStartDate.AddDays(2);

                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekDate);
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(timeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Day3 = GetTotalTime(timeDetail);
                    timesheetObj.Day3J = timesheetObj.Day3.ToString();
                    weekDate = weekStartDate.AddDays(3);
                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekDate);
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(timeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Day4 = GetTotalTime(timeDetail);
                    timesheetObj.Day4J = timesheetObj.Day4.ToString();
                    weekDate = weekStartDate.AddDays(4);

                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekDate);
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(timeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Day5 = GetTotalTime(timeDetail);
                    timesheetObj.Day5J = timesheetObj.Day5.ToString();
                    weekDate = weekStartDate.AddDays(5);

                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekDate);
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(timeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }
                    timesheetObj.Day6 = GetTotalTime(timeDetail);
                    timesheetObj.Day6J = timesheetObj.Day6.ToString();
                    weekDate = weekStartDate.AddDays(6);

                    timeheaders = db.TimeHeaders.Where(x => x.TimeDate == weekDate);
                    timeDetail = new List<TimeDetail>();
                    foreach (var timeHeader in timeheaders)
                    {
                        timeDetail.AddRange(timeDetails.Where(x => x.TimeDetailID == timeHeader.TimeDetailID).ToList<TimeDetail>());
                    }

                    timesheetObj.Day7 = GetTotalTime(timeDetail);

                    timesheetObj.Day7J = timesheetObj.Day7.ToString();
                    result.Add(timesheetObj);
                }
            }

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetTimeDetails(string datetime)
        {
            DateTime dateTimeT;
            if (DateTime.TryParse(datetime, out dateTimeT))
            {
                using (var db = new DBEntity())
                {
                    var v = db.TimeHeaders.Where(e => e.TimeDate == dateTimeT).FirstOrDefault();
                    if (v != null)
                    {
                        var x = db.TimeDetails.Where(e => e.TimeDetailID == v.TimeDetailID).FirstOrDefault();
                        TimeDetailsResult res = new TimeDetailsResult()
                        {
                            TimeIN=x.TimeIn.ToString(),
                            TimeOut=x.TimeOut.ToString(),
                            Notes=x.Notes
                        };
                        return Json(res);
                    }
                    else
                        return Json("");
                }
            }
            else
                return Json("");
        }

        public int GetWeekNumber(DateTime dt)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            return weekNum;
        }

        private TimeSpan GetTotalTime(List<TimeDetail> timedetails)
        {

            var timespan = new TimeSpan();

            foreach (var timedetail in timedetails)
            {
                var time = timedetail.TimeOut - timedetail.TimeIn;
                timespan+=time;
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

        private List<DateTime> GetWeeklyDates(int UserID)
        {
            List<DateTime> dates = new List<DateTime>();

            using (var db = new DBEntity())
            {
                var weekStartDates = db.TimeHeaders.Where(x => x.UserID == UserID).Select(x => x.TimeDate).ToList<DateTime>();

                var maxDate = db.TimeHeaders.Max(x => x.TimeDate);
                var minDate = db.TimeHeaders.Min(x => x.TimeDate);

                if (minDate.DayOfWeek > 0)
                {
                    var diff = 0 - minDate.DayOfWeek;
                    minDate = minDate.AddDays(diff);
                }

                while (minDate <= maxDate)
                {
                    if (minDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        dates.Add(minDate);
                    }
                    minDate = minDate.AddDays(1);
                }
                //if (weekStartDates.Count > 0)
                //{
                //    if (weekStartDates[0].DayOfWeek > 0)
                //    {
                //        int days = 0 - weekStartDates[0].DayOfWeek;
                //        dates.Insert(0, weekStartDates[0].AddDays(days * -1));
                //    }
                //}
            }
            return dates;
        }
    }
}