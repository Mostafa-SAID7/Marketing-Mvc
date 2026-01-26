using newApp.Models;
using newApp.Models.entity;

namespace newApp.Repositoriers
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<PaginatedResult<Product>> GetProductsAsync(ProductSearchRequest request);
        Task<Product?> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
