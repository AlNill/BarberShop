using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public User GetById(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public void Create(User user)
        {
            _repository.Create(user);
        }
    }
}
