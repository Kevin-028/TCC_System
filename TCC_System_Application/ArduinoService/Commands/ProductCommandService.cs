using System;
using System.Threading.Tasks;
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
    }

    public class ProductCommandService : ApplicationService, IProductCommandService
    {
        private readonly IProductRepository _productRepository;

        public ProductCommandService(IUnitOfWork unitOfWork, IProductRepository repository): base(unitOfWork)
        {
            _productRepository = repository;
        }

        public async Task Insert(ProductViewModel view, string user) 
        { 
            Product obj = Adapter.ToProduct(view);

            obj.SetId();

            _productRepository.Insert(obj);

            Commit(user);         
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
    }
}
