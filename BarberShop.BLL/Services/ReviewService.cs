using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IGenericRepository<Review> _repository;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.ReviewRepository();
        }

        public Review GetById(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Review> GetAll()
        {
            return _repository.GetWithInclude(x => x.Barber, u => u.User);
        }

        public void Create(Review review)
        {
            _repository.Create(review);
        }
    }
}
