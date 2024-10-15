using System;
using System.Threading.Tasks;
using TCC_System_Domain.Arduino;
using TCC_System_Domain.Arduino.Repositories;
using TCC_System_Domain.Core;

namespace TCC_System_Application.Mensageria
{
    public interface IMessageCommandService
    {
        Task<Guid> Insert(MessageVM view, string user);
        Task<MessageVM> GetMessagebyAPI(Guid project);
        Task GetMessageStatus(Guid id);
        Task MessageOff(Guid id);

    }

    public class MessageCommandService : ApplicationService, IMessageCommandService
    {
        private readonly IMessageRepository _repository;

        public MessageCommandService(IUnitOfWork unitOfWork, IMessageRepository repository) 
            : base(unitOfWork)
        {
            _repository = repository;
        }

        public async Task<Guid> Insert(MessageVM view, string user)
        {
           
            view.Id = Guid.NewGuid();
            MessageAction obj = Adapter.ToMessageAction(view);

            _repository.Insert(obj);

            if (Commit(user)){
                return view.Id;
            }
            else
            {
                return Guid.Empty;
            }
        }
        public async Task<MessageVM> GetMessagebyAPI(Guid project)
        {
            var obj = _repository.GetByProject(project);

            if (obj != null)
            {
                obj.SetAction(Code.Pego);

                _repository.Update(obj);
            }
            else
                return null;    

            if (Commit())
            {
                return await Adapter.ToMessageVM(obj);
            }
            else
                return null;
        }
        public async Task GetMessageStatus(Guid id)
        {
            var obj = _repository.GetByAPI(id);

            if (obj == null)
                AssertionConcern.AssertNotification("...");

        }
        public async Task MessageOff(Guid id)
        {
            var obj = _repository.FindByID(id);

            obj.SetActiveFalse();

            _repository.Update(obj);

            Commit("System");
        }

    }
}
