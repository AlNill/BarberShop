using BarberShop.DAL.Common.Models;

namespace BarberShop.DAL.Common
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Barber> BarberRepository();
        public IGenericRepository<BusyRecord> BusyRecordRepository();
        public IGenericRepository<Log> LogRepository();
        public IGenericRepository<Offer> OfferRepository();
        public IGenericRepository<Review> ReviewRepository();
        public IGenericRepository<User> UserRepository();
    }
}
