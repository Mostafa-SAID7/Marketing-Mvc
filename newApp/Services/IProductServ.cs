namespace newApp.Services
{
    public interface IProductServ
    {
        Task<Guid> CreateProductAsync(string name);
    }
}
