using System;
using System.Collections.Generic;
using System.Linq;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User Get(int id)
        {
            return _context.Users.Find(id);
        }

        public User IsExists(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
                _context.Users.Remove(user);
        }
    }
}
