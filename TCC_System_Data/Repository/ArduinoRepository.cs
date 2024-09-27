using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC_System_Domain.Arduino;
using TCC_System_Domain.Arduino.Repositories;

namespace TCC_System_Data
{
    public class ArduinoRepository: Repository<Product> , IProductRepository
    {
        public ArduinoRepository(TCC_Context context)
            : base(context)
        {

        }
       

        public Product GetProduct(Guid id)
        {
            return Context.Products.Find(id);
        }
        public Module GetModule(Guid id)
        { 
            return Context.Modules.Find(id);
        }
        public Product GetProductModules(Guid id)
        {
            return Context.Products
                .Include(x => x.ProductModeles)
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }
        public async Task<IEnumerable<Product>> GetProductByLogin(int loginId)
        {
            return Context.Products
                .Include(x => x.ProductModeles)
                .Where(x => x.UserId == loginId);
        }
    }
}
