using System.Collections.Generic;
using BarberShop.BLL.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IBarberService
    {
        Barber GetById(int id);
        IEnumerable<Barber> GetAll();
    }
}
