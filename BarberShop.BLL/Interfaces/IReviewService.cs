using System.Collections.Generic;
using BarberShop.BLL.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IReviewService
    {
        Review GetById(int id);
        IEnumerable<Review> GetAll();
    }
}
