using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Data;
using RealEstateAPI.DTOs;
using RealEstateAPI.Models;
using System.Text.Json;

namespace RealEstateAPI.Services
{
    public interface IPropertyService
    {
        Task<PropertyListResponse> GetPropertiesAsync(PropertyFilter filter, int? userId = null);
        Task<PropertyResponse?> GetPropertyByIdAsync(int id, int? userId = null);
        Task<bool> ToggleFavoriteAsync(int propertyId, int userId);
        Task<List<PropertyResponse>> GetUserFavoritesAsync(int userId);
    }

    public class PropertyService : IPropertyService
    {
        private readonly RealEstateDbContext _context;
        private readonly IMapper _mapper;

        public PropertyService(RealEstateDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PropertyListResponse> GetPropertiesAsync(PropertyFilter filter, int? userId = null)
        {
            var query = _context.Properties.AsQueryable();

            // Apply filters
            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            if (filter.MinBedrooms.HasValue)
                query = query.Where(p => p.Bedrooms >= filter.MinBedrooms.Value);

            if (filter.MaxBedrooms.HasValue)
                query = query.Where(p => p.Bedrooms <= filter.MaxBedrooms.Value);

            if (!string.IsNullOrEmpty(filter.Suburb))
                query = query.Where(p => p.Address.Contains(filter.Suburb));

            if (filter.ListingType.HasValue)
                query = query.Where(p => p.ListingType == filter.ListingType.Value);

            if (filter.MinSquareFeet.HasValue)
                query = query.Where(p => p.SquareFeet >= filter.MinSquareFeet.Value);

            if (filter.MaxSquareFeet.HasValue)
                query = query.Where(p => p.SquareFeet <= filter.MaxSquareFeet.Value);

            // Get total count for pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var properties = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            // Map to DTOs and check favorites
            var propertyResponses = new List<PropertyResponse>();
            foreach (var property in properties)
            {
                var propertyResponse = _mapper.Map<PropertyResponse>(property);
                
                // Handle ImageUrls mapping
                if (!string.IsNullOrEmpty(property.ImageUrls))
                {
                    try
                    {
                        propertyResponse.ImageUrls = JsonSerializer.Deserialize<List<string>>(property.ImageUrls) ?? new List<string>();
                    }
                    catch
                    {
                        propertyResponse.ImageUrls = new List<string>();
                    }
                }
                
                if (userId.HasValue)
                {
                    propertyResponse.IsFavorite = await _context.Favorites
                        .AnyAsync(f => f.UserId == userId.Value && f.PropertyId == property.Id);
                }
                
                propertyResponses.Add(propertyResponse);
            }

            return new PropertyListResponse
            {
                Properties = propertyResponses,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize)
            };
        }

        public async Task<PropertyResponse?> GetPropertyByIdAsync(int id, int? userId = null)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return null;

            var propertyResponse = _mapper.Map<PropertyResponse>(property);
            
            // Handle ImageUrls mapping
            if (!string.IsNullOrEmpty(property.ImageUrls))
            {
                try
                {
                    propertyResponse.ImageUrls = JsonSerializer.Deserialize<List<string>>(property.ImageUrls) ?? new List<string>();
                }
                catch
                {
                    propertyResponse.ImageUrls = new List<string>();
                }
            }
            
            if (userId.HasValue)
            {
                propertyResponse.IsFavorite = await _context.Favorites
                    .AnyAsync(f => f.UserId == userId.Value && f.PropertyId == id);
            }

            return propertyResponse;
        }

        public async Task<bool> ToggleFavoriteAsync(int propertyId, int userId)
        {
            var existingFavorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PropertyId == propertyId);

            if (existingFavorite != null)
            {
                // Remove from favorites
                _context.Favorites.Remove(existingFavorite);
                await _context.SaveChangesAsync();
                return false;
            }
            else
            {
                // Add to favorites
                var favorite = new Favorite
                {
                    UserId = userId,
                    PropertyId = propertyId
                };
                
                _context.Favorites.Add(favorite);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<PropertyResponse>> GetUserFavoritesAsync(int userId)
        {
            var favorites = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Property)
                .ToListAsync();

            var propertyResponses = new List<PropertyResponse>();
            foreach (var favorite in favorites)
            {
                var propertyResponse = _mapper.Map<PropertyResponse>(favorite.Property);
                
                // Handle ImageUrls mapping
                if (!string.IsNullOrEmpty(favorite.Property.ImageUrls))
                {
                    try
                    {
                        propertyResponse.ImageUrls = JsonSerializer.Deserialize<List<string>>(favorite.Property.ImageUrls) ?? new List<string>();
                    }
                    catch
                    {
                        propertyResponse.ImageUrls = new List<string>();
                    }
                }
                
                propertyResponse.IsFavorite = true;
                propertyResponses.Add(propertyResponse);
            }

            return propertyResponses;
        }
    }
}
