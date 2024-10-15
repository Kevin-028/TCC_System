using System;
using System.Threading.Tasks;
using TCC_System_Domain.Arduino;
using TCC_System_Domain.Arduino.Repositories;
using TCC_System_Domain.Core;

namespace TCC_System_Application.Mensageria
{
    public interface IMessageCommandService
    {
        Task Insert(MessageVM view);

    }

    public class MessageCommandService : ApplicationService, IMessageCommandService
    {
        private readonly IMessageRepository _repository;

        public MessageCommandService(IUnitOfWork unitOfWork, IMessageRepository repository) 
            : base(unitOfWork)
        {
            _repository = repository;
        }

        public async Task Insert(MessageVM view)
        {
           
            view.Id = Guid.NewGuid();
            MessageAction obj = Adapter.ToMessageAction(view);

            _repository.Insert(obj);

            Commit();
        }

    }
}
