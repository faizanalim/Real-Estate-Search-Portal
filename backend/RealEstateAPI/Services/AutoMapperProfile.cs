using AutoMapper;
using RealEstateAPI.DTOs;
using RealEstateAPI.Models;
using System.Text.Json;

namespace RealEstateAPI.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Property mappings
            CreateMap<Property, PropertyResponse>()
                .ForMember(dest => dest.ImageUrls, opt => opt.Ignore());

            // User mappings
            CreateMap<User, AuthResponse>()
                .ForMember(dest => dest.Token, opt => opt.Ignore());

            // Register request to User (for creating new users)
            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Favorites, opt => opt.Ignore());
        }
    }
}
