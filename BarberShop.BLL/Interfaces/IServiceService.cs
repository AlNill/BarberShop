using System.Collections.Generic;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IServiceService
    {
        Service GetById(int id);
        IEnumerable<Service> GetAll();
        void Create(Service service);
        void Update(Service service);
        void Delete(int id);
        public IEnumerable<Service> GetServicesForSubTitle(string subTitle);
        public IEnumerable<Service> AdvancedSearch(Service serviceParams);
    }
}
