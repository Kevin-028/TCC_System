using System.Collections.Generic;
using System.Linq;
using TCC_System_Domain.Core;

namespace TCC_System_Domain.Management
{
    public class User : Entity, IAggregateRoot
    {
        public int Id { get; private set; }
        public string GroupName { get; private set; }
        public string Login { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Languages Language { get; private set; }
        public bool Ativo { get; private set; }

        private readonly List<UserClaims> _userClaims;
        public IReadOnlyCollection<UserClaims> UserClaims => _userClaims;


        public User(string login, string groupName, string name, string email, Languages language)
        {
            _userClaims = new List<UserClaims>();
            SetLanguage(language);
            SetLogin(login);
            SetGroup(groupName);
            SetName(name);
            SetEmail(email);
            SetStatusAtivo();
        }
        public void SetGroup(string group) => GroupName = group;
        public void SetLogin(string user) => Login = user;
        public void SetName(string name) => Name = name;
        public void SetEmail(string email) => Email = EntityValidation.SetEmailProperty(email);
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
