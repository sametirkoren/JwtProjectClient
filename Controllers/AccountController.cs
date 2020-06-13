using Microsoft.AspNetCore.Mvc;

namespace JwtProjectClient.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn(){
            return View();
        }
    }
}