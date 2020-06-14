using JwtProjectClient.Builders.Abstract;
using JwtProjectClient.Models;

namespace JwtProjectClient.Builders.Concrete
{
    public class MultiRoleStatusBuilder : StatusBuilder
    {
        public override Status GenerateStatus(AppUser activeUser, string roles)
        {
            Status status = new Status();
            var acceptedRoles = roles.Split(',');
            foreach (var role in acceptedRoles)
            {
                if (activeUser.Roles.Contains(role))
                {
                    status.AccessStatus = true;
                    break;
                }
            }
            return status;
        }
    }
}