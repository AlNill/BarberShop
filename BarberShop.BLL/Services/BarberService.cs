using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Services
{
    public class BarberService : IBarberService
    {
        private readonly IGenericRepository<Barber> _repository;

        public BarberService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.BarberRepository();
        }

        public Barber GetById(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Barber> GetAll()
        {
            return _repository.GetAll();
        }

        public void Create(Barber barber)
        {
            _repository.Create(barber);
        }

        public void Update(Barber barber)
        {
            _repository.Update(barber);
        }
    }
}