using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;
using Microsoft.Extensions.Logging;

namespace BarberShop.BLL.Services
{
    public class LoggerService: ILoggerService
    {
        private readonly IGenericRepository<Log> _repository;

        public LoggerService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.LogRepository();
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var log = new Log { Level = logLevel, Time = DateTime.Now, Message = formatter(state, exception) };
            await _repository.Create(log);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public async Task<IEnumerable<Log>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
