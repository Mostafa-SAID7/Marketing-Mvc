using Microsoft.AspNetCore.Mvc;
using newApp.Models;
using newApp.Repositoriers;

namespace newApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepo _productRepo;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductRepo productRepo, ILogger<ProductController> logger)
        {
            _productRepo = productRepo;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new ProductVM());
        }
    }
}
