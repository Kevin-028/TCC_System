using System;
using System.Collections.Generic;
using System.Text;
using TCC_System_Domain.Core;

namespace TCC_System_Domain.Arduino.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetProduct(Guid id);
        Module GetModule(Guid id);
        Product GetProductModules(Guid id);
    }
}
