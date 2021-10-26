using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;

namespace BarberShop.BLL.Services
{
    public class BarberService : IBarberService
    {
        private readonly IGenericRepository<Barber> _repository;

        public BarberService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.BarberRepository();
        }

        public async Task<Barber> GetById(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<Barber>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Create(Barber barber)
        {
            await _repository.Create(barber);
        }

        public async Task Update(Barber barber)
        {
            await _repository.Update(barber);
        }
    }
}