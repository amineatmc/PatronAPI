using Business.Abstract;
using Business.VerimorOtp;
using Core.Utilities.IoC;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatronApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICheckOtpSend _checkOtpSend;
        public AuthController(IAuthService authService, ICheckOtpSend checkOtpSend)
        {
            _authService = authService;
            _checkOtpSend = checkOtpSend;
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromForm] UserForRegister userForRegister)
        {
            var userExists = _authService.UserExists(userForRegister.UserPhone);
            if (!userExists.Success)
            {
                return BadRequest(userExists);
            }
            var registerResult = _authService.Register(userForRegister, userForRegister.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            //if (registerResult.Success)
            //{
            //    return Ok(registerResult);
            //}
            return BadRequest(result);

        }
        [HttpPost("[action]")]
        public IActionResult Login(UserForLogin userForLogin)
        {
            var userToLogin = _authService.Login(userForLogin);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            if (userToLogin.Data.IsActive == true)
            {
                var result = _authService.CreateAccessToken(userToLogin.Data);
                if (result.Success)
                {                 
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Kullanıcı Pasif Durumda.");


        }
        [HttpPost("[action]")]
        public IActionResult ChangePassword(UserChangePassword userChangePassword)
        {
            var result = _authService.ChangePassword(userChangePassword);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public IActionResult ForgetPassword(UserForgetPassword userForgetPassword)
        {
            var result = _authService.ForgetPassword(userForgetPassword);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public IActionResult OtpSend(CheckOtpDto checkOtpDto)
        {
            var result = _checkOtpSend.CheckOtpSendMethod(checkOtpDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public IActionResult OtpVerification(CheckOtpDto checkOtpVerify)
        {
            var result = _checkOtpSend.CheckOtpVerification(checkOtpVerify);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
