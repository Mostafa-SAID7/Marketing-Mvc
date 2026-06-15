using market_mvc.Repositoriers;

namespace market_mvc.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepo Products { get; }
        IOrderRepository Orders { get; }
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
