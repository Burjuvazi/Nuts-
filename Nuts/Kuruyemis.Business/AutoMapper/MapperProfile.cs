using AutoMapper;
using Kuruyemis.Entities.Concrete;
using Kuruyemis.Entities.Concrete.Dtos;

namespace Kuruyemis.Business.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
             CreateMap<AppUser,RegisterDto>().ReverseMap();
        }
    }
}
