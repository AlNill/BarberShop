using System.Collections.Generic;
using BarberShop.BLL.Models;

namespace BarberShop.BLL.Services
{
    public interface IReviewService
    {
        Review GetById(int id);
        IEnumerable<Review> GetAll();
    }
}
