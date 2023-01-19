using Entities.Dtos;
using IdentityNumberVerifyService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace PatronApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityNumberVerifyController : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Verify(IdentityNumberVerifyDto verifyDto)
        {
            var client = new IdentityNumberVerifyService.KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            var response = await client.TCKimlikNoDogrulaAsync(Convert.ToInt64(verifyDto.IdentityNumber), verifyDto.Name.ToUpper(), verifyDto.Surname.ToUpper(), verifyDto.YearOfBirth);
            var result = response.Body.TCKimlikNoDogrulaResult;
            if (result == true)
            {
                return Ok(JsonConvert.SerializeObject(result));
            }
            return BadRequest(result);

        }
    }
}
