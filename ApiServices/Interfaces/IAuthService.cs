using System.Threading.Tasks;
using JwtProjectClient.Models;

namespace JwtProjectClient.ApiServices.Interfaces
{
    public interface IAuthService
    {
         Task<bool> Login(AppUserLogin appUserLogin);
         void LogOut();
         
    }
}