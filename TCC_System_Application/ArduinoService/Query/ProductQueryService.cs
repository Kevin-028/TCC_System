using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCC_System_Domain.Arduino.Repositories;

namespace TCC_System_Application.ArduinoService.Query
{
    public interface IProductQueryService
    {

    }

    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductRepository _repository;

        public ProductQueryService(IProductRepository repository)
        {
            _repository = repository;
        }



    }
}
