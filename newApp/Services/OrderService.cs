using newApp.Models.entity;
using newApp.Repositoriers;

namespace newApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateOrderAsync(decimal total)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Total = total,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(order);
            return order.Id;
        }
    }

}
