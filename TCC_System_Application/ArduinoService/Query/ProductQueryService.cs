using Dapper;
using System;
using System.Collections.Generic;
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
            var sql = CreateSQLQueryModules() + " where id = @id";

            return _repository.GetDbConnection().QuerySingleOrDefault<ModuleViewModel>(sql, new { id });
        }

        private string CreateSQLQueryModules()
        {
            return @"SELECT *, value as Name FROM Ardu_Modulo ";
        }

    }
}
