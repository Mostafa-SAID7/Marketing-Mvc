using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using newApp.Data;
using newApp.Extensions;
using newApp.Models;
using newApp.Models.entity;
using newApp.Models.Enums;
using newApp.Services;

namespace newApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServ _productService;
        private readonly ILogger<ProductsController> _logger;
        
        public ProductsController(IProductServ productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: Products
        public async Task<IActionResult> Index(ProductSearchRequest request)
        {
            // Set up filter options for the view
            ViewBag.FilterOptions = new Dictionary<string, List<SelectListItem>>
            {
                ["PriceRange"] = new List<SelectListItem>
                {
                    new SelectListItem { Text = "$0 - $50", Value = "0-50" },
                    new SelectListItem { Text = "$51 - $100", Value = "51-100" },
                    new SelectListItem { Text = "$101 - $500", Value = "101-500" },
                    new SelectListItem { Text = "$500+", Value = "500+" }
                }
            };

            ViewBag.SearchPlaceholder = "Search products by name...";

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // AJAX request - return partial view with data
                var result = await _productService.GetProductsAsync(request);
                return Json(new
                {
                    html = await this.RenderViewAsync("_ProductList", result.Items),
                    totalItems = result.TotalItems,
                    currentPage = result.CurrentPage,
                    totalPages = result.TotalPages
                });
            }

            // Regular request - return full view
            var products = await _productService.GetProductsAsync(request);
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            await SetupCreateEditViewBag();
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;
                
                // Generate SKU if not provided
                if (string.IsNullOrEmpty(product.Sku))
                {
                    product.Sku = $"PRD-{DateTime.UtcNow:yyyyMMdd}-{product.Id.ToString()[..8].ToUpper()}";
                }

                await _productService.CreateProductAsync(product);
                TempData["SuccessMessage"] = $"Product '{product.Name}' created successfully.";
                return RedirectToAction(nameof(Index));
            }
            
            await SetupCreateEditViewBag();
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            
            await SetupCreateEditViewBag();
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                product.UpdatedAt = DateTime.UtcNow;
                await _productService.UpdateProductAsync(product);
                TempData["SuccessMessage"] = $"Product '{product.Name}' updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            
            await SetupCreateEditViewBag();
            return View(product);
        }

        private async Task SetupCreateEditViewBag()
        {
            // Get categories for dropdown
            using var scope = HttpContext.RequestServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var categories = await context.Categories.Where(c => c.IsActive).OrderBy(c => c.Name).ToListAsync();
            
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.ProductStatuses = new SelectList(Enum.GetValues<ProductStatus>().Select(s => new { 
                Value = (int)s, 
                Text = s.ToString() 
            }), "Value", "Text");
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                await _productService.DeleteProductAsync(id);
                TempData["SuccessMessage"] = $"Product '{product.Name}' deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Product not found.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
