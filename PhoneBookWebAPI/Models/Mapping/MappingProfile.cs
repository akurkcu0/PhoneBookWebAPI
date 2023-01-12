using AutoMapper;
using PhoneBookWebAPI.Models.Dtos;
using PhoneBookWebAPI.Models.Entities;

namespace PhoneBookWebAPI.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PhoneBookDto, PhoneBook>();
        }
    }
}
