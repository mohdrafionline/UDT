using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class PositionsController: Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            Position model = new Position();
            return View("Index", model);
            //return View();
        }

        public ActionResult GetPositions(Position position)
        {
            using (var db = new DBEntity())
            {              
                var result = db.Positions.OrderBy(a => a.PositionName).ToList();
                return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            using (var db = new DBEntity())
            {
                var v = db.Positions.Where(a => a.PositionId == id).FirstOrDefault();
                return View(v);
            }
        }

        //[HttpPost]
        //[MultiButton(MatchFormKey = "action", MatchFormValue = "Save")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(Position position)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var db = new DBEntity())
                {
                    if (position.PositionId > 0)
                    {
                        //Edit
                        var v = db.Positions.Where(a => a.PositionId == position.PositionId).FirstOrDefault();
                        if (v != null)
                        {
                            v.PositionName = position.PositionName;                          
                        }
                    }
                    else
                    {
                        //Save
                        db.Positions.Add(position);
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
                var v = db.Positions.Where(a => a.PositionId == id).FirstOrDefault();
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
        public ActionResult DeletePosition(int id)
        {
            bool status = false;
            using (var db = new DBEntity())
            {
                var v = db.Positions.Where(a => a.PositionId == id).FirstOrDefault();
                if (v != null)
                {
                    db.Positions.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}