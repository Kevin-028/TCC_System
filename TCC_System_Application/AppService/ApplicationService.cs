using TCC_System_Domain.Core;

namespace TCC_System_Application
{
    public class ApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IHandler<DomainNotification> Notifications;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Notifications = DomainEvent.Container.GetInstance<IHandler<DomainNotification>>();
        }

        public IHandler<DomainNotification> GetNotifications()
        {
            return Notifications;
        }

        public bool Commit()
        {
            if (Notifications.HasNotifications())
                return false;

            _unitOfWork.Commit();
            return true;
        }

        public bool Commit(string Usuario)
        {
            if (Notifications.HasNotifications())
                return false;

            _unitOfWork.Commit(Usuario);
            return true;
        }
    }

}
