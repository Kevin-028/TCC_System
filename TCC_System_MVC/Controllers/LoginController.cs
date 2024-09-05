using System.Web.Mvc;

namespace TCC_System_MVC.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        
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
    }
}