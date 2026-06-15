using System.ComponentModel.DataAnnotations;

namespace market_mvc.Models.ViewModels.Product
{
    public class ProductListVM
    {
        [Required]
        [Range(1, 8, ErrorMessage = "Name Product ...")]
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
