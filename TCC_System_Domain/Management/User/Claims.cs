using System.Collections.Generic;
using TCC_System_Domain.Core;

namespace TCC_System_Domain.Management
{
    public class Claims : Entity, IAggregateRoot
    {
        public int ClaimID { get; private set; }
        public string NomeClaim { get; private set; }
        private readonly List<UserClaims> _claims;
        public IReadOnlyCollection<UserClaims> ClaimUsers => _claims;
        protected Claims()
        {
            _claims = new List<UserClaims>();
        }

        public Claims(string nomeclaim)
        {
            NomeClaim = EntityValidation.SetStringProperty(nomeclaim, "Nome da Claim");
        }

    }
}
