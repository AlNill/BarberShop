using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IBusyRecordService
    {
        public Task<BusyRecord> GetAsync(int id);
        public Task<IEnumerable<BusyRecord>> GetAllAsync();
        public Task CreateAsync(int barberId, int offerId, DateTime date);
        public Task UpdateAsync(BusyRecord record);
    }
}
