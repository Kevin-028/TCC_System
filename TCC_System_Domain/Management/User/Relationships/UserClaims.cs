using TCC_System_Domain.Core;

namespace TCC_System_Domain.Management
{
    public class UserClaims : Entity, IAggregateRoot
    {
        public int UsuarioID { get; private set; }
        public int ClaimID { get; private set; }
        public User User { get; set; }
        public Claims Claims { get; set; }

        protected UserClaims() { }
        public UserClaims(int claimId)
        {
            SetClaim(claimId);
        }

        public void SetClaim(int claim)
        {
            ClaimID = claim;
        }

        public void SetUser(int user)
        {
            UsuarioID = user;
        }
    }
}
