using Microsoft.AspNetCore.Mvc;
using newApp.Models;
using newApp.Services;

namespace newApp.Controllers
{
   
    [Route("orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateOrderViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var orderId = await _orderService.CreateOrderAsync(model.Total);

            model.OrderId = orderId;
            ModelState.Clear();

            return View(model);
        }
    }


}
