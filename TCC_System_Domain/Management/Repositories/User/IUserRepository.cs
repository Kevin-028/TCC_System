using TCC_System_Domain.Core;

namespace TCC_System_Domain.Management
{
    public interface IUserRepository : IRepository<User>
    {
        User FindUserClaim(int id);
    }
}
