using market_mvc.Models;
using market_mvc.Models.entity;

namespace market_mvc.Repositoriers
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<PaginatedResult<Product>> GetProductsAsync(ProductSearchRequest request);
        Task<Product?> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        void Update(Product product);
        Task DeleteAsync(Guid id);
        void Delete(Product product);
    }
}
