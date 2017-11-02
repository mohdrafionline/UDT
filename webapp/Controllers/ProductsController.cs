using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Products()
        {
            return View();
        }
    }
}