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
            return View();
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