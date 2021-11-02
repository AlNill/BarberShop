using System.Collections.Generic;
using System.Linq;
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
        private readonly IGenericRepository<Barber> _barberRepository;
        private readonly IGenericRepository<User> _userRepository;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.ReviewRepository();
            _barberRepository = unitOfWork.BarberRepository();
            _userRepository = unitOfWork.UserRepository();
        }

        public async Task<Review> GetAsync(int id) => await _repository.GetAsync(id);

        public IEnumerable<Review> GetAll() => _repository.GetWithInclude(x => x.Barber, u => u.User);

        public async Task CreateAsync(int barberId, string reviewText, string contextNickName)
        {
            var review = new Review()
            {
                BarberId = (await _barberRepository.GetAsync(barberId)).Id,
                UserReview = reviewText,
                UserId = _userRepository.Get(u => u.NickName == contextNickName).FirstOrDefault().Id
            };
            await _repository.CreateAsync(review);
        }

        public async Task DeleteAsync(int id, string userRole, string userNickName)
        {
            if (!await _repository.ExistsAsync(id))
                return;
            
            var review = _repository.GetWithInclude(x => x.Barber, u => u.User).FirstOrDefault(x => x.Id == id);
            if (review?.User.NickName != userNickName && userRole != "Admin")
                return;
            await _repository.DeleteAsync(id);
        }

        public async Task UpdateAsync(Review review)
        {
            await _repository.UpdateAsync(review);
        }
    }
}
