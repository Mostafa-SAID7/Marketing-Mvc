using Microsoft.EntityFrameworkCore;
using newApp.Models.entity;
using newApp.Models.Enums;

namespace newApp.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Seed Categories
            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Electronics",
                        Description = "Electronic devices and gadgets",
                        Type = CategoryType.Electronics,
                        ImageUrl = "https://images.unsplash.com/photo-1498049794561-7780e7231661?w=400",
                        IsActive = true
                    },
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Clothing",
                        Description = "Fashion and apparel",
                        Type = CategoryType.Clothing,
                        ImageUrl = "https://images.unsplash.com/photo-1441986300917-64674bd600d8?w=400",
                        IsActive = true
                    },
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Books",
                        Description = "Books and literature",
                        Type = CategoryType.Books,
                        ImageUrl = "https://images.unsplash.com/photo-1481627834876-b7833e8f5570?w=400",
                        IsActive = true
                    },
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Home & Garden",
                        Description = "Home improvement and garden supplies",
                        Type = CategoryType.Home,
                        ImageUrl = "https://images.unsplash.com/photo-1586023492125-27b2c045efd7?w=400",
                        IsActive = true
                    },
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sports & Outdoors",
                        Description = "Sports equipment and outdoor gear",
                        Type = CategoryType.Sports,
                        ImageUrl = "https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?w=400",
                        IsActive = true
                    }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed Customers
            if (!await context.Customers.AnyAsync())
            {
                var customers = new List<Customer>
                {
                    new Customer
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        Phone = "+1-555-0123",
                        Address = "123 Main St",
                        City = "New York",
                        State = "NY",
                        ZipCode = "10001",
                        Country = "USA",
                        IsActive = true
                    },
                    new Customer
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        Phone = "+1-555-0124",
                        Address = "456 Oak Ave",
                        City = "Los Angeles",
                        State = "CA",
                        ZipCode = "90210",
                        Country = "USA",
                        IsActive = true
                    },
                    new Customer
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Mike",
                        LastName = "Johnson",
                        Email = "mike.johnson@example.com",
                        Phone = "+1-555-0125",
                        Address = "789 Pine Rd",
                        City = "Chicago",
                        State = "IL",
                        ZipCode = "60601",
                        Country = "USA",
                        IsActive = true
                    }
                };

                await context.Customers.AddRangeAsync(customers);
                await context.SaveChangesAsync();
            }

            // Seed Products
            if (!await context.Products.AnyAsync())
            {
                var categories = await context.Categories.ToListAsync();
                var electronicsCategory = categories.FirstOrDefault(c => c.Type == CategoryType.Electronics);
                var clothingCategory = categories.FirstOrDefault(c => c.Type == CategoryType.Clothing);
                var booksCategory = categories.FirstOrDefault(c => c.Type == CategoryType.Books);
                var homeCategory = categories.FirstOrDefault(c => c.Type == CategoryType.Home);
                var sportsCategory = categories.FirstOrDefault(c => c.Type == CategoryType.Sports);

                var products = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Wireless Bluetooth Headphones",
                        Description = "High-quality wireless headphones with noise cancellation and 30-hour battery life.",
                        Price = 199.99m,
                        CompareAtPrice = 249.99m,
                        CostPrice = 120.00m,
                        ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=400",
                        ImageAlt = "Wireless Bluetooth Headphones",
                        Sku = "WBH-001",
                        Barcode = "1234567890123",
                        StockQuantity = 50,
                        LowStockThreshold = 10,
                        TrackQuantity = true,
                        Weight = 0.3m,
                        WeightUnit = "kg",
                        Status = ProductStatus.Active,
                        IsFeatured = true,
                        SortOrder = 1,
                        MetaTitle = "Best Wireless Bluetooth Headphones",
                        MetaDescription = "Premium wireless headphones with superior sound quality",
                        Tags = "headphones,wireless,bluetooth,audio",
                        CategoryId = electronicsCategory?.Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cotton T-Shirt",
                        Description = "Comfortable 100% cotton t-shirt available in multiple colors and sizes.",
                        Price = 24.99m,
                        CompareAtPrice = 34.99m,
                        CostPrice = 12.00m,
                        ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=400",
                        ImageAlt = "Cotton T-Shirt",
                        Sku = "CTS-001",
                        Barcode = "1234567890124",
                        StockQuantity = 100,
                        LowStockThreshold = 20,
                        TrackQuantity = true,
                        Weight = 0.2m,
                        WeightUnit = "kg",
                        Status = ProductStatus.Active,
                        IsFeatured = false,
                        SortOrder = 2,
                        MetaTitle = "Comfortable Cotton T-Shirt",
                        MetaDescription = "High-quality cotton t-shirt for everyday wear",
                        Tags = "t-shirt,cotton,clothing,casual",
                        CategoryId = clothingCategory?.Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Programming Fundamentals Book",
                        Description = "Comprehensive guide to programming fundamentals for beginners and intermediate developers.",
                        Price = 49.99m,
                        CompareAtPrice = 59.99m,
                        CostPrice = 25.00m,
                        ImageUrl = "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=400",
                        ImageAlt = "Programming Fundamentals Book",
                        Sku = "PFB-001",
                        Barcode = "1234567890125",
                        StockQuantity = 30,
                        LowStockThreshold = 5,
                        TrackQuantity = true,
                        Weight = 0.8m,
                        WeightUnit = "kg",
                        Status = ProductStatus.Active,
                        IsFeatured = true,
                        SortOrder = 3,
                        MetaTitle = "Learn Programming Fundamentals",
                        MetaDescription = "Essential programming book for developers",
                        Tags = "book,programming,education,development",
                        CategoryId = booksCategory?.Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Smart Home Security Camera",
                        Description = "WiFi-enabled security camera with night vision, motion detection, and mobile app control.",
                        Price = 129.99m,
                        CompareAtPrice = 179.99m,
                        CostPrice = 80.00m,
                        ImageUrl = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=400",
                        ImageAlt = "Smart Home Security Camera",
                        Sku = "SHSC-001",
                        Barcode = "1234567890126",
                        StockQuantity = 25,
                        LowStockThreshold = 5,
                        TrackQuantity = true,
                        Weight = 0.5m,
                        WeightUnit = "kg",
                        Status = ProductStatus.Active,
                        IsFeatured = true,
                        SortOrder = 4,
                        MetaTitle = "Smart Security Camera for Home",
                        MetaDescription = "Advanced home security with smart features",
                        Tags = "security,camera,smart home,surveillance",
                        CategoryId = homeCategory?.Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Yoga Mat Premium",
                        Description = "Non-slip premium yoga mat with extra cushioning and carrying strap.",
                        Price = 39.99m,
                        CompareAtPrice = 54.99m,
                        CostPrice = 20.00m,
                        ImageUrl = "https://images.unsplash.com/photo-1544367567-0f2fcb009e0b?w=400",
                        ImageAlt = "Premium Yoga Mat",
                        Sku = "YMP-001",
                        Barcode = "1234567890127",
                        StockQuantity = 40,
                        LowStockThreshold = 8,
                        TrackQuantity = true,
                        Weight = 1.2m,
                        WeightUnit = "kg",
                        Status = ProductStatus.Active,
                        IsFeatured = false,
                        SortOrder = 5,
                        MetaTitle = "Premium Yoga Mat for Exercise",
                        MetaDescription = "High-quality yoga mat for comfortable practice",
                        Tags = "yoga,mat,exercise,fitness,sports",
                        CategoryId = sportsCategory?.Id
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }

            // Seed Orders with OrderItems
            if (!await context.Orders.AnyAsync())
            {
                var customers = await context.Customers.ToListAsync();
                var products = await context.Products.ToListAsync();

                if (customers.Any() && products.Any())
                {
                    var orders = new List<Order>();
                    var orderItems = new List<OrderItem>();

                    // Order 1
                    var order1 = new Order
                    {
                        Id = Guid.NewGuid(),
                        OrderNumber = "ORD-2024-001",
                        CustomerId = customers[0].Id,
                        CustomerEmail = customers[0].Email,
                        CustomerPhone = customers[0].Phone,
                        ShippingFirstName = customers[0].FirstName,
                        ShippingLastName = customers[0].LastName,
                        ShippingAddress = customers[0].Address ?? "",
                        ShippingCity = customers[0].City ?? "",
                        ShippingState = customers[0].State ?? "",
                        ShippingZipCode = customers[0].ZipCode ?? "",
                        ShippingCountry = customers[0].Country ?? "",
                        SubTotal = 249.98m,
                        TaxAmount = 20.00m,
                        ShippingAmount = 9.99m,
                        DiscountAmount = 0m,
                        Total = 279.97m,
                        Status = OrderStatus.Delivered,
                        PaymentStatus = PaymentStatus.Paid,
                        PaymentMethod = "Credit Card",
                        PaymentTransactionId = "TXN-001-2024",
                        TrackingNumber = "TRK123456789",
                        CreatedAt = DateTime.UtcNow.AddDays(-7),
                        ShippedAt = DateTime.UtcNow.AddDays(-5),
                        DeliveredAt = DateTime.UtcNow.AddDays(-2)
                    };

                    orders.Add(order1);

                    orderItems.AddRange(new[]
                    {
                        new OrderItem
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order1.Id,
                            ProductId = products[0].Id, // Headphones
                            Quantity = 1,
                            UnitPrice = 199.99m
                        },
                        new OrderItem
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order1.Id,
                            ProductId = products[1].Id, // T-Shirt
                            Quantity = 2,
                            UnitPrice = 24.99m
                        }
                    });

                    // Order 2
                    var order2 = new Order
                    {
                        Id = Guid.NewGuid(),
                        OrderNumber = "ORD-2024-002",
                        CustomerId = customers[1].Id,
                        CustomerEmail = customers[1].Email,
                        CustomerPhone = customers[1].Phone,
                        ShippingFirstName = customers[1].FirstName,
                        ShippingLastName = customers[1].LastName,
                        ShippingAddress = customers[1].Address ?? "",
                        ShippingCity = customers[1].City ?? "",
                        ShippingState = customers[1].State ?? "",
                        ShippingZipCode = customers[1].ZipCode ?? "",
                        ShippingCountry = customers[1].Country ?? "",
                        SubTotal = 179.98m,
                        TaxAmount = 14.40m,
                        ShippingAmount = 9.99m,
                        DiscountAmount = 10.00m,
                        Total = 194.37m,
                        Status = OrderStatus.Processing,
                        PaymentStatus = PaymentStatus.Paid,
                        PaymentMethod = "PayPal",
                        PaymentTransactionId = "TXN-002-2024",
                        CreatedAt = DateTime.UtcNow.AddDays(-3)
                    };

                    orders.Add(order2);

                    orderItems.AddRange(new[]
                    {
                        new OrderItem
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order2.Id,
                            ProductId = products[2].Id, // Book
                            Quantity = 1,
                            UnitPrice = 49.99m
                        },
                        new OrderItem
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order2.Id,
                            ProductId = products[3].Id, // Security Camera
                            Quantity = 1,
                            UnitPrice = 129.99m
                        }
                    });

                    await context.Orders.AddRangeAsync(orders);
                    await context.OrderItems.AddRangeAsync(orderItems);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}