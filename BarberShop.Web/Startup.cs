using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.BLL.Models;
using BarberShop.BLL.Services;
using BarberShop.DAL.Common;
using BarberShop.DAL.EF.Contexts;
using BarberShop.DAL.EF.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BarberShop.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=BarberShopDb;Trusted_Connection=True;";
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
                connectionString
            ));

            services.AddScoped<IRepository<Barber>, BarbersRepository>();

            services.AddScoped<IBarberService, BarberService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
