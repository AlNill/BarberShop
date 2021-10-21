using System;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;
using BarberShop.DAL.EF.Contexts;
using BarberShop.DAL.EF.Repositories;

namespace BarberShop.DAL.EF
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IGenericRepository<Barber> _barberRepository;
        private IGenericRepository<BusyRecord> _busyRecordRepository;
        private IGenericRepository<Log> _logRepository;
        private IOfferRepository _offerRepository;
        private IGenericRepository<Review> _reviewRepository;
        private IGenericRepository<User> _userRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IGenericRepository<Barber> BarberRepository() => _barberRepository ??= new GenericRepository<Barber>(_context);
        public IGenericRepository<BusyRecord> BusyRecordRepository() => _busyRecordRepository ??= new GenericRepository<BusyRecord>(_context);
        public IGenericRepository<Log> LogRepository() => _logRepository ??= new GenericRepository<Log>(_context);
        public IOfferRepository OfferRepository() => _offerRepository ??= new OfferRepository(_context);
        public IGenericRepository<Review> ReviewRepository() => _reviewRepository ??= new GenericRepository<Review>(_context);
        public IGenericRepository<User> UserRepository() => _userRepository ??= new GenericRepository<User>(_context);
    }
}
