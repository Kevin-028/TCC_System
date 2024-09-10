using TCC_System_Application.ManagementServices;
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
                Name = user.Nome
            };

        }

    }
}
