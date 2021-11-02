using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetAsync(int id);
        public Task<User> GetByNickNameAsync(string nickName);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task CreateAsync(string name, string fatherName, string surname,
            string nickname, string password, string email);
        public User Get(Func<User, bool> predicate);
        public Task UpdateAsync(User user);
        public User GetWithInclude(int id);
        public Task<int> GetCountAsync();
        public IEnumerable<User> GetRange(int skipPos = 0, int count = 10);
        public Task DeleteAsync(int id);
    }
}
