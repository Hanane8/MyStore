﻿using Domain_Layer.Models;
using Infrastructure_Layer.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure_Layer.DatabaseHelper
{
    public class SeedHelper
    {

        public static async Task SeedDataAsync(DatabaseContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Men",
                        ImageUrl = "https://images.unsplash.com/photo-1598974081621-62122a866f87?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=400"
                    },
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Women",
                        ImageUrl = "https://images.unsplash.com/photo-1593032457861-48b9b400215e?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=400"
                    },
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Kids",
                        ImageUrl = "https://images.unsplash.com/photo-1618354692164-fcc3cdda0ff2?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=400"
                    }
                };

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();

                var clothingTypes = new List<ClothingType>
                    {
                        new ClothingType { Id = Guid.NewGuid(), Name = "T-Shirt", CategoryId = categories[0].Id },
                        new ClothingType { Id = Guid.NewGuid(), Name = "Jeans", CategoryId = categories[0].Id },
                        new ClothingType { Id = Guid.NewGuid(), Name = "Blouse", CategoryId = categories[1].Id },
                        new ClothingType { Id = Guid.NewGuid(), Name = "Skirt", CategoryId = categories[1].Id },
                        new ClothingType { Id = Guid.NewGuid(), Name = "Hoodie", CategoryId = categories[2].Id },
                        new ClothingType { Id = Guid.NewGuid(), Name = "Shorts", CategoryId = categories[2].Id }
                    };

                context.ClothingTypes.AddRange(clothingTypes);
                await context.SaveChangesAsync();

                var products = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Men's T-Shirt",
                        Description = "Comfortable cotton T-shirt for men",
                        Price = 19.99m,
                        Stock = 50,
                        ImageUrl = "https://images.unsplash.com/photo-1602810317284-2f71990b17cc?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=400",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "T-Shirt").Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Men's Jeans",
                        Description = "Classic blue jeans for men",
                        Price = 49.99m,
                        Stock = 30,
                        ImageUrl = "https://images.unsplash.com/photo-1603782142209-d017e4a3cbf4?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=400",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Jeans").Id
                    },

                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Women's Blouse",
                        Description = "Lightweight and stylish blouse for women",
                        Price = 29.99m,
                        Stock = 40,
                        ImageUrl = "https://images.unsplash.com/photo-1520971545331-b1c00e103e13?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=400",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Blouse").Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Women's Skirt",
                        Description = "Elegant skirt for any occasion",
                        Price = 39.99m,
                        Stock = 20,
                        ImageUrl = "https://images.unsplash.com/photo-1550831107-ded5b1c15550?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=400",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Skirt").Id
                    },

                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Kids' Hoodie",
                        Description = "Warm and cozy hoodie for kids",
                        Price = 24.99m,
                        Stock = 60,
                        ImageUrl = "https://images.unsplash.com/photo-1595361799459-e491a819ee4b?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=400",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Hoodie").Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Kids' Shorts",
                        Description = "Breathable shorts for active kids",
                        Price = 14.99m,
                        Stock = 70,
                        ImageUrl = "https://images.unsplash.com/photo-1600417096443-69fa4f6dbaca?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=400",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Shorts").Id
                    }
                };

                context.Products.AddRange(products);

                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var passwordHasher = new PasswordHasher<User>();
                var users = new List<User>
                {
                    new User
                    {
                        Email = "hanane@hotmail.com",
                        PasswordHash = "Hanane123!",
                        FirstName = "hanane",
                        LastName = "kh",
                        UserName="hanane@hotmail.com",
                        NormalizedUserName="HANANE@HOTMAIL.COM",
                        NormalizedEmail="HANANE@HOTMAIL.COM",
                        Phone = "1234567890",
                        Address = "Götbeorg 12",
                        Role = "Customer"
                    },
                    new User
                    {
                        Email = "maria@hotmail.com",
                        PasswordHash = "Maria123!",
                        FirstName = "Maria",
                        LastName = "Son",
                        UserName="maria@hotmail.com",
                        NormalizedUserName="MARIA@HOTMAIL.COM",
                        NormalizedEmail="MARIA@HOTMAIL.COM",
                        Phone = "9876543210",
                        Address = " Stockholm 123",
                        Role = "Staff"
                    }
                };
                
                users[0].PasswordHash = passwordHasher.HashPassword(users[0], "Hanane123!");
                users[1].PasswordHash = passwordHasher.HashPassword(users[1], "Maria123!");

                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }
        }
    }
}
