using System.Collections.Generic;
using System.Linq;
using TCC_System_Domain.Core;

namespace TCC_System_Domain.Management
{
    public class User : Entity, IAggregateRoot
    {
        public int Id { get; private set; }
        public string Login { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Languages Language { get; private set; }
        public bool Ativo { get; private set; }

        private readonly List<UserClaims> _userClaims;
        public IReadOnlyCollection<UserClaims> UserClaims => _userClaims;


        public User(string name, string email, string password, Languages language)
        {
            _userClaims = new List<UserClaims>();
            SetLanguage(language);
            SetName(name);
            SetEmail(email);
            SetPassWord(password);
            SetStatusAtivo();
            SetLogin();
        }
        public void SetPassWord(string passWord) => Password = passWord;
        public void SetName(string name) => Name = name;
        public void SetEmail(string email) => Email = EntityValidation.SetEmailProperty(email);
        public void SetLogin() => Login = this.Email;
        public void SetStatusAtivo() => Ativo = true;
        public void SetStatusInativo() => Ativo = false;
        public void SetLanguage(Languages language) => Language = language;

        public void AddClaimsList(List<int> claims)
        {
            claims.ForEach(x => AddClaims(x));
        }
        public void AddClaims(int claimId)
        {
            var claims = new UserClaims(claimId);
            _userClaims.Add(claims);

        }
        public void RemoveClaims(int claimId)
        {
            var claims = _userClaims.Where(x => x.ClaimID == claimId).SingleOrDefault();
            if (claims != null)
                _userClaims.Remove(claims);

        }
       
    }
}
