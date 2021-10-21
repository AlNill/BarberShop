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
        private readonly IGenericRepository<User> _repository;

        public UserService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.UserRepository();
        }

        public async Task<int> GetCount()
        {
            return await _repository.GetCount();
        }

        public IEnumerable<User> GetRange(int skipPos = 0, int count = 10)
        {
            return _repository.GetRange(skipPos, count);
        }

        public async Task<User> GetById(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAll();
        }

        public void Create(User user)
        {
            _repository.Create(user);
        }

        public User Get(Func<User, bool> predicate)
        {
            return _repository.Get(predicate).FirstOrDefault();
        }

        public void Update(User user)
        {
            _repository.Update(user);
        }

        public User GetWithInclude()
        {
            return _repository.GetWithInclude(u => u.Role).FirstOrDefault();
        }

        public User GetWithInclude(int id)
        {
            var users = _repository.GetWithInclude(u => u.Role);
            return users.FirstOrDefault(user => user.Id == id);
        }
    }
}
