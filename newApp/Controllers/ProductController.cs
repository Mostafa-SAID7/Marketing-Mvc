using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using newApp.Data;
using newApp.Extensions;
using newApp.Models;
using newApp.Models.entity;
using newApp.Models.Enums;
using newApp.Models.ViewModels.Product;
using newApp.Services;

namespace newApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServ _productService;
        private readonly IImageService _imageService;
        private readonly ILogger<ProductsController> _logger;
        
        public ProductsController(IProductServ productService, IImageService imageService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _imageService = imageService;
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
            return View(new ProductCreateVM());
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Price = viewModel.Price,
                    CompareAtPrice = viewModel.CompareAtPrice,
                    CostPrice = viewModel.CostPrice,
                    ImageAlt = viewModel.ImageAlt,
                    Sku = viewModel.Sku,
                    Barcode = viewModel.Barcode,
                    StockQuantity = viewModel.StockQuantity,
                    LowStockThreshold = viewModel.LowStockThreshold,
                    TrackQuantity = viewModel.TrackQuantity,
                    Weight = viewModel.Weight,
                    WeightUnit = viewModel.WeightUnit,
                    Status = viewModel.Status,
                    IsFeatured = viewModel.IsFeatured,
                    SortOrder = viewModel.SortOrder,
                    MetaTitle = viewModel.MetaTitle,
                    MetaDescription = viewModel.MetaDescription,
                    Tags = viewModel.Tags,
                    CategoryId = viewModel.CategoryId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Handle image upload
                if (viewModel.ImageFile != null && _imageService.IsValidImageFile(viewModel.ImageFile))
                {
                    var imageUrl = await _imageService.UploadImageAsync(viewModel.ImageFile, "products");
                    if (imageUrl != null)
                    {
                        product.ImageUrl = imageUrl;
                    }
                    else
                    {
                        ModelState.AddModelError("ImageFile", "Failed to upload image. Please try again.");
                        await SetupCreateEditViewBag();
                        return View(viewModel);
                    }
                }
                else if (!string.IsNullOrEmpty(viewModel.ImageUrl))
                {
                    product.ImageUrl = viewModel.ImageUrl;
                }

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
            return View(viewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductEditVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CompareAtPrice = product.CompareAtPrice,
                CostPrice = product.CostPrice,
                ImageUrl = product.ImageUrl,
                CurrentImageUrl = product.ImageUrl,
                ImageAlt = product.ImageAlt,
                Sku = product.Sku,
                Barcode = product.Barcode,
                StockQuantity = product.StockQuantity,
                LowStockThreshold = product.LowStockThreshold,
                TrackQuantity = product.TrackQuantity,
                Weight = product.Weight,
                WeightUnit = product.WeightUnit,
                Status = product.Status,
                IsFeatured = product.IsFeatured,
                SortOrder = product.SortOrder,
                MetaTitle = product.MetaTitle,
                MetaDescription = product.MetaDescription,
                Tags = product.Tags,
                CategoryId = product.CategoryId,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
            
            await SetupCreateEditViewBag();
            return View(viewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductEditVM viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                // Store old image URL for potential deletion
                var oldImageUrl = product.ImageUrl;

                // Update product properties
                product.Name = viewModel.Name;
                product.Description = viewModel.Description;
                product.Price = viewModel.Price;
                product.CompareAtPrice = viewModel.CompareAtPrice;
                product.CostPrice = viewModel.CostPrice;
                product.ImageAlt = viewModel.ImageAlt;
                product.Sku = viewModel.Sku;
                product.Barcode = viewModel.Barcode;
                product.StockQuantity = viewModel.StockQuantity;
                product.LowStockThreshold = viewModel.LowStockThreshold;
                product.TrackQuantity = viewModel.TrackQuantity;
                product.Weight = viewModel.Weight;
                product.WeightUnit = viewModel.WeightUnit;
                product.Status = viewModel.Status;
                product.IsFeatured = viewModel.IsFeatured;
                product.SortOrder = viewModel.SortOrder;
                product.MetaTitle = viewModel.MetaTitle;
                product.MetaDescription = viewModel.MetaDescription;
                product.Tags = viewModel.Tags;
                product.CategoryId = viewModel.CategoryId;
                product.UpdatedAt = DateTime.UtcNow;

                // Handle image upload
                if (viewModel.ImageFile != null && _imageService.IsValidImageFile(viewModel.ImageFile))
                {
                    var imageUrl = await _imageService.UploadImageAsync(viewModel.ImageFile, "products");
                    if (imageUrl != null)
                    {
                        product.ImageUrl = imageUrl;
                        
                        // Delete old image if it was uploaded (not external URL)
                        if (!string.IsNullOrEmpty(oldImageUrl) && !oldImageUrl.StartsWith("http"))
                        {
                            await _imageService.DeleteImageAsync(oldImageUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ImageFile", "Failed to upload image. Please try again.");
                        viewModel.CurrentImageUrl = oldImageUrl;
                        await SetupCreateEditViewBag();
                        return View(viewModel);
                    }
                }
                else if (!string.IsNullOrEmpty(viewModel.ImageUrl) && viewModel.ImageUrl != oldImageUrl)
                {
                    // URL changed, update it
                    product.ImageUrl = viewModel.ImageUrl;
                    
                    // Delete old uploaded image if it exists
                    if (!string.IsNullOrEmpty(oldImageUrl) && !oldImageUrl.StartsWith("http"))
                    {
                        await _imageService.DeleteImageAsync(oldImageUrl);
                    }
                }

                await _productService.UpdateProductAsync(product);
                TempData["SuccessMessage"] = $"Product '{product.Name}' updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            
            await SetupCreateEditViewBag();
            return View(viewModel);
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
