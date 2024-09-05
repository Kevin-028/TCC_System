using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_System_Domain.Core.Auth.JsonObjects
{
    public class DBJson
    {
        // Essas propriedades precisam estar abertas, não podem ser private devido a renderização do Token
        public string ConnectionID { get; set; }
        public string Server { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public DBJson()
        {
        }

        public DBJson(string connectionID, string server, string user, string password)
        {
            ConnectionID = connectionID;
            Server = server;
            User = user;
            Password = password;
        }

        public bool EhValido()
        {
            var valid = 0;

            if (!string.IsNullOrEmpty(ConnectionID))
            {
                ConnectionID = ConnectionID.ToUpper();
            }
            else
                valid += 1;

            valid += string.IsNullOrEmpty(Server) ? 1 : 0;
            valid += string.IsNullOrEmpty(User) ? 1 : 0;
            valid += string.IsNullOrEmpty(Password) ? 1 : 0;

            return valid == 0;
        }
    }
}
