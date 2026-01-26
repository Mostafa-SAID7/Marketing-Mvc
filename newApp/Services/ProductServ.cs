using newApp.Data;
using newApp.Models;
using newApp.Models.entity;

namespace newApp.Services
{
    public class ProductServ : IProductServ
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductServ> _logger;
        
        public ProductServ(IUnitOfWork unitOfWork, ILogger<ProductServ> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<PaginatedResult<Product>> GetProductsAsync(ProductSearchRequest request)
        {
            return await _unitOfWork.Products.GetProductsAsync(request);
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _unitOfWork.Products.GetByIdAsync(id);
        }

        public async Task<Guid> CreateProductAsync(string name, decimal price)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Price = price
            };
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return product.Id;
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
