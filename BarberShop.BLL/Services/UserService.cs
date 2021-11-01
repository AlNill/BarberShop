using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;

namespace BarberShop.BLL.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.UserRepository();
        }

        public async Task<int> GetCount() => await _repository.GetCountAsync();

        public IEnumerable<User> GetRange(int skipPos = 0, int count = 10) => 
            _repository.GetRange(skipPos, count);
    
        public async Task<User> GetById(int id) => await _repository.Get(id);

        public async Task<User> GetByNickName(string nickName) => await _repository.GetByNickName(nickName);

        public async Task<IEnumerable<User>> GetAll() => await _repository.GetAllAsync();

        public async Task Create(User user) => await _repository.CreateAsync(user);

        public User Get(Func<User, bool> predicate) => _repository.Get(predicate).FirstOrDefault();

        public async Task Update(User user) => await _repository.UpdateAsync(user);

        public User GetWithInclude() => _repository.GetWithInclude(u => u.Role).FirstOrDefault();

        public User GetWithInclude(int id)
        {
            var users = _repository.GetWithInclude(u => u.Role);
            return users.FirstOrDefault(user => user.Id == id);
        }
    }
}
