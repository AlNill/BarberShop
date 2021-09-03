using System;
using Microsoft.Extensions.Logging;

namespace BarberShop.DAL.Common.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; }
    }
}
