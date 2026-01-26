using newApp.Models;
using newApp.Models.entity;

namespace newApp.Repositoriers
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<PaginatedResult<Order>> GetOrdersAsync(OrderSearchRequest request);
        Task<Order?> GetByIdAsync(Guid id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
    }
}
