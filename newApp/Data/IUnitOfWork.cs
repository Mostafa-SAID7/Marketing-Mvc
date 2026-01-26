using newApp.Repositoriers;

namespace newApp.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepo Products { get; }
        IOrderRepository Orders { get; }
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}