using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Athletes.Controllers
{
    public class SchoolController : Controller
    {
        // GET: School
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}