using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IReviewService
    {
        public Task<Review> GetAsync(int id);
        public IEnumerable<Review> GetAll();
        public Task CreateAsync(int barberId, string reviewText, string contextNickName);
        public Task DeleteAsync(int id, string userRole, string userNickName);
        public Task UpdateAsync(Review review);
    }
}
