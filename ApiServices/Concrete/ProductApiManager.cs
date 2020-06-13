using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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