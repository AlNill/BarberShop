using System;
using System.Collections.Generic;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IBusyRecordService
    {
        BusyRecord GetById(int id);
        IEnumerable<BusyRecord> GetAll();
        void Create(BusyRecord record);
        void Update(BusyRecord record);
        BusyRecord IsExists(int barberId, DateTime date);
    }
}
