using System.Threading.Tasks;
using JwtProjectClient.ApiServices.Interfaces;
using JwtProjectClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtProjectClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult SignIn(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLogin appUserLogin){
            if(ModelState.IsValid){
                if(await _authService.Login(appUserLogin)){
                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError("","Kullanıcı adı veya şifre hatalı");
            }
            return View(appUserLogin);
        }
    }
}