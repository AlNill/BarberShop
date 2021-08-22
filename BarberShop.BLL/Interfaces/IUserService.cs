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
    }
}
