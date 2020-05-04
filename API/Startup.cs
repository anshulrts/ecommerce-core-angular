using API.Helper;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration  _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Ordering of services doesn't matter. It matter in the case of middleware in Configure()
            services.AddControllers();
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
            
            // Scoped - Service lives the the lifetime of Http request, mostly we use this.
            // Transient - Unique for every call. Created and destroyed for every access.
            // Singleton - Service lives throughout the lifetime
            services.AddScoped<IProductRepository, ProductRepository>();

            // This is how we declare the service of generics
            services.AddScoped(typeof (IGenericRepository<>), typeof (GenericRepository<>));

            // Automapper Service
            services.AddAutoMapper(typeof(MappingProfiles));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
