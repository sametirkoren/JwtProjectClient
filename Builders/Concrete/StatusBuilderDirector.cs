using JwtProjectClient.Builders.Abstract;
using JwtProjectClient.Models;

namespace JwtProjectClient.Builders.Concrete
{
    public class StatusBuilderDirector
    {
        private StatusBuilder builder ;
        public StatusBuilderDirector(StatusBuilder builder)
        {
            this.builder = builder;
        }

        public Status GenerateStatus(AppUser activeUser, string roles){
            return builder.GenerateStatus(activeUser , roles);
        }
    }
}