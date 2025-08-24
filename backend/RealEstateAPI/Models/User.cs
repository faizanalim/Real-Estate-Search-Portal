using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        // Navigation property for favorites
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
