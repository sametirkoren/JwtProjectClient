using JwtProjectClient.Builders.Concrete;
using JwtProjectClient.Models;

namespace JwtProjectClient.Builders.Abstract
{
    public abstract class StatusBuilder
    {
        public abstract Status GenerateStatus(AppUser activeUser , string roles);
    }
}