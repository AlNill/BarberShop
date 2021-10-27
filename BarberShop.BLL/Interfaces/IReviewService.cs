using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IReviewService
    {
        public Task<Review> GetById(int id);
        public IEnumerable<Review> GetAll();
        public Task Create(Review review);
    }
}
