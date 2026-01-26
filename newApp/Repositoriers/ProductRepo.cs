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

        public async Task AddAsync(Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
    }
}
