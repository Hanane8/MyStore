using Domain_Layer.Models;
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
                        new ClothingType { Id = Guid.NewGuid(), Name = "Jacket", CategoryId = categories[0].Id },
                        new ClothingType { Id = Guid.NewGuid(), Name = "Pants", CategoryId = categories[1].Id },
                        new ClothingType { Id = Guid.NewGuid(), Name = "Dress", CategoryId = categories[1].Id },
                        new ClothingType { Id = Guid.NewGuid(), Name = "Blouse", CategoryId = categories[1].Id },                        
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
                        Name = "White T-Shirt",
                        Description = "Comfortable cotton T-shirt for men",
                        Size = "M", 
                        Price = 29.99m,
                        Stock = 50,
                        ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=800&q=80",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "T-Shirt").Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Denim Jacket",
                        Description = "Classic blue jeans for men",
                        Size = "L",
                        Price = 49.99m,
                        Stock = 30,
                        ImageUrl = "https://images.unsplash.com/photo-1551537482-f2075a1d41f2?auto=format&fit=crop&w=800&q=80",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Jacket").Id
                    },

                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Wool Sweater",
                        Description = "Classic wool sweater for men",
                        Size = "L",
                        Price = 49.99m,
                        Stock = 30,
                        ImageUrl = "https://images.unsplash.com/photo-1576566588028-4147f3842f27?auto=format&fit=crop&w=800&q=80",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "T-Shirt").Id
                    },

                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Summer Dress",
                        Description = "Lightweight and stylish blouse for women",
                        Size = "M",
                        Price = 49.99m,
                        Stock = 40,
                        ImageUrl = "https://images.unsplash.com/photo-1595777457583-95e059d581b8?auto=format&fit=crop&w=800&q=80",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Dress").Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Black Jeans",
                        Description = "Elegant skirt for any occasion",
                        Size = "L",
                        Price = 39.99m,
                        Stock = 20,
                        ImageUrl = "https://images.unsplash.com/photo-1541099649105-f69ad21f3246?auto=format&fit=crop&w=800&q=80",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Pants").Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Floral Maxi Dress",
                        Description = "Elegant skirt for any occasion",
                        Size = "L",
                        Price = 79.99m,
                        Stock = 20,
                        ImageUrl = "https://images.unsplash.com/photo-1618244972963-dbee1a7edc95?auto=format&fit=crop&w=800&q=80",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Dress").Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Summer Blouse",
                        Description = "Elegant skirt for any occasion",
                        Size = "L",
                        Price = 49.99m,
                        Stock = 20,
                        ImageUrl = "https://images.unsplash.com/photo-1551163943-3f6a855d1153?auto=format&fit=crop&w=800&q=80",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Blouse").Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Elegant Jumpsuit",
                        Description = "Elegant skirt for any occasion",
                        Size = "L",
                        Price = 89.99m,
                        Stock = 20,
                        ImageUrl = "https://images.unsplash.com/photo-1594633312681-425c7b97ccd1?auto=format&fit=crop&w=800&q=80",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Pants").Id
                    },

                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Kids' Denim Overall",
                        Description = "Warm and cozy hoodie for kids",
                        Size= "M",
                        Price = 59.99m,
                        Stock = 60,
                        ImageUrl = "https://images.unsplash.com/photo-1519457431-44ccd64a579b?auto=format&fit=crop&w=800&q=80",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Hoodie").Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Children's Summer Set",
                        Description = "Breathable shorts for active kids",
                        Size = "S",
                        Price = 14.99m,
                        Stock = 70,
                        ImageUrl = "https://images.unsplash.com/photo-1622290291468-a28f7a7dc6a8?auto=format&fit=crop&w=800&q=80",
                        ClothingTypeId = clothingTypes.First(ct => ct.Name == "Shorts").Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Kids' Winter Jacket",
                        Description = "Breathable shorts for active kids",
                        Size = "S",
                        Price = 69.99m,
                        Stock = 70,
                        ImageUrl = "https://images.unsplash.com/photo-1503919545889-aef636e10ad4?auto=format&fit=crop&w=800&q=80",
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
