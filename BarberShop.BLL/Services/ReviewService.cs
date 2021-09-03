using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IGenericRepository<Review> _repository;

        public ReviewService(IGenericRepository<Review> repository)
        {
            _repository = repository;
        }

        public Review GetById(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Review> GetAll()
        {
            return _repository.GetWithInclude(x => x.Barber);
        }

        public void Create(Review review)
        {
            _repository.Create(review);
        }
    }
}
