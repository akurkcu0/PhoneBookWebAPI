using AutoMapper;
using PhoneBookWebAPI.Controllers.Models.Dtos;
using PhoneBookWebAPI.Controllers.Models.Entities;

namespace PhoneBookWebAPI.Controllers.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PhoneBookDto,PhoneBook>();
        }
    }
}
