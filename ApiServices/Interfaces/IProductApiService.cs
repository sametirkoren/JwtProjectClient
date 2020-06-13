using System.Collections.Generic;
using System.Threading.Tasks;
using JwtProjectClient.Models;

namespace JwtProjectClient.ApiServices.Interfaces
{
    public interface IProductApiService
    {
         Task<List<ProductList>> GetAllAsync();
         Task AddAsync (ProductAdd productAdd);
    }
}