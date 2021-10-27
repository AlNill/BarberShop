using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IBarberService
    {
        public Task<Barber> GetById(int id);
        public Task<IEnumerable<Barber>> GetAll();
        public Task Create(Barber barber);
        public Task Update(Barber barber);
    }
}
