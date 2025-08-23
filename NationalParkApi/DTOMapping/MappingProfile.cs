using AutoMapper;
using NationalParkApi.Models;
using NationalParkApi.Models.DTOs;

namespace NationalParkApi.DTOMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<TrailDto, Trail>().ReverseMap();
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();

        }
       
    }
}
