using System.Collections.Generic;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IBarberService
    {
        Barber GetById(int id);
        IEnumerable<Barber> GetAll();
        void Create(Barber barber);
        void Update(Barber barber);
    }
}
