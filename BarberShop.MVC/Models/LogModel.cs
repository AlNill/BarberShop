using System;
using Microsoft.Extensions.Logging;

namespace BarberShop.MVC.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; }
    }
}
