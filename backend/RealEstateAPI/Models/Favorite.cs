using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int PropertyId { get; set; }
        
        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual Property Property { get; set; } = null!;
    }
}
