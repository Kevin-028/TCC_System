using SimpleInjector;
using TCC_System_Application.ManagementServices.Query;
using TCC_System_Application.ManagementServices;
using TCC_System_Data;
using TCC_System_Data.UnitOfWorks;
using TCC_System_Domain.Core;
using TCC_System_Domain.Management;
using TCC_System_Domain.Arduino.Repositories;
using TCC_System_Application.ArduinoService;

namespace TCC_System_Application.IoC
{
    public class BootstrapperTCC
    {
        public static void Register(Container container)
        {
            //Repositories
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IProductRepository, ArduinoRepository>(Lifestyle.Scoped);

            // Application Services
            container.Register<IUserCommandService, UserCommandService>(Lifestyle.Scoped);
            container.Register<IUserQueryService, UserQueryService>(Lifestyle.Scoped);

            container.Register<IProductCommandService, ProductCommandService>(Lifestyle.Scoped);
            container.Register<IProductQueryService, ProductQueryService>(Lifestyle.Scoped);

            container.Register<IMessageRepository, MessageRepository>(Lifestyle.Scoped);


            // Domain Services
            container.Register<IHandler<DomainNotification>, DomainNotificationHandler>(Lifestyle.Scoped);

            // Events

            // UnitOfWork
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);

            // Context
            container.Register<TCC_Context>(Lifestyle.Scoped);
        }
    }
}
