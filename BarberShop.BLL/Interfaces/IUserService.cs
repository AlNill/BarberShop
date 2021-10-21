using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetById(int id);
        public Task<IEnumerable<User>> GetAll();
        public void Create(User user);
        public User Get(Func<User, bool> predicate);
        public void Update(User user);
        public User GetWithInclude();
        public User GetWithInclude(int id);
        public Task<int> GetCount();
        public IEnumerable<User> GetRange(int skipPos = 0, int count = 10);
    }
}
