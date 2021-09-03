﻿using System;
using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using Microsoft.Extensions.Logging;

namespace BarberShop.BLL.Services
{
    public class LoggerService: ILoggerService
    {
        private readonly IGenericRepository<Log> _repository;

        public LoggerService(IGenericRepository<Log> repository)
        {
            _repository = repository;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var log = new Log {Level = logLevel, Time = DateTime.Now, Message = formatter(state, exception) };
            _repository.Create(log);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public IEnumerable<Log> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
