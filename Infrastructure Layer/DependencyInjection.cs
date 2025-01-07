using Infrastructure_Layer.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Layer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrctureLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(connectionString));

            return services;
        }
    }
}
