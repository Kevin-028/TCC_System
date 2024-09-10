using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TCC_System_Application.ManagementServices;
using TCC_System_Application.ManagementServices.Query;
using TCC_System_Domain.Core;
using TCC_System_MVC.Core;

namespace TCC_System_MVC.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public IHandler<DomainNotification> Notifications;


        public BaseController()
        {
            Notifications = DomainEvent.Container.GetInstance<IHandler<DomainNotification>>();
        }

        public JsonResult ModelStateNotification()
        {
            var validationErrors = ModelState.Values.Select(x => x.Errors);
            List<string> errorMessages = new List<string>();

            validationErrors.ToList().ForEach(ve =>
            {
                var errorStrings = ve.Select(x => x.ErrorMessage);
                errorStrings.ToList().ForEach(em =>
                {
                    errorMessages.Add(em);
                });
            });

            return new JsonResult
            {
                Data = new { ErrorList = errorMessages, Status = "ERROR" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult JsonNotification()
        {
            if (!Notifications.HasNotifications())
            {
                return new JsonResult
                {
                    Data = new { Msg = "Sucesso", Status = "SUCCESS" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            var errors = new List<string>();
            Notifications.GetValues().ForEach(x => errors.Add(x.Value));

            return new JsonResult
            {
                Data = new { ErrorList = errors, Status = "ERROR" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public static string RenderRazorViewToString(ControllerContext controllerContext, string viewName, object model)
        {
            controllerContext.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        [ChildActionOnly]
        public ContentResult UserName()
        {

            try
            {
                var a = CookieManager.GetUserJsonByToken("TCC_System");

                var UserName = a.Nome;

                return Content(UserName);
            }
            catch (System.Exception)
            {
                return Content("Visitor");
            }
        }

        public void Cookie(IUserQueryService _userQueryService, UserViewModel view)
        {

            var usuario = _userQueryService.ObterUserEAcessosPorLogin(view.Email);

            var tokenCookie = CookieManager.GenerateTokenCookie(usuario);

            HttpContext.Response.Cookies.Set(tokenCookie);

        }
        public void CookieRemove()
        {
            HttpContext.Response.Cookies.Set(CookieManager.RemoveCookie());
        }


        public ActionResult SetLanguage(string language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            HttpCookie cookie = new HttpCookie("Languages");
            cookie.Value = language;
            Response.Cookies.Add(cookie);

            return Redirect(Request.UrlReferrer.ToString());
        }

       
    }
}