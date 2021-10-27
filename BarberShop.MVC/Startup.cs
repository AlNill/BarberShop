using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.BLL.Services;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;
using BarberShop.DAL.EF;
using BarberShop.DAL.EF.Contexts;
using BarberShop.DAL.EF.Repositories;
using BarberShop.MVC.Mapper;
using BarberShop.MVC.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BarberShop.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = DbOptionsBuilder.GetConnectionString();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
                connectionString
                ));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBarberService, BarberService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBusyRecordService, BusyRecordService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IOfferService, OfferService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=About}/{action=Index}");
            });
        }
    }
}
