using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using market_mvc.Models.entity;

namespace market_mvc.Data
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

            // Configure soft delete for all entities inheriting from BaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                    var filter = Expression.Lambda(Expression.Equal(property, Expression.Constant(false)), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price).HasPrecision(18, 2);
                entity.Property(p => p.CompareAtPrice).HasPrecision(18, 2);
                entity.Property(p => p.CostPrice).HasPrecision(18, 2);
                entity.Property(p => p.Weight).HasPrecision(10, 3);
                
                entity.Property(p => p.Status).HasConversion<int>();
                
                entity.HasIndex(p => p.Sku).IsUnique().HasFilter("[Sku] IS NOT NULL AND [IsDeleted] = 0");
                entity.HasIndex(p => p.Barcode).IsUnique().HasFilter("[Barcode] IS NOT NULL AND [IsDeleted] = 0");
                entity.HasIndex(p => p.Status);
                entity.HasIndex(p => p.IsFeatured);
                entity.HasIndex(p => p.CreatedAt);
                entity.HasIndex(p => p.IsDeleted);

                // Relationship with Category
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Order entity with Value Objects
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.SubTotal).HasPrecision(18, 2);
                entity.Property(o => o.TaxAmount).HasPrecision(18, 2);
                entity.Property(o => o.ShippingAmount).HasPrecision(18, 2);
                entity.Property(o => o.DiscountAmount).HasPrecision(18, 2);
                entity.Property(o => o.Total).HasPrecision(18, 2);

                entity.Property(o => o.Status).HasConversion<int>();
                entity.Property(o => o.PaymentStatus).HasConversion<int>();

                // Configure PersonName value object
                entity.OwnsOne(o => o.CustomerName, name =>
                {
                    name.Property(n => n.FirstName).HasColumnName("CustomerFirstName").HasMaxLength(100).IsRequired();
                    name.Property(n => n.LastName).HasColumnName("CustomerLastName").HasMaxLength(100).IsRequired();
                    name.Property(n => n.MiddleName).HasColumnName("CustomerMiddleName").HasMaxLength(100);
                });

                // Configure Shipping Address value object
                entity.OwnsOne(o => o.ShippingAddress, address =>
                {
                    address.Property(a => a.Street).HasColumnName("ShippingStreet").HasMaxLength(200).IsRequired();
                    address.Property(a => a.City).HasColumnName("ShippingCity").HasMaxLength(100).IsRequired();
                    address.Property(a => a.State).HasColumnName("ShippingState").HasMaxLength(100).IsRequired();
                    address.Property(a => a.ZipCode).HasColumnName("ShippingZipCode").HasMaxLength(20).IsRequired();
                    address.Property(a => a.Country).HasColumnName("ShippingCountry").HasMaxLength(100).IsRequired();
                });

                // Configure Billing Address value object (optional)
                entity.OwnsOne(o => o.BillingAddress, address =>
                {
                    address.Property(a => a.Street).HasColumnName("BillingStreet").HasMaxLength(200);
                    address.Property(a => a.City).HasColumnName("BillingCity").HasMaxLength(100);
                    address.Property(a => a.State).HasColumnName("BillingState").HasMaxLength(100);
                    address.Property(a => a.ZipCode).HasColumnName("BillingZipCode").HasMaxLength(20);
                    address.Property(a => a.Country).HasColumnName("BillingCountry").HasMaxLength(100);
                });

                entity.HasIndex(o => o.OrderNumber).IsUnique().HasFilter("[IsDeleted] = 0");
                entity.HasIndex(o => o.CustomerEmail);
                entity.HasIndex(o => o.Status);
                entity.HasIndex(o => o.PaymentStatus);
                entity.HasIndex(o => o.CreatedAt);
                entity.HasIndex(o => o.IsDeleted);

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
                entity.HasIndex(oi => oi.IsDeleted);

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
                entity.Property(c => c.Type).HasConversion<int>();

                entity.HasIndex(c => c.Name).IsUnique().HasFilter("[IsDeleted] = 0");
                entity.HasIndex(c => c.Type);
                entity.HasIndex(c => c.IsActive);
                entity.HasIndex(c => c.IsDeleted);
            });

            // Configure Customer entity with Value Objects
            modelBuilder.Entity<Customer>(entity =>
            {
                // Configure PersonName value object
                entity.OwnsOne(c => c.Name, name =>
                {
                    name.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
                    name.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired();
                    name.Property(n => n.MiddleName).HasColumnName("MiddleName").HasMaxLength(100);
                });

                // Configure Address value object (optional)
                entity.OwnsOne(c => c.Address, address =>
                {
                    address.Property(a => a.Street).HasColumnName("AddressStreet").HasMaxLength(200);
                    address.Property(a => a.City).HasColumnName("AddressCity").HasMaxLength(100);
                    address.Property(a => a.State).HasColumnName("AddressState").HasMaxLength(100);
                    address.Property(a => a.ZipCode).HasColumnName("AddressZipCode").HasMaxLength(20);
                    address.Property(a => a.Country).HasColumnName("AddressCountry").HasMaxLength(100);
                });

                entity.HasIndex(c => c.Email).IsUnique().HasFilter("[IsDeleted] = 0");
                entity.HasIndex(c => c.IsActive);
                entity.HasIndex(c => c.CreatedAt);
                entity.HasIndex(c => c.IsDeleted);
            });

            // Configure Item entity
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(i => i.Price).HasPrecision(18, 2);
                
                entity.HasIndex(i => i.Sku).IsUnique().HasFilter("[Sku] IS NOT NULL AND [IsDeleted] = 0");
                entity.HasIndex(i => i.IsActive);
                entity.HasIndex(i => i.IsDeleted);
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        // TODO: Set CreatedBy from current user context
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        // TODO: Set UpdatedBy from current user context
                        entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                        entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        // TODO: Set DeletedBy from current user context
                        break;
                }
            }
        }
    }
}
