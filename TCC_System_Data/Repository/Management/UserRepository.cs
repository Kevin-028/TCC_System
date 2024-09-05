using TCC_System_Domain.Management;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TCC_System_Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TCC_Context contex)
            : base(contex)
        {
        }

        public User FindUserClaim(int id)
        {
            return Context.Users
                .Include(x => x.UserClaims)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }
    }
}
