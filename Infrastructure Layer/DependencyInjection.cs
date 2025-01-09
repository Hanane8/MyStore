using Infrastructure_Layer.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure_Layer.Repositories;
using Microsoft.Extensions.Configuration;
using Infrastructure_Layer.DatabaseHelper;
using Application_Layer.Interfaces;
using Application_Layer;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure_Layer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrctureLayer(this IServiceCollection services, string connectionString, IConfiguration Configuration)
        {
            services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<SeedHelper>();
            services.AddTransient<PasswordHasher<User>>();


            return services;
        }
    }
}
