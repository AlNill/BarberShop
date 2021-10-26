using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace BarberShop.MVC.Utils
{
    public static class AppSettingsParser
    {
        private static IConfigurationRoot SetConfig()
        {
            var builder = new ConfigurationBuilder();
            // Set path to current directory
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // Get configuration from json file
            builder.AddJsonFile("appsettings.json");
            // Save configuration
            return builder.Build();
        }

        public static string GetConnectionString()
        {
            return SetConfig().GetSection("ConnectionsString:DefaultConnection").Value;
        }

        public static Tuple<string, string> GetEmailCredentials()
        {
            return new Tuple<string, string>(
                SetConfig().GetSection("NotifyEmail:Email").Value,
                SetConfig().GetSection("NotifyEmail:Password").Value
                );
        }
    }
}
