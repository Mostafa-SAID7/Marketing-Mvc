using newApp.Models.ViewModels.Home;
using newApp.Models.ViewModels.Product;

namespace newApp.Services
{
    public interface IHomeService
    {
        Task<HomeVM> GetHomeDataAsync();
        Task<List<ProductCardVM>> GetFeaturedProductsAsync(int count = 6);
        Task<List<ProductCardVM>> GetLatestProductsAsync(int count = 8);
        Task<List<ProductCardVM>> GetOnSaleProductsAsync(int count = 6);
        Task<Dictionary<string, int>> GetDashboardStatisticsAsync();
        Task<Dictionary<string, decimal>> GetSalesStatisticsAsync();
        Task<List<ProductCardVM>> GetRecommendedProductsAsync(int count = 4);
        Task<HomeVM> GetDashboardDataAsync();
        Task<Dictionary<string, int>> GetProductStatisticsAsync();
    }
}