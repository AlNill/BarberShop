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

        public async Task<int> GetCountAsync() => await _repository.GetCountAsync();

        public IEnumerable<User> GetRange(int skipPos = 0, int count = 10) => 
            _repository.GetRange(skipPos, count);

        public async Task DeleteAsync(int id)
        {
            if (await _repository.GetAsync(id) != null)
                await _repository.DeleteAsync(id);
        }

        public async Task<User> GetAsync(int id) => await _repository.GetAsync(id);

        public async Task<User> GetByNickNameAsync(string nickName) => await _repository.GetByNickName(nickName);

        public async Task<IEnumerable<User>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task CreateAsync(string name, string fatherName, string surname, 
            string nickname, string password, string email)
        {
            if (Get(u => u.NickName == nickname) != null)
                throw new ArgumentException($"User with nickname {nickname} exists");

            var user = new User()
            {
                Name = name,
                FatherName = fatherName,
                Surname = surname,
                NickName = nickname,
                Password = password,
                Email = email
            };
            await _repository.CreateAsync(user);
        }

        public User Get(Func<User, bool> predicate) => _repository.Get(predicate).FirstOrDefault();

        public async Task UpdateAsync(User user) => await _repository.UpdateAsync(user);

        public User GetWithInclude(int id)
        {
            var users = _repository.GetWithInclude(u => u.Role);
            return users.FirstOrDefault(user => user.Id == id);
        }
    }
}
