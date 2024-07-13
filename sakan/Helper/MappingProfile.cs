using AutoMapper;
using sakan.DTOs;
using sakan.Models;

namespace sakan.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student,RoommatesDTO>().ForMember(a => a.Photo, b => b.Ignore()).ReverseMap();
            CreateMap<CreateStudentDTO, Student > ().ForMember(a => a.Photo, b => b.Ignore()).ReverseMap();

            CreateMap<HouseOwner, CreateOwnerDTO>().ReverseMap();
            CreateMap<CreateOwnerDTO, HouseOwner>().ReverseMap();


            CreateMap<House,GetHousesDTO>().ReverseMap();

            CreateMap<House, GetAllHousesDTO>().ForMember(a => a.Photo1, b => b.Ignore()).ForMember(a => a.Photo2, b => b.Ignore())
               .ForMember(a => a.Photo3, b => b.Ignore()).ForMember(a => a.Photo4, b => b.Ignore()).ReverseMap();

            CreateMap<CreateHouseDTO, House>().ForMember(a => a.HouseOwnerID, b => b.Ignore())
               .ForMember(a => a.Photo1, b => b.Ignore()).ForMember(a => a.Photo2, b => b.Ignore())
               .ForMember(a => a.Photo3, b => b.Ignore()).ForMember(a => a.Photo4, b => b.Ignore())
               .ReverseMap();

            CreateMap<CreateHouseDTO, AdminHouse>().ForMember(a => a.HouseOwnerID, b => b.Ignore())
              .ForMember(a => a.Photo1, b => b.Ignore()).ForMember(a => a.Photo2, b => b.Ignore())
              .ForMember(a => a.Photo3, b => b.Ignore()).ForMember(a => a.Photo4, b => b.Ignore())
              .ReverseMap();


            CreateMap<AdminHouse, House>().ReverseMap();

        }
    }
}
