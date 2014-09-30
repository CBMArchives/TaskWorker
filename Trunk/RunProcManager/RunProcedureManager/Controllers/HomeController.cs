using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RunProcedureManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TextEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TreeEdit()
        {
            return View();
        }
    }
}
