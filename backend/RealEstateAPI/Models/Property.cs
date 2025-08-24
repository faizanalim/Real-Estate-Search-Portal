using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models
{
    public enum ListingType
    {
        Rent,
        Sale
    }

    public class Property
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Address { get; set; } = string.Empty;
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public ListingType ListingType { get; set; }
        
        [Required]
        public int Bedrooms { get; set; }
        
        [Required]
        public int Bathrooms { get; set; }
        
        [Required]
        public int CarSpots { get; set; }
        
        public string Description { get; set; } = string.Empty;
        
        // Store image URLs as JSON string
        public string ImageUrls { get; set; } = string.Empty;
        
        // Navigation property for favorites
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
