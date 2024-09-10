using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC_System_MVC.Core;

namespace TCC_System_MVC.Controllers
{
    public class HomeController : BaseController
    {
        [ClaimsAuthorize(Claims = "PADRAO")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}