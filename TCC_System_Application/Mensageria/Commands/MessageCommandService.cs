using System;
using System.Threading.Tasks;
using TCC_System_Domain.Arduino;
using TCC_System_Domain.Arduino.Repositories;
using TCC_System_Domain.Core;

namespace TCC_System_Application.Mensageria
{
    public interface IMessageCommandService
    {
        Task Insert(MessageVM view, string user);
        Task<MessageVM> GetMessagebyAPI(Guid project);

    }

    public class MessageCommandService : ApplicationService, IMessageCommandService
    {
        private readonly IMessageRepository _repository;

        public MessageCommandService(IUnitOfWork unitOfWork, IMessageRepository repository) 
            : base(unitOfWork)
        {
            _repository = repository;
        }

        public async Task Insert(MessageVM view, string user)
        {
           
            view.Id = Guid.NewGuid();
            MessageAction obj = Adapter.ToMessageAction(view);

            _repository.Insert(obj);

            Commit(user);
        }
        public async Task<MessageVM> GetMessagebyAPI(Guid project)
        {
            var obj = _repository.GetByProject(project);

            if (obj != null)
            {
                obj.SetActiveFalse();

                _repository.Update(obj);
            }
            else
            {
                return null;
            }

            if (Commit())
            {
                return await Adapter.ToMessageVM(obj);
            }
            else { return null; }
        }

    }
}
