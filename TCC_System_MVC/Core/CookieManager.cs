using System;
using System.Web;
using TCC_System_Domain.Core.Auth.JsonObjects;
using TCC_System_Domain.Core;

namespace TCC_System_MVC.Core
{
    public class CookieManager
    {
        public static HttpCookie GenerateTokenCookie(UserJson user)
        {
            var token = TokenManager.GenerateToken(user);

            // Criação do Cookie para envio ao navegador.
            HttpCookie cookie = new HttpCookie(TokenManager.GetTokenKey())
            {
                Value = TokenManager.CompressString(token)
                //Expires = DateTime.Now.AddHours(4)
            };

            //Set Token Expiration
            TimeSpan somaTempo = new TimeSpan(0, 0, 30, 0);
            cookie.Expires = DateTime.Now + somaTempo;

            return cookie;
        }

        public static UserJson GetUserJsonByToken(string sistema)
        {
            if (HttpContext.Current.Request.Cookies[sistema] != null)
            {
                return TokenManager.GetUserJsonByToken(HttpContext.Current.Request.Cookies[sistema].Value);
            }

            return null;
        }
    }
}