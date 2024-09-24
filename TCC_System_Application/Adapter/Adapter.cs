using System.Linq;
using System.Threading.Tasks;
using TCC_System_Application.ArduinoService;
using TCC_System_Application.ManagementServices;
using TCC_System_Domain.Arduino;
using TCC_System_Domain.Core.Auth.JsonObjects;
using TCC_System_Domain.Management;

namespace TCC_System_Application
{
    public static class Adapter
    {
        public static User ToUser(UserViewModel view)
        {
            return new User(view.Name, view.Email,view.Password, Languages.Br);
        }
        public static UserViewModel ToUserViewModel(UserJson user)
        {
            return new UserViewModel
            {
                Email = user.Email,
                Name = user.Nome,
                Id = user.Id
            };
        }
        public static Product ToProduct(ProductViewModel view)
        {
            return new Product(view.UserId, view.Name);
        }
        public static ModuleViewModel ToModuleVM(Module obj) {


            return new ModuleViewModel
            {
                Id = obj.Id,
                 value = obj.Value,
                 Type = obj.Type.ToString()           
            };
        
        }


        public static async Task<ProductViewModel> ToProductVM(Product obj)
        {
            return new ProductViewModel
            {
                Id = obj.Id,
                Name = obj.Name,
                UserId = obj.UserId,
                Modules = obj.ProductModeles.Select(x => ToModuleVM(x)).ToList(),
            }; ;
        }


    }
}
