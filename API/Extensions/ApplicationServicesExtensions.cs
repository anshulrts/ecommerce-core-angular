using API.Errors;
using API.Helper;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services)
        {
            // Scoped - Service lives the the lifetime of Http request, mostly we use this.
            // Transient - Unique for every call. Created and destroyed for every access.
            // Singleton - Service lives throughout the lifetime
            services.AddScoped<IProductRepository, ProductRepository>();

            // This is how we declare the service of generics
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Automapper Service
            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                    .Where(e => e.Value.Errors.Count > 0)
                                    .SelectMany(x => x.Value.Errors)
                                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
