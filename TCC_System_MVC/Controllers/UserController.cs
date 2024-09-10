using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC_System_Application.ManagementServices.Query;
using TCC_System_Application.ManagementServices;
using TCC_System_MVC.Core;

namespace TCC_System_MVC.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserCommandService _userCommandService;
        private readonly IUserQueryService _userQueryService;

        public UserController(IUserCommandService userCommandService, IUserQueryService userQueryService)
        {
            _userCommandService = userCommandService;
            _userQueryService = userQueryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var a = CookieManager.GetUserJsonByToken("TCC_System");

            UserViewModel user = _userQueryService.GetForLogin(a.Email);

            @ViewBag.Claims = new SelectList(new List<int> { 1 }, 1);

            return View(user);
        }
        [HttpPut]
        public JsonResult User(UserViewModel view)
        {
            Cookie(_userQueryService, view);

            _userCommandService.Update(view, view.Name);

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

        [HttpPut]
        public JsonResult UserPW(UserViewModel view)
        {
            _userCommandService.PutPassWord(view);

            var results = JsonNotification();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

    }
}