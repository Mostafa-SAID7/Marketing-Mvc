using newApp.Data;
using newApp.Models;
using newApp.Models.entity;

namespace newApp.Services
{
    public interface IProductServ
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<PaginatedResult<Product>> GetProductsAsync(ProductSearchRequest request);
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<Guid> CreateProductAsync(string name, decimal price);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
    }
}
