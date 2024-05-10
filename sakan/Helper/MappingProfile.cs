using AutoMapper;
using sakan.DTOs;
using sakan.Models;

namespace sakan.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student,RoommatesDTO>().ReverseMap();
            CreateMap<CreateStudentDTO, Student > ().ForMember(a => a.Photo, b => b.Ignore()).ReverseMap();

            CreateMap<HouseOwner, CreateOwnerDTO>().ReverseMap();
            CreateMap<CreateOwnerDTO, HouseOwner>().ReverseMap();

            CreateMap<House,GetHousesDTO>().ReverseMap();
            CreateMap<House, GetAllHousesDTO>().ReverseMap();

            CreateMap<CreateHouseDTO, House>().ForMember(a => a.HouseOwnerID, b => b.Ignore())
                .ForMember(a => a.Photo, b => b.Ignore()).ForMember(a => a.Time, b => b.Ignore()).ReverseMap();

        }
    }
}
