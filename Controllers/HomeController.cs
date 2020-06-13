using System.Threading.Tasks;
using JwtProjectClient.ApiServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JwtProjectClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApiService _productApiService;

        public HomeController(IProductApiService productApiService)
        {
            _productApiService = productApiService;
        }

        public async Task<IActionResult> Index(){
            return View(await _productApiService.GetAllAsync());
        }
    }
}