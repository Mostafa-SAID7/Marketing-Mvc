using newApp.Models.Enums;

namespace newApp.Models.entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? CompareAtPrice { get; set; }
        public decimal? CostPrice { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageAlt { get; set; }
        public string? Sku { get; set; }
        public string? Barcode { get; set; }
        public int StockQuantity { get; set; }
        public int? LowStockThreshold { get; set; }
        public bool TrackQuantity { get; set; } = true;
        public decimal? Weight { get; set; }
        public string? WeightUnit { get; set; } = "kg";
        public ProductStatus Status { get; set; } = ProductStatus.Draft;
        public bool IsFeatured { get; set; } = false;
        public int SortOrder { get; set; } = 0;
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? Tags { get; set; }
        public Guid? CategoryId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Computed properties
        public bool IsOnSale => CompareAtPrice.HasValue && CompareAtPrice > Price;
        public decimal? SavingsAmount => CompareAtPrice.HasValue ? CompareAtPrice - Price : null;
        public decimal? SavingsPercentage => CompareAtPrice.HasValue && CompareAtPrice > 0 
            ? Math.Round(((CompareAtPrice.Value - Price) / CompareAtPrice.Value) * 100, 2) 
            : null;
        public bool IsLowStock => TrackQuantity && LowStockThreshold.HasValue && StockQuantity <= LowStockThreshold;
        public bool IsOutOfStock => TrackQuantity && StockQuantity <= 0;
    }
}
