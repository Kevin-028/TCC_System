using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TCC_System_MVC.Core
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public string Sistema { get; set; }
        public string Claims { get; set; }

        #region AuthorizeCore

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Sistema = "LLSToken";

            var user = CookieManager.GetUserJsonByToken(Sistema);

            if (user != null)
            {
#if !DEBUG
                var CheckLogin = HttpContext.Current.Request.LogonUserIdentity.Name;

                // Validação Usuário Logado no PC
                if (user.LoginCompleto.ToUpper() != CheckLogin.ToUpper())
                {
                    HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies[Sistema];
                    HttpContext.Current.Response.Cookies.Remove(Sistema);
                    currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                    currentUserCookie.Value = null;
                    HttpContext.Current.Response.SetCookie(currentUserCookie);

                    return false;
                }
#endif

                // Analisa Claims do Usuário
                if (!string.IsNullOrEmpty(Claims))
                {
                    if (!user.PossuiAcessos())
                        return false;

                    // Montagem das Claims permitidas
                    var _claimsPermitidas = Claims.Split(',');

                    // Validação do Acesso
                    foreach (string claim in _claimsPermitidas)
                    {
                        if (user.UsuarioPossuiClaim(claim))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        #endregion

        #region HandleUnauthorizedRequest

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        #endregion
    }
}