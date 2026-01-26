using newApp.Models.Enums;

namespace newApp.Models.entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public Guid? CustomerId { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public string ShippingFirstName { get; set; } = string.Empty;
        public string ShippingLastName { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string ShippingCity { get; set; } = string.Empty;
        public string ShippingState { get; set; } = string.Empty;
        public string ShippingZipCode { get; set; } = string.Empty;
        public string ShippingCountry { get; set; } = string.Empty;
        public string? BillingFirstName { get; set; }
        public string? BillingLastName { get; set; }
        public string? BillingAddress { get; set; }
        public string? BillingCity { get; set; }
        public string? BillingState { get; set; }
        public string? BillingZipCode { get; set; }
        public string? BillingCountry { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public string? PaymentMethod { get; set; }
        public string? PaymentTransactionId { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

        // Navigation properties
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Computed properties
        public string ShippingFullName => $"{ShippingFirstName} {ShippingLastName}";
        public string BillingFullName => $"{BillingFirstName} {BillingLastName}";
        public string ShippingFullAddress => $"{ShippingAddress}, {ShippingCity}, {ShippingState} {ShippingZipCode}, {ShippingCountry}".Trim(' ', ',');
        public string BillingFullAddress => $"{BillingAddress}, {BillingCity}, {BillingState} {BillingZipCode}, {BillingCountry}".Trim(' ', ',');
        public int TotalItems => OrderItems?.Sum(oi => oi.Quantity) ?? 0;
        public int UniqueItems => OrderItems?.Count ?? 0;
    }
}
