using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.DTOs;
using RealEstateAPI.Services;
using System.Security.Claims;

namespace RealEstateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<ActionResult<PropertyListResponse>> GetProperties([FromQuery] PropertyFilter filter)
        {
            // Get user ID from token if authenticated
            int? userId = null;
            if (User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int id))
                {
                    userId = id;
                }
            }

            var result = await _propertyService.GetPropertiesAsync(filter, userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyResponse>> GetProperty(int id)
        {
            // Get user ID from token if authenticated
            int? userId = null;
            if (User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int userIdValue))
                {
                    userId = userIdValue;
                }
            }

            var property = await _propertyService.GetPropertyByIdAsync(id, userId);
            if (property == null)
            {
                return NotFound();
            }

            return Ok(property);
        }
    }
}
