using Microsoft.AspNetCore.Mvc;
using newApp.Models.entity;
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
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product.Name, product.Price);
                TempData["SuccessMessage"] = "Product created successfully.";
                return RedirectToAction(nameof(Index));
            }
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
                await _productService.UpdateProductAsync(product);
                TempData["SuccessMessage"] = "Product updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            TempData["SuccessMessage"] = "Product deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
