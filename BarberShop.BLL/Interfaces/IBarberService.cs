using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;
using Microsoft.AspNetCore.Http;

namespace BarberShop.BLL.Interfaces
{
    public interface IBarberService
    {
        public Task<Barber> GetAsync(int id);
        public Task<IEnumerable<Barber>> GetAllAsync();
        public Task CreateAsync(Barber barber);
        public Task UpdateAsync(Barber barber);
        public Task DeleteAsync(int id);
        public Task SaveAvatarAsync(Barber barber, IFormFile image);
    }
}
