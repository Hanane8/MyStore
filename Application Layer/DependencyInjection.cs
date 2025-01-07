using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.AutoMapper;
using Application_Layer.Behaviors;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application_Layer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly))
                 .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                 .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });
            services.AddSingleton(config.CreateMapper());



            return services;
        }
    }
}
