using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IBusyRecordService
    {
        public Task<BusyRecord> GetById(int id);
        public Task<IEnumerable<BusyRecord>> GetAll();
        public Task Create(BusyRecord record);
        public Task Update(BusyRecord record);
        public BusyRecord IsExists(int barberId, DateTime date);
    }
}
