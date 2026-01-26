using newApp.Models.entity;

namespace newApp.Repositoriers
{
    public interface IProductRepo
    {
        Task AddAsync(Product product);
    }
}
