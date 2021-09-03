using System.Collections.Generic;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IReviewService
    {
        Review GetById(int id);
        IEnumerable<Review> GetAll();
        void Create(Review review);
    }
}
