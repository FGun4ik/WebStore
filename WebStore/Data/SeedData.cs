using Microsoft.AspNetCore.Identity;
using WebStore.Models;

namespace WebStore.Data
{
    public class SeedData
    {
        public static async Task Initialize(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await context.Database.EnsureCreatedAsync();


            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }


            var adminEmail = "admin@store.com";
            var adminPassword = "Admin123!";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }


            var userEmail = "user@store.com";
            var userPassword = "User123!";
            var regularUser = await userManager.FindByEmailAsync(userEmail);

            if (regularUser == null)
            {
                regularUser = new IdentityUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(regularUser, userPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularUser, "User");
                }
            }


            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Электроника" },
                    new Category { Name = "Одежда" },
                    new Category { Name = "Книги" },
                    new Category { Name = "Спорт" },
                    new Category { Name = "Для дома" }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }


            if (!context.Products.Any())
            {
                var electronicsCategory = context.Categories.FirstOrDefault(c => c.Name == "Электроника");
                var booksCategory = context.Categories.FirstOrDefault(c => c.Name == "Книги");
                var homeCategory = context.Categories.FirstOrDefault(c => c.Name == "Для дома");

                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Смартфон Samsung Galaxy S23",
                        Description = "Флагманский смартфон с камерой 200 Мп",
                        Price = 79999.99m,
                        ImageUrl = "/images/smartphone.jpg",
                        StockQuantity = 15,
                        CategoryId = electronicsCategory?.Id ?? 1,
                        CreatedDate = DateTime.Now.AddDays(-10)
                    },
                    new Product
                    {
                        Name = "Ноутбук Lenovo IdeaPad",
                        Description = "Ноутбук с процессором Intel Core i5",
                        Price = 54999.50m,
                        ImageUrl = "/images/laptop.jpg",
                        StockQuantity = 8,
                        CategoryId = electronicsCategory?.Id ?? 1,
                        CreatedDate = DateTime.Now.AddDays(-5)
                    },
                    new Product
                    {
                        Name = "Наушники Sony WH-1000XM5",
                        Description = "Беспроводные наушники с шумоподавлением",
                        Price = 29999.00m,
                        ImageUrl = "/images/headphones.jpg",
                        StockQuantity = 25,
                        CategoryId = electronicsCategory?.Id ?? 1,
                        CreatedDate = DateTime.Now.AddDays(-3)
                    },
                    new Product
                    {
                        Name = "Книга 'C# для начинающих'",
                        Description = "Подробное руководство по C#",
                        Price = 2499.99m,
                        ImageUrl = "/images/book.jpg",
                        StockQuantity = 30,
                        CategoryId = booksCategory?.Id ?? 3,
                        CreatedDate = DateTime.Now.AddDays(-7)
                    },
                    new Product
                    {
                        Name = "Кофемашина DeLonghi",
                        Description = "Автоматическая кофемашина для дома",
                        Price = 45999.00m,
                        ImageUrl = "/images/coffee.jpg",
                        StockQuantity = 5,
                        CategoryId = homeCategory?.Id ?? 5,
                        CreatedDate = DateTime.Now.AddDays(-1)
                    },
                    new Product
                    {
                        Name = "Умные часы Apple Watch",
                        Description = "Смарт-часы с функцией ECG",
                        Price = 38999.00m,
                        ImageUrl = "/images/watch.jpg",
                        StockQuantity = 12,
                        CategoryId = electronicsCategory?.Id ?? 1,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Name = "Игровая консоль PlayStation 5",
                        Description = "Новейшая игровая консоль от Sony",
                        Price = 64999.00m,
                        ImageUrl = "/images/ps5.jpg",
                        StockQuantity = 3,
                        CategoryId = electronicsCategory?.Id ?? 1,
                        CreatedDate = DateTime.Now.AddDays(-15)
                    },
                    new Product
                    {
                        Name = "Пылесос Dyson V15",
                        Description = "Беспроводной пылесос с лазерной подсветкой",
                        Price = 72999.00m,
                        ImageUrl = "/images/vacuum.jpg",
                        StockQuantity = 7,
                        CategoryId = homeCategory?.Id ?? 5,
                        CreatedDate = DateTime.Now.AddDays(-20)
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}