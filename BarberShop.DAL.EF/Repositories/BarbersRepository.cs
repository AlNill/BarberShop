using System.Collections.Generic;
using System.Linq;
using BarberShop.BLL.Models;
using BarberShop.DAL.Common;
using BarberShop.DAL.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Repositories
{
    public class BarbersRepository: IRepository<Barber>
    {
        private readonly BarberContext _context;

        public BarbersRepository(BarberContext context)
        {
            _context = context;
        }

        public IEnumerable<Barber> GetAll()
        {
            return _context.Barbers.ToList();
        }

        public Barber Get(int id) => _context.Barbers.Find(id);

        public IEnumerable<Barber> Find()
        {
            //public IEnumerable<Barber> Find(Func<Barber, bool> predicate)
            //return _context.Barbers.Where(predicate).ToList();
            throw new System.NotImplementedException();
        }

        public void Create(Barber item)
        {
            _context.Barbers.Add(item);
            _context.SaveChanges();
        }

        public void Update(Barber item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Barber barber = _context.Barbers.Find(id);
            if (barber != null)
                _context.Barbers.Remove(barber);
        }
    }
}
