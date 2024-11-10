using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC_System_Domain.Arduino.Repositories;

namespace TCC_System_Application.ArduinoService
{
    public interface IProductQueryService
    {
        Task<ModuleViewModel> GetModelById(Guid id);
        ProductViewModel GetProductModel(Guid id);
        ProductViewModel GetProductModel(int id);
        Task<IEnumerable<PostVM>> GetAllPosts();
        Task<PostVM> GetPostbyId(Guid id);

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
        public ProductViewModel GetProductModel(int id)
        {
            // Consulta SQL para obter o produto e os módulos associados
            string sql = CreateSQLQueryProduct() + " WHERE Nid = @id";

            using (var connection = _repository.GetDbConnection())
            {
                var mult = connection.QueryMultiple(sql, new { id });

                // Cria a instância do ProductViewModel
                ProductViewModel product = mult.ReadFirstOrDefault<ProductViewModel>();

                if (product == null)
                {
                    return null; // ou uma nova instância de ProductViewModel, dependendo da sua lógica
                }

                return product;
            }

        }

        public async Task<IEnumerable<PostVM>> GetAllPosts()
        {
            string sql = CreateSQLPost();

            return await _repository.GetDbConnection().QueryAsync<PostVM>(sql);
        }
        
        public async Task<PostVM> GetPostbyId(Guid id)
        {
            string sql = CreateSQLPost()+ " WHERE id = @id";
            var a = await _repository.GetDbConnection().QueryAsync<PostVM>(sql, new { id });
            return a.FirstOrDefault();
        }


        private string CreateSQLPost()
        {
            return @"SELECT * FROM [BLOG_POST] ";
        }

        private string CreateSQLQueryModules()
        {
            return @"SELECT *, [image] as imageBytes ,[value] as Name, [Id] as ModuleId, [ProductId] as ProjectId, Active FROM Ardu_Modulo ";
        }
        private string CreateSQLQueryProduct()
        {
            return @"Select * FROM Produto ";
        }


    }
}
