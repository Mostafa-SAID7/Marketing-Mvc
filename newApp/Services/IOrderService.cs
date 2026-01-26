using newApp.Data;
using newApp.Models.entity;

namespace newApp.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<Guid> CreateOrderAsync(decimal total);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Guid id);
    }
}
