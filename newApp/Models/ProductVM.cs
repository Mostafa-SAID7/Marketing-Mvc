using System.ComponentModel.DataAnnotations;

namespace newApp.Models
{
    public class ProductVM
    {
        [Required]
        [Range(1,8,ErrorMessage ="Name Product ...")]
        public string Name { get; set; } = string.Empty;
        decimal Price { get; set; }
    }
}
