using System;
using System.Collections.Generic;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IUserService
    {
        User GetById(int id);
        IEnumerable<User> GetAll();
        void Create(User user);
        User Get(Func<User, bool> predicate);
        void Update(User user);
        public User GetWithInclude();
        public User GetWithInclude(int id);
        public int GetCount();
        public IEnumerable<User> GetRange(int skipPos = 0, int count = 10);
    }
}
