using RealEstateAPI.Models;

namespace RealEstateAPI.DTOs
{
    public class PropertyResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ListingType ListingType { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int CarSpots { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new List<string>();
        public bool IsFavorite { get; set; }
    }

    public class PropertyFilter
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinBedrooms { get; set; }
        public int? MaxBedrooms { get; set; }
        public string? Suburb { get; set; }
        public ListingType? ListingType { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class PropertyListResponse
    {
        public List<PropertyResponse> Properties { get; set; } = new List<PropertyResponse>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
