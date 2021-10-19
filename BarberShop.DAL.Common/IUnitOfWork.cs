using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;

namespace BarberShop.DAL.Common
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Barber> BarberRepository();
        public IGenericRepository<BusyRecord> BusyRecordRepository();
        public IGenericRepository<Log> LogRepository();
        public IOfferRepository OfferRepository();
        public IGenericRepository<Review> ReviewRepository();
        public IGenericRepository<User> UserRepository();
    }
}
