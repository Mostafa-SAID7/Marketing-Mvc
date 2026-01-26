using System.ComponentModel.DataAnnotations;

namespace newApp.Models
{
    public class CreateOrderViewModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Total must be greater than zero")]
        public decimal Total { get; set; }

        public Guid? OrderId { get; set; }
    }
}
