using System.Collections.Generic;
using BarberShop.DAL.Common.Models;
using Microsoft.Extensions.Logging;

namespace BarberShop.BLL.Interfaces
{
    public interface ILoggerService : ILogger
    {
        public IEnumerable<Log> GetAll();
    }
}
