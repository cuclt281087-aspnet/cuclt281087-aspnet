using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;
using Newtonsoft.Json;

namespace ShoeStore.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Check if products already exist
            if (context.Products.Any())
            {
                return; // DB has been seeded
            }

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Nike Air Max 270",
                    Description = "Giày thể thao Nike Air Max 270 với thiết kế hiện đại và công nghệ đệm khí tiên tiến",
                    Price = 3500000,
                    SalePrice = 2800000,
                    Brand = "Nike",
                    Category = "sports",
                    Material = "Mesh + Synthetic",
                    Origin = "Vietnam",
                    Stock = 50,
                    Images = JsonConvert.SerializeObject(new[] { 
                        "https://images.pexels.com/photos/2529148/pexels-photo-2529148.jpeg",
                        "https://images.pexels.com/photos/1456733/pexels-photo-1456733.jpeg"
                    }),
                    Sizes = JsonConvert.SerializeObject(new[] { 38, 39, 40, 41, 42, 43 }),
                    Colors = JsonConvert.SerializeObject(new[] { "Đen", "Trắng", "Xám" }),
                    IsFeatured = true
                },
                new Product
                {
                    Name = "Adidas Ultraboost 22",
                    Description = "Giày chạy bộ Adidas Ultraboost 22 với công nghệ Boost Energy Return",
                    Price = 4200000,
                    Brand = "Adidas",
                    Category = "sports",
                    Material = "Primeknit",
                    Origin = "Germany",
                    Stock = 30,
                    Images = JsonConvert.SerializeObject(new[] { 
                        "https://images.pexels.com/photos/1456733/pexels-photo-1456733.jpeg",
                        "https://images.pexels.com/photos/2529148/pexels-photo-2529148.jpeg"
                    }),
                    Sizes = JsonConvert.SerializeObject(new[] { 37, 38, 39, 40, 41, 42, 43, 44 }),
                    Colors = JsonConvert.SerializeObject(new[] { "Đen", "Trắng", "Xanh" }),
                    IsFeatured = true
                },
                new Product
                {
                    Name = "Converse Chuck 70",
                    Description = "Giày Converse Chuck Taylor All Star 70 phiên bản cổ điển",
                    Price = 1800000,
                    SalePrice = 1500000,
                    Brand = "Converse",
                    Category = "casual",
                    Material = "Canvas",
                    Origin = "Vietnam",
                    Stock = 80,
                    Images = JsonConvert.SerializeObject(new[] { 
                        "https://images.pexels.com/photos/298864/pexels-photo-298864.jpeg",
                        "https://images.pexels.com/photos/1456733/pexels-photo-1456733.jpeg"
                    }),
                    Sizes = JsonConvert.SerializeObject(new[] { 35, 36, 37, 38, 39, 40, 41, 42 }),
                    Colors = JsonConvert.SerializeObject(new[] { "Đen", "Trắng", "Đỏ", "Xanh" }),
                    IsFeatured = false
                },
                new Product
                {
                    Name = "Vans Old Skool",
                    Description = "Giày Vans Old Skool với thiết kế skate classic",
                    Price = 1900000,
                    Brand = "Vans",
                    Category = "casual",
                    Material = "Suede + Canvas",
                    Origin = "Vietnam",
                    Stock = 45,
                    Images = JsonConvert.SerializeObject(new[] { 
                        "https://images.pexels.com/photos/1456733/pexels-photo-1456733.jpeg",
                        "https://images.pexels.com/photos/298864/pexels-photo-298864.jpeg"
                    }),
                    Sizes = JsonConvert.SerializeObject(new[] { 36, 37, 38, 39, 40, 41, 42, 43 }),
                    Colors = JsonConvert.SerializeObject(new[] { "Đen", "Trắng", "Nâu" }),
                    IsFeatured = true
                },
                new Product
                {
                    Name = "Clarks Desert Boot",
                    Description = "Giày boot sa mạc Clarks với da lộn cao cấp",
                    Price = 3200000,
                    SalePrice = 2900000,
                    Brand = "Clarks",
                    Category = "formal",
                    Material = "Suede Leather",
                    Origin = "UK",
                    Stock = 25,
                    Images = JsonConvert.SerializeObject(new[] { 
                        "https://images.pexels.com/photos/298864/pexels-photo-298864.jpeg",
                        "https://images.pexels.com/photos/2529148/pexels-photo-2529148.jpeg"
                    }),
                    Sizes = JsonConvert.SerializeObject(new[] { 38, 39, 40, 41, 42, 43, 44 }),
                    Colors = JsonConvert.SerializeObject(new[] { "Nâu", "Đen", "Xám" }),
                    IsFeatured = false
                },
                new Product
                {
                    Name = "Dr. Martens 1460",
                    Description = "Boots cổ cao Dr. Martens 1460 với da bền chắc",
                    Price = 4500000,
                    Brand = "Dr. Martens",
                    Category = "formal",
                    Material = "Smooth Leather",
                    Origin = "UK",
                    Stock = 20,
                    Images = JsonConvert.SerializeObject(new[] { 
                        "https://images.pexels.com/photos/298864/pexels-photo-298864.jpeg",
                        "https://images.pexels.com/photos/1456733/pexels-photo-1456733.jpeg"
                    }),
                    Sizes = JsonConvert.SerializeObject(new[] { 37, 38, 39, 40, 41, 42, 43, 44, 45 }),
                    Colors = JsonConvert.SerializeObject(new[] { "Đen", "Nâu" }),
                    IsFeatured = true
                },
                new Product
                {
                    Name = "New Balance 574",
                    Description = "Giày thể thao New Balance 574 phong cách retro",
                    Price = 2800000,
                    SalePrice = 2300000,
                    Brand = "New Balance",
                    Category = "sports",
                    Material = "Suede + Mesh",
                    Origin = "USA",
                    Stock = 60,
                    Images = JsonConvert.SerializeObject(new[] { 
                        "https://images.pexels.com/photos/2529148/pexels-photo-2529148.jpeg",
                        "https://images.pexels.com/photos/1456733/pexels-photo-1456733.jpeg"
                    }),
                    Sizes = JsonConvert.SerializeObject(new[] { 36, 37, 38, 39, 40, 41, 42, 43 }),
                    Colors = JsonConvert.SerializeObject(new[] { "Xám", "Đen", "Xanh", "Đỏ" }),
                    IsFeatured = false
                },
                new Product
                {
                    Name = "Puma RS-X",
                    Description = "Giày thể thao Puma RS-X với thiết kế chunky đầy cá tính",
                    Price = 3100000,
                    Brand = "Puma",
                    Category = "sports",
                    Material = "Mesh + Synthetic",
                    Origin = "Vietnam",
                    Stock = 35,
                    Images = JsonConvert.SerializeObject(new[] { 
                        "https://images.pexels.com/photos/1456733/pexels-photo-1456733.jpeg",
                        "https://images.pexels.com/photos/2529148/pexels-photo-2529148.jpeg"
                    }),
                    Sizes = JsonConvert.SerializeObject(new[] { 37, 38, 39, 40, 41, 42, 43 }),
                    Colors = JsonConvert.SerializeObject(new[] { "Đen", "Trắng", "Xanh", "Đỏ" }),
                    IsFeatured = true
                }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}