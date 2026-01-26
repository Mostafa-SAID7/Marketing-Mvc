using newApp.Models.entity;
using newApp.Repositoriers;

namespace newApp.Services
{
    public class ProductServ : IProductServ
    {
        private readonly ILogger<ProductServ> _logger;
        private readonly IProductRepo _productRepo;
        public ProductServ(ILogger<ProductServ> logger, IProductRepo productRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
        }
        public async Task<Guid> CreateProductAsync(string name)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = name
            };
            await _productRepo.AddAsync(product);
            return product.Id; // Fixed: use property access, not method invocation
        }
    }
}
