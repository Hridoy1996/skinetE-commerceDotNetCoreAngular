using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace API.Interfaces
{
    public interface IProduct
    {
         Task<Product> GetProductByIdAsync(string id);
         Task<IReadOnlyList<Product>> GetProductsAsync(string id);

    }
}