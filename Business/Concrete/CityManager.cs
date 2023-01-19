using Business.Abstract;
using Business.Aspects;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CityManager :ICityService
    {
        private readonly ICity _city;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CityManager(ICity city)
        {
            _city = city;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        [SecuredOperation("Admin")]
        public IDataResult<List<CityTbl>> GetCityAllList()
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            foreach (var key in header)
            {
                if (key.Key == "apikey")
                {
                    if (key.Value.ToString() == apiKey.keyValue.ToString())
                    {
                        return new SuccessDataResult<List<CityTbl>>(_city.GetList(), "Şehirler Başarıyla Listelendi.", 200);
                    }
                    else
                    {
                        return new ErrorDataResult<List<CityTbl>>(null, "Api ye Erişiminiz Bulunmuyor", 401);
                    }
                }
            }
            return new ErrorDataResult<List<CityTbl>>(null, "Lütfen Doğru Bir Şifre Giriniz", 401);

        }
    }
}
