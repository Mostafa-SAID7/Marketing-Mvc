using Microsoft.AspNetCore.Mvc;

namespace newApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("NotFound");
                default:
                    return View("Error");
            }
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}