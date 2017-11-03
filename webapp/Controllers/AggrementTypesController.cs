using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class AggrementTypesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            AggrementType model = new AggrementType();
            return View("Index", model);
            //return View();
        }

        public ActionResult GetAggrementType(AggrementType aggrementType)
        {
            using (var db = new DBEntity())
            {
                //var employ = (from s in db.TblEmployees where s.FirstName == employees.FirstName select s).ToList();
                var employ = db.AggrementTypes.OrderBy(a => a.AggrementTypeName).ToList();

                return Json(new { data = employ }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            using (var db = new DBEntity())
            {
                var v = db.AggrementTypes.Where(a => a.AggrementTypeID == id).FirstOrDefault();
                return View(v);
            }
        }

        //[HttpPost]
        //[MultiButton(MatchFormKey = "action", MatchFormValue = "Save")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(AggrementType aggrementType)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var db = new DBEntity())
                {
                    if (aggrementType.AggrementTypeID > 0)
                    {
                        //Edit
                        var v = db.AggrementTypes.Where(a => a.AggrementTypeID == aggrementType.AggrementTypeID).FirstOrDefault();
                        if (v != null)
                        {
                            v.AggrementTypeName = aggrementType.AggrementTypeName;
                           
                        }
                    }
                    else
                    {
                        //Save
                        db.AggrementTypes.Add(aggrementType);
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
                var v = db.AggrementTypes.Where(a => a.AggrementTypeID == id).FirstOrDefault();
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
        public ActionResult DeleteAggrementType(int id)
        {
            bool status = false;
            using (var db = new DBEntity())
            {
                var v = db.AggrementTypes.Where(a => a.AggrementTypeID == id).FirstOrDefault();
                if (v != null)
                {
                    db.AggrementTypes.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}