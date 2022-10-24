using AutoMapper;
using FamiliesApi.Models;
using FamiliesApi.DTOs;
namespace FamiliesApi {
    public class AutoMapperProfile: Profile {
        public AutoMapperProfile() {
            CreateMap<Family, FamilyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Members));
            CreateMap<FamilyMember, FamilyMemberDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.FamilyId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.GetType().Name)).ReverseMap();
            CreateMap<Father, FatherDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.FamilyId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.GetType().Name))
                .ForMember(dest => dest.FavoriteCar, opt => opt.MapFrom(src => src.FavoriteCar)).ReverseMap();
            CreateMap<Mother, MotherDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.FamilyId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.GetType().Name))
                .ForMember(dest => dest.FavoriteDish, opt => opt.MapFrom(src => src.FavoriteDish)).ReverseMap();
            CreateMap<Child, ChildDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.FamilyId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.GetType().Name))
                .ForMember(dest => dest.FavoriteToy, opt => opt.MapFrom(src => src.FavoriteToy)).ReverseMap();
        }
    }
}
