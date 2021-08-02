using System.Collections.Generic;
using BarberShop.BLL.Models;
using BarberShop.DAL.Common;

namespace BarberShop.BLL.Services
{
    public interface IBarberService
    {
        Barber GetById(int id);
        IEnumerable<Barber> GetAll();
    }
}
