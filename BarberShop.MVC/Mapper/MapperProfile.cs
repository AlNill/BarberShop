using AutoMapper;
using BarberShop.BLL.Models;
using BarberShop.MVC.Models;

namespace BarberShop.MVC.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<Barber, BarberModel>().ReverseMap();
        }
    }
}
