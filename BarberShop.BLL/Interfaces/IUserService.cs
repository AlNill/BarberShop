using System.Collections.Generic;
using BarberShop.BLL.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IUserService
    {
        User GetById(int id);
        IEnumerable<User> GetAll();
        void Create(User user);
    }
}
