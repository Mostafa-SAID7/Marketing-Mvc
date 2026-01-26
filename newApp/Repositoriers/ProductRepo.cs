using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using newApp.Data;
using newApp.Models.entity;

namespace newApp.Repositoriers
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));

            await _context.Products.AddAsync(product);
        }

        public Task UpdateAsync(Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));

            _context.Products.Update(product);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }
    }
}
