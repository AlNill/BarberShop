using AutoMapper;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Models;

namespace BarberShop.MVC.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Person, PersonModel>().ReverseMap();
            CreateMap<Barber, BarberModel>().ReverseMap();
            CreateMap<Review, ReviewModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<BusyRecord, BusyRecordModel>().ReverseMap();
            CreateMap<Role, RoleModel>().ReverseMap();
        }
    }
}
