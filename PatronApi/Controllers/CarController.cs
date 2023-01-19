using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PatronApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("[action]/{userId}")]
        public IActionResult GetListByUserId(int userId)
        {
            var userCarToList = _carService.GetList(userId);
            if (userCarToList.Success)
            {             
                return Ok(userCarToList);       
            }
            return BadRequest(userCarToList);
        }
        [HttpPost("[action]")]
        public IActionResult CarInsert([FromForm] CarAddDto carAddDto)
        {
            string userName = User.FindFirst(ClaimTypes.Name).Value;

            var carExists = _carService.CarExists(carAddDto.NumberPlate);
            if (!carExists.Success)
            {
                return BadRequest(carExists);
            }
            var result = _carService.Add(carAddDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public IActionResult CarUpdate([FromForm] CarUpdateDto carUpdateDto)
        {
            var updateCar = _carService.Update(carUpdateDto);
            if (updateCar.Success)
            {
                return Ok(updateCar);
            }
            return BadRequest(updateCar.Message);
        }
        
        [HttpGet("[action]")]
        public IActionResult GetCarList()
        {
            var updateCar = _carService.GetCarList();
            if (updateCar.Success)
            {
                return Ok(updateCar);
            }
            return BadRequest(updateCar);
        }

        [HttpGet("[action]/{cityId}")]
        public IActionResult GetCarByCarId(int cityId)
        {
            string userName = User.FindFirst(ClaimTypes.Name).Value;

            var updateCar = _carService.GetCarByCarId(cityId);
            if (updateCar.Success)
            {
                return Ok(updateCar);
            }
            return BadRequest(updateCar.Message);
        }

    }
}
