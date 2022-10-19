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
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.GetType().Name));
            CreateMap<FamilyMemberDto, FamilyMember>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age));
                //.ForMember(dest => dest.GetType().Name, opt => opt.MapFrom(src => src.Type));

        }
    }
}
