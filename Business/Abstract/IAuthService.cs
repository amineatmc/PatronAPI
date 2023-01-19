using Core.Utilities.Result.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegister userForRegister, string phone);
        IDataResult<User> Login(UserForLogin userForLogin);
        IResult UserExists(string phone);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult ChangePassword(UserChangePassword userChangePassword);
        IResult ForgetPassword(UserForgetPassword userForgetPassword);
    }
}
