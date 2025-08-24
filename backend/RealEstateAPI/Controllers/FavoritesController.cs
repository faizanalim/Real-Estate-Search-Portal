using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.DTOs;
using RealEstateAPI.Services;
using System.Security.Claims;

namespace RealEstateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public FavoritesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpPost("{propertyId}")]
        public async Task<ActionResult<object>> ToggleFavorite(int propertyId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var isFavorite = await _propertyService.ToggleFavoriteAsync(propertyId, userId);
            return Ok(new { IsFavorite = isFavorite });
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyResponse>>> GetFavorites()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var favorites = await _propertyService.GetUserFavoritesAsync(userId);
            return Ok(favorites);
        }
    }
}
