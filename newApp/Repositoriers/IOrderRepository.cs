using newApp.Models.entity;

namespace newApp.Repositoriers
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);

    }
}
