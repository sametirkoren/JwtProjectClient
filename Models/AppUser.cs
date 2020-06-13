using System.Collections.Generic;

namespace JwtProjectClient.Models
{
    public class AppUser
    {
        public string FullName{get;set;}

        public string UserName{get;set;}

        public List<string> Roles {get;set;}
    }
}