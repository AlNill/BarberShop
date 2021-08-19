using System;
using System.Collections.Generic;
using System.Linq;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Repositories
{
    public class BusyRecordsRepository: IRepository<BusyRecord>
    {
        private readonly ApplicationContext _context;
        public BusyRecordsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<BusyRecord> GetAll()
        {
            return _context.BusyRecords.Include(r => r.Barber).ToList();
        }

        public BusyRecord Get(int id)
        {
            return _context.BusyRecords.Find(id);
        }

        public BusyRecord IsExists(Func<BusyRecord, bool> predicate)
        {
            var records = _context.BusyRecords.Where(predicate).ToList();
            return records.Count == 0 ? null : records.First();
        }

        public void Create(BusyRecord item)
        {
            _context.BusyRecords.Add(item);
            _context.SaveChanges();
        }

        public void Update(BusyRecord item)
        {
            var record = _context.BusyRecords.Find(item.Id);
            _context.Entry(record).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            BusyRecord record = _context.BusyRecords.Find(id);
            if (record == null)
                return;
            _context.BusyRecords.Remove(record);
        }
    }
}
