namespace newApp.Models.entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        decimal Price { get; set; }

    }
}
