using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_System_Application.ManagementServices;
using TCC_System_Data;
using TCC_System_Domain.Arduino.Repositories;

namespace TCC_System_Application.ArduinoService.Query
{
    public interface IProductQueryService
    {
        Task<ModuleViewModel> GetModelById(Guid id);
        ProductViewModel GetProductModel(Guid id);
        ProductViewModel Geteste(Guid id);

    }

    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductRepository _repository;


        public ProductQueryService(IProductRepository repository)
        {
            _repository = repository;
        }


        public async Task<ModuleViewModel> GetModelById(Guid id)
        {
            string sql = CreateSQLQueryModules() + " WHERE id = @id";

            return await _repository.GetDbConnection().QueryFirstOrDefaultAsync<ModuleViewModel>(sql, new { id });
        }
        public IEnumerable<ModuleViewModel> GetModelByIdProduct(Guid id)
        {
            string sql = CreateSQLQueryModules() + " WHERE ProductId = @id";

            return _repository.GetDbConnection().Query<ModuleViewModel>(sql, new { id });
        }
        public ProductViewModel GetProductModel(Guid id)
        {
            string sql = CreateSQLQueryProduct() + " WHERE id = @id";

            var project = _repository.GetDbConnection().QueryFirstOrDefault<ProductViewModel>(sql, new {id});

            IEnumerable<ModuleViewModel> modules = GetModelByIdProduct(id);

            project.Modules = project.Modules ?? new List<ModuleViewModel>();

            foreach (var item in modules)
            {
                project.Modules.Add(new ModuleViewModel
                {
                    ModuleId = item.ModuleId,
                    ProjectId = item.ProjectId,
                    Type = item.Type,
                    value = item.value
                });
            }

            return project;
        }
        public ProductViewModel Geteste(Guid id)
        {
            // Consulta SQL para obter o produto e os módulos associados
            string sql = CreateSQLQueryProduct() + " WHERE id = @id; " +
                         CreateSQLQueryModules() + " WHERE ProductId = @id;";

            using (var connection = _repository.GetDbConnection())
            {
                var mult = connection.QueryMultiple(sql, new { id });

                // Cria a instância do ProductViewModel
                ProductViewModel product = mult.ReadFirstOrDefault<ProductViewModel>();

                if (product == null)
                {
                    return null; // ou uma nova instância de ProductViewModel, dependendo da sua lógica
                }

                // Lê os módulos associados ao produto
                var modules = mult.Read<ModuleViewModel>().ToList();

                // Inicializa a lista de módulos
                product.Modules = modules;

                return product;
            }
        }




        private string CreateSQLQueryModules()
        {
            return @"SELECT *, value as Name, [Id] as ModuleId, [ProductId] as ProjectId  FROM Ardu_Modulo ";
        }
        private string CreateSQLQueryProduct()
        {
            return @"Select * FROM Produto ";
        }


    }
}
