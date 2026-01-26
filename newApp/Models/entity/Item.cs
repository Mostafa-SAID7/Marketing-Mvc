using System.ComponentModel.DataAnnotations;

namespace newApp.Models.entity
{
    public class Item
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MinLength(3),MaxLength(20)]
        public string Name { get; set; }= string.Empty;

        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
