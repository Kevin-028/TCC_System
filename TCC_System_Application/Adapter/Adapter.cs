﻿using TCC_System_Application.ManagementServices;
using TCC_System_Domain.Management;

namespace TCC_System_Application
{
    public static class Adapter
    {




        public static User ToUser(UserViewModel view)
        {
            return new User(view.Login, view.GroupName, view.Name, view.Email, Languages.Br);
        }

    }
}
