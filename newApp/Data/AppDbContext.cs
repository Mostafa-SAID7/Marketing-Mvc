using Microsoft.EntityFrameworkCore;
using newApp.Models.entity;
using newApp.Models.Enums;

namespace newApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price).HasPrecision(18, 2);
                entity.Property(p => p.CompareAtPrice).HasPrecision(18, 2);
                entity.Property(p => p.CostPrice).HasPrecision(18, 2);
                entity.Property(p => p.Weight).HasPrecision(10, 3);
                
                entity.Property(p => p.Name).HasMaxLength(200).IsRequired();
                entity.Property(p => p.Description).HasMaxLength(2000);
                entity.Property(p => p.ImageUrl).HasMaxLength(500);
                entity.Property(p => p.ImageAlt).HasMaxLength(200);
                entity.Property(p => p.Sku).HasMaxLength(100);
                entity.Property(p => p.Barcode).HasMaxLength(100);
                entity.Property(p => p.WeightUnit).HasMaxLength(10);
                entity.Property(p => p.MetaTitle).HasMaxLength(200);
                entity.Property(p => p.MetaDescription).HasMaxLength(500);
                entity.Property(p => p.Tags).HasMaxLength(1000);

                entity.Property(p => p.Status).HasConversion<int>();
                
                entity.HasIndex(p => p.Sku).IsUnique().HasFilter("[Sku] IS NOT NULL");
                entity.HasIndex(p => p.Barcode).IsUnique().HasFilter("[Barcode] IS NOT NULL");
                entity.HasIndex(p => p.Status);
                entity.HasIndex(p => p.IsFeatured);
                entity.HasIndex(p => p.CreatedAt);

                // Relationship with Category
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.SubTotal).HasPrecision(18, 2);
                entity.Property(o => o.TaxAmount).HasPrecision(18, 2);
                entity.Property(o => o.ShippingAmount).HasPrecision(18, 2);
                entity.Property(o => o.DiscountAmount).HasPrecision(18, 2);
                entity.Property(o => o.Total).HasPrecision(18, 2);

                entity.Property(o => o.OrderNumber).HasMaxLength(50).IsRequired();
                entity.Property(o => o.CustomerEmail).HasMaxLength(200).IsRequired();
                entity.Property(o => o.CustomerPhone).HasMaxLength(20);
                entity.Property(o => o.ShippingFirstName).HasMaxLength(100).IsRequired();
                entity.Property(o => o.ShippingLastName).HasMaxLength(100).IsRequired();
                entity.Property(o => o.ShippingAddress).HasMaxLength(500).IsRequired();
                entity.Property(o => o.ShippingCity).HasMaxLength(100).IsRequired();
                entity.Property(o => o.ShippingState).HasMaxLength(100).IsRequired();
                entity.Property(o => o.ShippingZipCode).HasMaxLength(20).IsRequired();
                entity.Property(o => o.ShippingCountry).HasMaxLength(100).IsRequired();
                entity.Property(o => o.BillingFirstName).HasMaxLength(100);
                entity.Property(o => o.BillingLastName).HasMaxLength(100);
                entity.Property(o => o.BillingAddress).HasMaxLength(500);
                entity.Property(o => o.BillingCity).HasMaxLength(100);
                entity.Property(o => o.BillingState).HasMaxLength(100);
                entity.Property(o => o.BillingZipCode).HasMaxLength(20);
                entity.Property(o => o.BillingCountry).HasMaxLength(100);
                entity.Property(o => o.PaymentMethod).HasMaxLength(50);
                entity.Property(o => o.PaymentTransactionId).HasMaxLength(200);
                entity.Property(o => o.TrackingNumber).HasMaxLength(100);
                entity.Property(o => o.Notes).HasMaxLength(2000);

                entity.Property(o => o.Status).HasConversion<int>();
                entity.Property(o => o.PaymentStatus).HasConversion<int>();

                entity.HasIndex(o => o.OrderNumber).IsUnique();
                entity.HasIndex(o => o.CustomerEmail);
                entity.HasIndex(o => o.Status);
                entity.HasIndex(o => o.PaymentStatus);
                entity.HasIndex(o => o.CreatedAt);

                // Relationship with Customer
                entity.HasOne(o => o.Customer)
                      .WithMany(c => c.Orders)
                      .HasForeignKey(o => o.CustomerId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure OrderItem entity
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(oi => oi.UnitPrice).HasPrecision(18, 2);

                entity.HasIndex(oi => new { oi.OrderId, oi.ProductId });

                // Relationships
                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Product)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(oi => oi.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name).HasMaxLength(200).IsRequired();
                entity.Property(c => c.Description).HasMaxLength(1000);
                entity.Property(c => c.ImageUrl).HasMaxLength(500);
                entity.Property(c => c.Type).HasConversion<int>();

                entity.HasIndex(c => c.Name).IsUnique();
                entity.HasIndex(c => c.Type);
                entity.HasIndex(c => c.IsActive);
            });

            // Configure Customer entity
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(c => c.LastName).HasMaxLength(100).IsRequired();
                entity.Property(c => c.Email).HasMaxLength(200).IsRequired();
                entity.Property(c => c.Phone).HasMaxLength(20);
                entity.Property(c => c.Address).HasMaxLength(500);
                entity.Property(c => c.City).HasMaxLength(100);
                entity.Property(c => c.State).HasMaxLength(100);
                entity.Property(c => c.ZipCode).HasMaxLength(20);
                entity.Property(c => c.Country).HasMaxLength(100);

                entity.HasIndex(c => c.Email).IsUnique();
                entity.HasIndex(c => c.IsActive);
                entity.HasIndex(c => c.CreatedAt);
            });

            // Configure Item entity (existing)
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(i => i.Price).HasPrecision(18, 2);
                entity.Property(i => i.Name).HasMaxLength(200).IsRequired();
            });
        }
    }
}
