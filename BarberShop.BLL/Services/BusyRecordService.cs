﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;

namespace BarberShop.BLL.Services
{
    public class BusyRecordService: IBusyRecordService
    {
        private readonly IGenericRepository<BusyRecord> _repository;
        public BusyRecordService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.BusyRecordRepository();
        }

        public async Task<BusyRecord> GetById(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<BusyRecord>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task Create(BusyRecord record)
        {
            await _repository.CreateAsync(record);
        }

        public async Task Update(BusyRecord record)
        {
            await _repository.UpdateAsync(record);
        }

        public BusyRecord IsExists(int barberId, DateTime date)
        {
            return _repository.Get(b => b.BarberId == barberId && b.RecordTime == date).FirstOrDefault();
        }
    }
}
