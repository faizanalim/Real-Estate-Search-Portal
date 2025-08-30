using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.DTOs;
using RealEstateAPI.Services;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace RealEstateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ILogger<FavoritesController> _logger;

        public FavoritesController(IPropertyService propertyService, ILogger<FavoritesController> logger)
        {
            _propertyService = propertyService;
            _logger = logger;
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
            _logger.LogInformation("GetFavorites called. User authenticated: {IsAuthenticated}", User.Identity?.IsAuthenticated);
            _logger.LogInformation("User claims: {Claims}", string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}")));
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation("UserId from claim: {UserId}", userIdClaim);
            
            if (!int.TryParse(userIdClaim, out int userId))
            {
                _logger.LogWarning("Failed to parse userId from claim: {UserIdClaim}", userIdClaim);
                return Unauthorized();
            }

            var favorites = await _propertyService.GetUserFavoritesAsync(userId);
            return Ok(favorites);
        }
    }
}
