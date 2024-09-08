using System.Collections.Generic;
using System.Web.Mvc;
using TCC_System_Application.ManagementServices;
using TCC_System_Application.ManagementServices.Query;

namespace TCC_System_MVC.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUserCommandService _userCommandService;
        private readonly IUserQueryService _userQueryService;

        public LoginController(IUserCommandService userCommandService, IUserQueryService userQueryService)
        {
            _userCommandService = userCommandService;
            _userQueryService = userQueryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NewUser()
        {
            @ViewBag.Claims = new SelectList(new List<int>{ 1 }, 1);

            return View();
        }
        [HttpPost]
        public JsonResult Login(UserViewModel view)
        {
            _userCommandService.Login(view);
            
            var results = JsonNotification();

            if (results.Data.ToString().Contains("SUCCESS"))
            {
                Cookie(_userQueryService, view);
            }

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public JsonResult NewUser(UserViewModel view)
        {
            _userCommandService.Insert(view, "System");

            var results = JsonNotification();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        
    }
}