using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PatronApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CityController(ICityService cityService, IHttpContextAccessor httpContextAccessor)
        {
            _cityService = cityService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("[action]")]
        public IActionResult CityAllList()
        {
            string role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ss = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
          // var currentUserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var result=_cityService.GetCityAllList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
