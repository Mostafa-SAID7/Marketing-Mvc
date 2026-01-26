namespace newApp.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(decimal total);

    }
}
