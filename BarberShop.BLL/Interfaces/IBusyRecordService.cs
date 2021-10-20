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
        public void Create(BusyRecord record);
        public void Update(BusyRecord record);
        public BusyRecord IsExists(int barberId, DateTime date);
    }
}
