using Business.Abstract;
using Core.Extensions;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace PatronApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCreditController : Controller
    {
        IUserCreditService _userCreditService;
        IHttpContextAccessor _contextAccessor;
        public UserCreditController(IUserCreditService userCreditService, IHttpContextAccessor contextAccessor)
        {
            _userCreditService = userCreditService;
            _contextAccessor = contextAccessor;
            var ss = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        }

        [HttpPost("add")]
        public IActionResult Add(UserCreditAddDto userCredit)
        {
            
            string role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ss = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
           // var currentUserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);


            var result = _userCreditService.Add(userCredit/*,userId*/);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
