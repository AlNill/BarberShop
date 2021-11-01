using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;

namespace BarberShop.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IGenericRepository<Review> _repository;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.ReviewRepository();
        }

        public async Task<Review> GetById(int id)
        {
            return await _repository.Get(id);
        }

        public IEnumerable<Review> GetAll()
        {
            return _repository.GetWithInclude(x => x.Barber, u => u.User);
        }

        public async Task Create(Review review)
        {
            await _repository.CreateAsync(review);
        }
    }
}
