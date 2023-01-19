using Business.Abstract;
using Business.Aspects;
using Business.Constans;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
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
    public class CarManager : ICarService
    {
        private readonly ICar _car;
        private readonly IUser _user;
        private readonly ITransactionsService _transactionsService;
        private readonly IFileService _fileService;
        private readonly IPermitImageService _permitImageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarManager(ICar car, IFileService fileService, IPermitImageService permitImageService, IUser user, ITransactionsService transactionsService)
        {
            _car = car;
            _fileService = fileService;
            _permitImageService = permitImageService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _user = user;
            _transactionsService = transactionsService;
        }
        //[SecuredOperation("CarInsert.Add")]
      //  [TransactionScopeAspect]
        public IResult Add(CarAddDto carAddDto)
        {

            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            foreach (var key in header)
            {
                if (key.Key == "apikey")
                {
                    if (key.Value.ToString() == apiKey.keyValue.ToString())
                    {
                        if (carAddDto != null)
                        {
                            var car = new CarTbl()
                            {
                                NumberPlate = carAddDto.NumberPlate,
                                CityID = carAddDto.CityID,
                                UserID = carAddDto.UserID,
                                RecipesID=carAddDto.RecipeID,
                                AddedAt = DateTime.Now,
                                IsActive = false
                            };

                            if (carAddDto.CarImages != null)
                            {
                                _car.Add(car);
                                foreach (var item in carAddDto.CarImages)
                                {
                                    string fileName = _fileService.FileSaveToServer(item, "./Content/car_img/");
                                    PermitImageTbl permitImageTbl = new()
                                    {
                                        CarID = car.ID,
                                        PermitPath = fileName
                                    };

                                    _permitImageService.Add(permitImageTbl);
                                }
                            }
                            else
                            {
                                return new ErrorResult(Messages.CarListNull, 404);
                            }
                            var user = _user.Get(p => p.UserID == carAddDto.UserID);
                            var actionAdd = new TransactionsDto()
                            {
                                CreatedDate = DateTime.Now,
                                DataID = carAddDto.UserID,
                                TableName = "CarTbl",
                                DataDescription = $"Telefon Numarası: {user.UserPhone} , {user.UserName + " " + user.UserSurname} isimli ve numaralı bu kullanıcı tarih: {DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute} Plaka Ekleme İslemi Yaptı.",
                                UserLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                            };
                            _transactionsService.Add(actionAdd);
                            return new SuccessDataResult<CarTbl>(car, Messages.SuccesFullCar, 200);

                        }
                        return new ErrorResult(Messages.CarListNull, 404);
                    }
                    else
                    {
                        return new ErrorResult("Api ye Erişiminiz Bulunmuyor", 401);
                    }
                }
            }
            return new ErrorResult("Lütfen Doğru Bir Şifre Giriniz", 401);

        }

        public IResult CarExists(string numberPlate)
        {
            var result = _car.Get(p => p.NumberPlate == numberPlate);
            if (result != null)
            {
                return new ErrorResult(Messages.CarNotNull, 400);
            }
            return new SuccessResult();
        }


        public IResult Update(CarUpdateDto carUpdateDto)
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            foreach (var key in header)
            {
                if (key.Key == "apikey")
                {
                    if (key.Value.ToString() == apiKey.keyValue.ToString())
                    {
                        if (carUpdateDto != null)
                        {
                            var car = _car.Get(p => p.ID == carUpdateDto.ID);

                            car.NumberPlate = carUpdateDto.NumberPlate;
                            car.CityID = carUpdateDto.CityID;
                            car.UserID = carUpdateDto.UserID;
                            car.RecipesID = carUpdateDto.RecipeID;
                            car.AddedAt = DateTime.Now;
                            car.IsActive = carUpdateDto.IsActive;

                            _car.Update(car);
                            if (carUpdateDto.CarImages != null)
                            {
                                var permitImages = _permitImageService.GetListByCarId(car.ID);
                                foreach (var item in permitImages)
                                {
                                    string path = "./Content/car_img/" + item.PermitPath;
                                    _fileService.FileDeleteToServer(path);
                                    _permitImageService.Delete(item);
                                }

                                foreach (var item in carUpdateDto.CarImages)
                                {
                                    string fileName = _fileService.FileSaveToServer(item, "./Content/car_img/");
                                    PermitImageTbl permitImageTbl = new()
                                    {
                                        CarID = car.ID,
                                        PermitPath = fileName
                                    };
                                    _permitImageService.Add(permitImageTbl);
                                }
                            }

                            var user = _user.Get(p => p.UserID == carUpdateDto.UserID);
                            var actionAdd = new TransactionsDto()
                            {
                                CreatedDate = DateTime.Now,
                                DataID = carUpdateDto.UserID,
                                TableName = "CarTbl",
                                DataDescription = $"Telefon Numarası: {user.UserPhone} , {user.UserName + " " + user.UserSurname} isimli ve numaralı bu kullanıcı tarih: {DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute} Plaka Guncelleme İslemi Yaptı.",
                                UserLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                            };
                            _transactionsService.Add(actionAdd);

                            return new SuccessResult(Messages.CarUpdate, 200);
                        }
                        return new ErrorResult(Messages.CarUpdateFail, 400);
                    }
                    else
                    {
                        return new ErrorResult("Api ye Erişiminiz Bulunmuyor", 401);
                    }
                }
            }
            return new ErrorResult("Lütfen Doğru Bir Şifre Giriniz", 401);

        }

        public IDataResult<List<CarListDto>> GetCarList()
        {
            return new SuccessDataResult<List<CarListDto>>(_car.GetCarList());
        }

        public IDataResult <List<CarListDto>> GetCarByCarId(int cityId)
        {
            return new SuccessDataResult<List<CarListDto>>(_car.GetCarList().Where(p => p.CityID == cityId).ToList());
        }

        public IDataResult<List<CarListDto>> GetList(int userId)
        {
            return new SuccessDataResult<List<CarListDto>>(_car.GetCarList().Where(p => p.UserID == userId).ToList());
        }
    }
}
