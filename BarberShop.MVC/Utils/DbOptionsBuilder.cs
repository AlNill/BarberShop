using System.IO;
using Microsoft.Extensions.Configuration;

namespace BarberShop.MVC.Utils
{
    public static class DbOptionsBuilder
    {
        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder();
            // Set path to current directory
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // Get configuration from json file
            builder.AddJsonFile("appsettings.json");
            // Save configuration
            var config = builder.Build();

            return config.GetSection("ConnectionsString:DefaultConnection").Value;
        }
    }
}
