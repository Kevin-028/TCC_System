using SimpleInjector.Integration.WebApi;
using SimpleInjector;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector.Lifestyles;
using TCC_System_Application.IoC;
using TCC_System_Domain.Core;
using TCC_System_MVC.App_Start;


namespace TCC_System_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Configurações padrão do ASP.NET MVC e Web API
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        private static void InitializeContainer(Container container)
        {
            BootstrapperTCC.Register(container);
        }
    }
}
