using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newApp.Data;
using newApp.Models;
using newApp.Models.Enums;
using newApp.Models.ViewModels.Home;
using newApp.Models.ViewModels.Product;

namespace newApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeVM();

            // Get featured products
            var featuredProducts = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsFeatured && p.Status == ProductStatus.Active)
                .OrderBy(p => p.SortOrder)
                .Take(6)
                .Select(p => new ProductCardVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CompareAtPrice = p.CompareAtPrice,
                    ImageUrl = p.ImageUrl,
                    ImageAlt = p.ImageAlt,
                    Status = p.Status,
                    IsFeatured = p.IsFeatured,
                    StockQuantity = p.StockQuantity,
                    TrackQuantity = p.TrackQuantity,
                    CategoryName = p.Category != null ? p.Category.Name : null
                })
                .ToListAsync();

            // Get latest products
            var latestProducts = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Status == ProductStatus.Active)
                .OrderByDescending(p => p.CreatedAt)
                .Take(8)
                .Select(p => new ProductCardVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CompareAtPrice = p.CompareAtPrice,
                    ImageUrl = p.ImageUrl,
                    ImageAlt = p.ImageAlt,
                    Status = p.Status,
                    IsFeatured = p.IsFeatured,
                    StockQuantity = p.StockQuantity,
                    TrackQuantity = p.TrackQuantity,
                    CategoryName = p.Category != null ? p.Category.Name : null
                })
                .ToListAsync();

            // Get products on sale
            var onSaleProducts = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Status == ProductStatus.Active && p.CompareAtPrice.HasValue && p.CompareAtPrice > p.Price)
                .OrderByDescending(p => (p.CompareAtPrice - p.Price) / p.CompareAtPrice) // Order by discount percentage
                .Take(6)
                .Select(p => new ProductCardVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CompareAtPrice = p.CompareAtPrice,
                    ImageUrl = p.ImageUrl,
                    ImageAlt = p.ImageAlt,
                    Status = p.Status,
                    IsFeatured = p.IsFeatured,
                    StockQuantity = p.StockQuantity,
                    TrackQuantity = p.TrackQuantity,
                    CategoryName = p.Category != null ? p.Category.Name : null
                })
                .ToListAsync();

            // Get statistics
            viewModel.FeaturedProducts = featuredProducts;
            viewModel.LatestProducts = latestProducts;
            viewModel.OnSaleProducts = onSaleProducts;
            viewModel.TotalProducts = await _context.Products.CountAsync(p => p.Status == ProductStatus.Active);
            viewModel.TotalOrders = await _context.Orders.CountAsync();
            viewModel.TotalCustomers = await _context.Customers.CountAsync(c => c.IsActive);

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
