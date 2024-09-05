using TCC_System_Domain.Core;
using TCC_System_Domain.Management;

namespace TCC_System_Application.ManagementServices
{
    public interface IUserCommandService
    {
        void Insert(UserViewModel view, string user);
        void Update(UserViewModel view, string user);
        void Disable(int id, string user);
        void Active(int id, string user);
        void RemoveClaim(int idSite, int idMachine, string user);
    }

    public class UserCommandService : ApplicationService, IUserCommandService
    {
        private readonly IUserRepository Repository;

        public UserCommandService(IUnitOfWork unitOfWork, IUserRepository repository) : base(unitOfWork)
        {
            Repository = repository;
        }

        public void Insert(UserViewModel view, string user)
        {
            User obj = Adapter.ToUser(view);

            obj.AddClaimsList(view.Claims);

            Repository.Insert(obj);

            if (Commit(user)) { }
            else
            {
                AssertionConcern.AssertNotification("Erro no cadastro");
            }
        }
        public void Update(UserViewModel view, string user)
        {
            User obj = Repository.FindByID(view.Id);

            if (view.Claims != null)
                obj.AddClaimsList(view.Claims);

            Repository.Update(obj);

            if (Commit(user))
            {

            }
            else
            {
                AssertionConcern.AssertNotification("Erro na Atualização");
            }
        }
        public void Disable(int id, string user)
        {
            User obj = Repository.FindByID(id);

            obj.SetStatusInativo();

            Repository.Update(obj);
            if (Commit(user))
            {

            }
            else
            {
                AssertionConcern.AssertNotification("Não é possivel desativar");
            }
        }

        public void Active(int id, string user)
        {
            var obj = Repository.FindByID(id);

            obj.SetStatusAtivo();

            Repository.Update(obj);
            if (Commit(user))
            {

            }
            else
            {
                AssertionConcern.AssertNotification("Não é possivel Ativar");
            }
        }
        public void RemoveClaim(int UsuarioID, int ClaimID, string user)
        {
            var usuario = Repository.FindUserClaim(UsuarioID);

            if (usuario != null)
            {
                usuario.RemoveClaims(ClaimID);

                Commit(user);
            }
        }

    }
}
