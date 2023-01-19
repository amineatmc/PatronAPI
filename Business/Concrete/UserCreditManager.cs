using Business.Abstract;
using Business.Aspects;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserCreditManager : IUserCreditService
    {
        IUserCreditDal _userCreditDal;
        IUserService _userService;
        IHttpContextAccessor _contextAccessor;

        public UserCreditManager(IUserCreditDal userCreditDal, IUserService userService,IHttpContextAccessor httpContextAccessor)
        {
            _userCreditDal = userCreditDal;
            _userService = userService;
            _contextAccessor = httpContextAccessor;
        }


        [SecuredOperation("Admin")]
        public IDataResult<UserCredit>Add(UserCreditAddDto userCredit/*,int id*/)
        {
            //var result = _userService.GetbyId(id);
            var header = _contextAccessor.HttpContext.Request.Headers.ToList();
            foreach (var key in header)
            {
                if (key.Key == "apikey")
                {
                    if (key.Value.ToString() == apiKey.keyValue.ToString())
                    {
                        UserCredit useCred = new UserCredit()
                        {
                            UserId = userCredit.Credit,
                            Credit = userCredit.Credit
                        };
                        _userCreditDal.Add(useCred);
                        return new SuccessDataResult<UserCredit>();
                 
                    }

                }

            }
            return new ErrorDataResult<UserCredit>(null, "Api ye Erişiminiz Bulunmuyor", 401);

        }
    }
}
