using JwtProjectClient.Builders.Abstract;
using JwtProjectClient.Models;

namespace JwtProjectClient.Builders.Concrete
{
    public class SingleRoleStatusBuilder : StatusBuilder
    {
        public override Status GenerateStatus(AppUser activeUser, string roles)
        {
            Status status = new Status();
            if (activeUser.Roles.Contains(roles))
            {
                status.AccessStatus = true;
                
            }

            return status;
        }
    }
}