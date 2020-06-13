using Microsoft.AspNetCore.Mvc;

namespace JwtProjectClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(){
            return View();
        }
    }
}