using System;
using System.Collections.Generic;
using System.Linq;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Services
{
    public class BusyRecordService: IBusyRecordService
    {
        private readonly IGenericRepository<BusyRecord> _repository;
        public BusyRecordService(IGenericRepository<BusyRecord> repository)
        {
            _repository = repository;
        }

        public BusyRecord GetById(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<BusyRecord> GetAll()
        {
            return _repository.GetAll();
        }

        public void Create(BusyRecord record)
        {
            _repository.Create(record);
        }

        public void Update(BusyRecord record)
        {
            _repository.Update(record);
        }

        public BusyRecord IsExists(int barberId, DateTime date)
        {
            return _repository.Get(b => b.BarberId == barberId && b.RecordTime == date).FirstOrDefault();
        }
    }
}
