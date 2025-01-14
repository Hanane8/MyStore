﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using AutoMapper;
using Application_Layer.AutoMapper;
using Application_Layer.Helpers;


namespace Application_Layer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });
            services.AddSingleton(config.CreateMapper());
           
            services.AddScoped<TokenHelper>();

            return services;
        }
    }
}
