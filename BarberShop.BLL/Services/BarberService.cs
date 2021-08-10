using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.BLL.Models;
using BarberShop.DAL.Common;

namespace BarberShop.BLL.Services
{
    public class BarberService : IBarberService
    {
        private readonly IRepository<Barber> _repository;

        public BarberService(IRepository<Barber> repository)
        {
            _repository = repository;
        }

        public Barber GetById(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Barber> GetAll()
        {
            return _repository.GetAll();
        }
    }
}