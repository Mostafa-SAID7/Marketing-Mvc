using Microsoft.EntityFrameworkCore;
using newApp.Data;
using newApp.Models.entity;

namespace newApp.Repositoriers
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.OrderByDescending(o => o.CreatedAt).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public Task UpdateAsync(Order order)
        {
            if (order is null)
                throw new ArgumentNullException(nameof(order));

            _context.Orders.Update(order);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }
    }
}
