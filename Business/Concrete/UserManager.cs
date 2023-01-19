using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
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
    public class UserManager : IUserService
    {
        private readonly IUser _user;
        private readonly IPermitImageService _permitImageService;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string fileName;
        public UserManager(IUser user, IPermitImageService permitImageService, IFileService fileService)
        {
            _user = user;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _permitImageService = permitImageService;
            _fileService = fileService;
        }

        [ValidationAspect(typeof(UserValidator))]
        public void Add(User user)
        {
            _user.Add(user);
        }

        public IDataResult<User> GetbyId(int id)
        {
            return new SuccessDataResult<User>(_user.Get(x => x.UserID == id));
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _user.GetClaims(user);
        }

        public IDataResult<List<User>> GetList(int userId)
        {
            //var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            //foreach (var key in header)
            //{
                //if (key.Key == "apikey")
                //{
                    //if (key.Value.ToString() == apiKey.keyValue.ToString())
                    //{
                        return new SuccessDataResult<List<User>>(_user.GetListByUserId(userId), "Kullanıcı Listelenmesi Başarılı", 200);
                    //}
                    //else
                    //{
                    //    return new ErrorDataResult<List<User>>(null, "Lütfen Doğru Bir Şifre Giriniz", 401);
                    //}
            //    }
            //}
            //return new ErrorDataResult<List<User>>(null, "Api ye Erişiminiz Bulunmuyor", 401);
        }

        public User GetUserPhone(string phone)
        {
            return _user.Get(x => x.UserPhone == phone);
        }

        public IResult Update(UserUpdateDto userUpdate)
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            foreach (var key in header)
            {
                if (key.Key == "apikey")
                {
                    if (key.Value.ToString() == apiKey.keyValue.ToString())
                    {
                        if (userUpdate != null)
                        {
                            try
                            {
                                var userData = _user.Get(x => x.UserPhone == userUpdate.UserPhone);
                                userData.UserName = userUpdate.Username;
                                userData.UserSurname = userUpdate.Usersurname;
                                userData.UserPhone = userUpdate.UserPhone;
                                userData.DateOfBirth = userUpdate.DateOfBirth;
                                userData.IdentityNumber = userUpdate.IdentityNumber;
                                if (userData.ProfilImage != "NULL")
                                {
                                    string path = "./Content/img/" + userData.ProfilImage;
                                    _fileService.FileDeleteToServer(path);
                                    fileName = _fileService.FileSaveToServer(userUpdate.ProfilImage, "./Content/img/");
                                }
                                else
                                {
                                    fileName = _fileService.FileSaveToServer(userUpdate.ProfilImage, "./Content/img/");
                                }
                                userData.ProfilImage = fileName;
                                _user.Update(userData);
                                return new SuccessResult("Kullanıcı Güncelleme İşelmi Başarılı.", 200);
                            }
                            catch (Exception ex)
                            {
                                return new ErrorResult(ex.Message, 404);

                            }
                        }
                        return new ErrorResult("Kullanıcı Boş", 404);
                    }
                    else
                    {
                        return new ErrorDataResult<List<CityTbl>>(null, "Lütfen Doğru Bir Şifre Giriniz", 401);
                    }
                }
            }
            return new ErrorDataResult<List<CityTbl>>(null, "Api ye Erişiminiz Bulunmuyor", 401);
        }
    }
}
