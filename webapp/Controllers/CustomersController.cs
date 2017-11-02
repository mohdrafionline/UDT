using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        [AllowAnonymous]
        public ActionResult Customers(string returnUrl)
        {
            return View();
        }

    }
}