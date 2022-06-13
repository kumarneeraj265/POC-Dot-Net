using AutoMapper;
using CRUD_PRAC.DTOs.AvailablityDTO;
using CRUD_PRAC.DTOs.UserDTO;
using CRUD_PRAC.Models;

namespace CRUD_PRAC
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Member, GetUserDTO>()
                .ForMember(dest => dest.PEmail, opt => opt.MapFrom(src=> src.Email))
                .ForMember(dest => dest.FName, opt => opt.MapFrom(src => src.Name));
            CreateMap<AddUserDTO, Member>();

            //CreateMap<AvailablityDTO, Availablity> ()
            //      .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId)).ReverseMap();

            //  CreateMap<Slots, Availablity> ()
            //      .ForMember(dest => dest.EarlyMorning, opt => opt.MapFrom(src =>  src.EarlyMorning )).ReverseMap()
            //      .ForMember(dest => dest.Morning, opt => opt.MapFrom(src =>  src.Morning)).ReverseMap()
            //      .ForMember(dest => dest.Evening, opt => opt.MapFrom(src =>  src.Evening)).ReverseMap()
            //      .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day)).ReverseMap();

            CreateMap<AvailablityEntity, Availablity>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId));
            CreateMap<Slots, Availablity>()
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day))
                .ForMember(dest => dest.Slot, opt => opt.MapFrom(src => src.Slot));


            CreateMap<Availablity, AvailablityDTO>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId));
            CreateMap<Member, AvailablityDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<Availablity, Slots>()
               .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day))
               .ForMember(dest => dest.Slot, opt => opt.MapFrom(src => src.Slot));

           

            //CreateMap<AvailablityDTO, FetchedPlayersList>();
            //CreateMap<SlotsDTO, FetchedPlayersList>();


        }
    }
}
