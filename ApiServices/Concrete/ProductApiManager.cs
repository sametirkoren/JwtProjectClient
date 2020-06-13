using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JwtProjectClient.ApiServices.Interfaces;
using JwtProjectClient.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace JwtProjectClient.ApiServices.Concrete
{
    public class ProductApiManager : IProductApiService
    {
        private readonly IHttpContextAccessor _accessor;
        public ProductApiManager(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public async Task AddAsync(ProductAdd productAdd)
        {
            var token = _accessor.HttpContext.Session.GetString("token");

            if(!string.IsNullOrWhiteSpace(token)){
                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
                var jsonData = JsonConvert.SerializeObject(productAdd);
                var stringContent = new StringContent(jsonData , Encoding.UTF8,"application/json");
                var responseMessage = await httpClient.PostAsync("http://localhost:54702/api/products",stringContent);
                
            }
        }

        public async Task<List<ProductList>> GetAllAsync()
        {
            var token = _accessor.HttpContext.Session.GetString("token");
            if(!string.IsNullOrWhiteSpace(token)){
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);

                var responseMessage  = await httpClient.GetAsync("http://localhost:54702/api/products");

                if(responseMessage.IsSuccessStatusCode){
                    return JsonConvert.DeserializeObject<List<ProductList>>( await responseMessage.Content.ReadAsStringAsync());
                }
            }
            return null;
        }
    }
}