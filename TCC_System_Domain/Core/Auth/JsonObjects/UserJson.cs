using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCC_System_Domain.Core.Auth.JsonObjects
{
    public class UserJson
    {
        // Essas propriedades precisam estar abertas, não podem ser private devido a renderização do Token
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        private string[] _claims;

        public UserJson()
        {
        }

        public UserJson(string login, string nome, string email)
        {
            Login = login;
            Nome = nome;
            Email = email;
        }

        public string Claims
        {
            get
            {
                return string.Join(",", _claims);
            }

            set
            {
                if (value != null)
                {
                    _claims = value.Split(',');
                }
            }
        }
        public void SetListClaims(List<string> claims)
        {
            _claims = claims.ToArray();
        }
        public bool AccessAvaliadorSite()
        {
            List<string> acessos = new List<string>()
            {
                "ADM",
                "ALTA DIRECAO",
                "AVALIADOR",
                "VERIFICADOR",
                "ALTA DIRECAO",
                "ADM"
            };

            if (acessos.Any(access => _claims.Contains(access)))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UsuarioPossuiClaim(string claim)
        {
            claim = claim.Trim();
            if (_claims.FirstOrDefault(x => x.Equals(claim)) == null)
                return false;

            return true;
        }

        public bool PossuiAcessos()
        {
            if (_claims.Length == 0)
                return false;

            return true;
        }
    }
}
