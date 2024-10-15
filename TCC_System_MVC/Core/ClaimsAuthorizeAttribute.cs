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
            Sistema = "TCC_System";

            var user = CookieManager.GetUserJsonByToken(Sistema);

            if (user != null)
            {
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