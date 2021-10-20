using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;
using Microsoft.Extensions.Logging;

namespace BarberShop.BLL.Interfaces
{
    public interface ILoggerService : ILogger
    {
        public Task<IEnumerable<Log>> GetAll();
    }
}
