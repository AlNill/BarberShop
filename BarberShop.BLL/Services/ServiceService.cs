using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Services
{
    public class ServiceService: IServiceService
    {
        private readonly IGenericRepository<Service> _repository;

        public ServiceService(IGenericRepository<Service> repository)
        {
            _repository = repository;
        }

        public Service GetById(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Service> GetAll()
        {
            return _repository.GetAll();
        }

        public void Create(Service service)
        {
            _repository.Create(service);
        }

        public void Update(Service service)
        {
            _repository.Update(service);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
