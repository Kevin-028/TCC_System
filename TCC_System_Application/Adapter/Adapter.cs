using System;
using System.Linq;
using System.Threading.Tasks;
using TCC_System_Application.ArduinoService;
using TCC_System_Application.ManagementServices;
using TCC_System_Application.Mensageria;
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
        public static Product ToProduct(ProductViewModel view)
        {
            return new Product(view.UserId, view.Name);
        }
        public static Module ToModule(ModuleViewModel view)
        {
            return new Module((TypeModule)Enum.Parse(typeof(TypeModule), view.Type), view.value,view.ProjectId);
        }

        public static ModuleViewModel ToModuleVM(Module obj)
        {
            return new ModuleViewModel
            {
                 ModuleId = obj.Id,
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
        public static UserViewModel ToUserViewModel(UserJson user)
        {
            return new UserViewModel
            {
                Email = user.Email,
                Name = user.Nome,
                Id = user.Id
            };
        }

        public static MessageAction ToMessageAction(MessageVM view)
        {
            TypeModule type = (TypeModule)Enum.Parse(typeof(TypeModule), view.Type);
            Code code = (Code)Enum.Parse(typeof(Code), view.Action);

            MessageAction obj = new MessageAction(view.Id, type, code);

            return obj;
        }
    }
}
