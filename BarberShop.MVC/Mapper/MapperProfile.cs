using AutoMapper;
using BarberShop.BLL.Models;
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
        }
    }
}
