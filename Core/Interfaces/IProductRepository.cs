using System.Threading.Tasks;
using Core.Entities;
using System.Collections.Generic;

/* 
    Repository pattern is another layer of abstraction on top of the DbContext that
    decouples business code from data access. A repository instance is typically used
    in a controller class rather than using the DbContext directly in the controller.
    Repository pattern allows for separation of concerns between DbContext and Controllers. 
    
    Repository pattern also used to reduce duplicate query logic and is makes it easier to
    dest database logic through mocks.
*/

namespace Core.Interfaces
{

    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);

        // Task<IReadyOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}