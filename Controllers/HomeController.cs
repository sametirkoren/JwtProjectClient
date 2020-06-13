using System.Threading.Tasks;
using JwtProjectClient.ApiServices.Interfaces;
using JwtProjectClient.Models;
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
        
        public  IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductAdd productAdd){
            if(ModelState.IsValid){
                await _productApiService.AddAsync(productAdd);
                return RedirectToAction("Index","Home");
            }
            return View(productAdd);
        }

        public async Task<IActionResult> Edit(int id){
           
            return View(await _productApiService.GetByIdAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductList productList){
           
            if(ModelState.IsValid){
                await _productApiService.UpdateAsync(productList);
                return RedirectToAction("Index","Home");
            }
            return View(productList);
            
        }

        public async Task<IActionResult> Delete(int id){
            await _productApiService.DeleteAsync(id);
            return RedirectToAction("Index","Home");
        }
    }
}