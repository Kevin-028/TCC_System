using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC_System_Application.ManagementServices.Query;
using TCC_System_Domain.Arduino;
using TCC_System_Domain.Arduino.Repositories;
using TCC_System_Domain.Core;

namespace TCC_System_Application.ArduinoService
{
    public interface IProductCommandService
    {
        Task Insert(ProductViewModel view, string user);
        Task Update(ProductViewModel view, string user);
        Task Delete(Guid id);
        Task<List<ProductViewModel>> GetProductByLogin(string loginId);

        Task InsertModule(ModuleViewModel view, string user);
    }

    public class ProductCommandService : ApplicationService, IProductCommandService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserQueryService _userQueryService;

        public ProductCommandService( IUnitOfWork unitOfWork
            , IProductRepository repository
            , IUserQueryService userQuery )
        : base(unitOfWork)
        {
            _productRepository = repository;
            _userQueryService = userQuery;
        }

        public async Task Insert(ProductViewModel view, string user) 
        {
            view.UserId = _userQueryService.GetUsersByLogin(user).Id;

            Product obj = Adapter.ToProduct(view);

            obj.SetId();
            
            _productRepository.Insert(obj);

            Commit(user);         
        }
        public async Task InsertModule(ModuleViewModel view, string user)
        {
            Product obj = _productRepository.GetProductModules(view.ProjectId);

            obj.AddModule(Adapter.ToModule(view));

            _productRepository.Update(obj);

            if (!Commit(user))
            {
                AssertionConcern.AssertNotification("Erro no cadastro no novo modulo");
            }
        }


        public async Task Update(ProductViewModel view, string user)
        {
            Product obj = _productRepository.FindByID(view.Id);

            obj.SetName(view.Name);

            _productRepository.Update(obj);

            Commit(user);
        }

        public async Task Delete(Guid id)
        {
            Product obj = _productRepository.FindByID(id);
            
            _productRepository.Delete(obj);

            Commit();
        }

        public async Task<List<ProductViewModel>> GetProductByLogin(string loginId)
        {

            var obj = await _productRepository.GetProductByLogin(_userQueryService.GetUsersByLogin(loginId).Id);

            var list = await Task.WhenAll(obj.Select(p => Adapter.ToProductVM(p)));

            return list.ToList();
        }
    }
}
