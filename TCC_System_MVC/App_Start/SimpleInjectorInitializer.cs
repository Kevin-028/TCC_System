using TCC_System_Application.IoC;
using TCC_System_Domain.Core;
using TCC_System_MVC.App_Start;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;
using WebActivatorEx;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;

[assembly: PostApplicationStartMethod(typeof(SimpleInjectorInitializer), "Initialize")]
namespace TCC_System_MVC.App_Start
{
    public static class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            DomainEvent.Container = new DomainEventsContainer(DependencyResolver.Current);



            // Cria e configura o contêiner para API
            var apiContainer = new Container();
            apiContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            apiContainer.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            DomainEvent.Container = new DomainEventsContainer(DependencyResolver.Current);

            InitializeContainer(apiContainer);
            apiContainer.Verify();

            // Integra o contêiner com a Web API
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(apiContainer);

        }

        private static void InitializeContainer(Container container)
        {
            BootstrapperTCC.Register(container);

        }
    }
}