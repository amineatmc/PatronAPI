using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Business.VerimorOtp;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Hashing;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IUser _user;
        private readonly ICheckOtpSend _checkOtpSent;
        private readonly IFileService _fileService;
        private readonly ITokenHelper _tokenHelper;
        private readonly ITransactionsService _transactionsService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUser user, IFileService fileService, ITransactionsService transactionsService, ICheckOtpSend checkOtpSent)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _user = user;
            _checkOtpSent = checkOtpSent;
            _fileService = fileService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _transactionsService = transactionsService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user,/* user.UserID,*/ claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.SuccesFullLogin, 200);
        }

        public IDataResult<User> Login(UserForLogin userForLogin)
        {
          //  var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            //foreach (var key in header)
            //{
            //    if (key.Key == "apikey")

            //    {
                    //if (key.Value.ToString() == apiKey.keyValue.ToString())
                    //{
                        var userToCheck = _userService.GetUserPhone(userForLogin.UserPhone);
                        if (userToCheck == null)
                        {
                            return new ErrorDataResult<User>(null, Messages.UserNotFound, 404);
                        }
                        if (userToCheck.IsActive == false)
                        {
                            return new ErrorDataResult<User>(null, Messages.PasswordError, 404);
                        }
                        if (!HashingHelper.VerifyPasswordHash(userForLogin.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
                        {
                            return new ErrorDataResult<User>(null, Messages.PasswordError, 404);
                        }

                        var actionAdd = new TransactionsDto()
                        {
                            CreatedDate = DateTime.Now,
                            DataID = userToCheck.UserID,
                            TableName = "User",
                            DataDescription = $"Telefon Numarası: {userToCheck.UserPhone} , {userToCheck.UserName + " " + userToCheck.UserSurname} isimli ve numaralı bu kullanıcı tarih: {DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute} giriş yaptı.",
                            UserLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                        };
                        _transactionsService.Add(actionAdd);
            return new SuccessDataResult<User>(userToCheck, Messages.SuccesFullLogin, 200);
            //}
            //else
            //{
            //    return new ErrorDataResult<User>(null, "Lütfen Doğru Bir Şifre Giriniz", 401);
            //}
            //    }
            //}
            //return new ErrorDataResult<User>(null, "Api ye Erişiminiz Bulunmuyor", 401);
        }
       // [TransactionScopeAspect]
        public IDataResult<User> Register(UserForRegister userForRegister, string Password)
        {
            //var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            //foreach (var key in header)
            //{
            //    if (key.Key == "apikey")
            //    {
            //        if (key.Value.ToString() == apiKey.keyValue.ToString())
            //        {
                        var userToCheck = _userService.GetUserPhone(userForRegister.UserPhone);
                        if (userToCheck == null)
                        {
                            string fileName;
                            ValidationTool.Validate(new UserValidator(), userForRegister);
                            if (userForRegister.ProfilImage != null)
                            {
                                fileName = _fileService.FileSaveToServer(userForRegister.ProfilImage, "./Content/img/");
                            }
                            else
                            {
                                fileName = "NULL";
                            }

                            byte[] passwordHash, passwordSalt;
                            HashingHelper.CratePasswordHash(Password, out passwordHash, out passwordSalt);

                            var user = new User()
                            {
                                UserName = userForRegister.Username,
                                UserSurname = userForRegister.Usersurname,
                                UserPhone = userForRegister.UserPhone,
                                IdentityNumber = userForRegister.IdentityNumber,
                                DateOfBirth = userForRegister.DateOfBirth,
                                AddedAt = DateTime.Now,
                                ProfilImage = fileName,
                                PasswordHash = passwordHash,
                                PasswordSalt = passwordSalt,
                                IsActive = false
                            };
                            _userService.Add(user);
                            var checkotp = new CheckOtpDto
                            {
                                PhoneNumber = userForRegister.UserPhone,
                                UserID = user.UserID
                            };
                            var sent = _checkOtpSent.CheckOtpSendMethod(checkotp);
                            if (sent.Success)
                            {
                                return new SuccessDataResult<User>(user, Messages.UserRegisterSuccess, 200);

                            }
                            var actionAdd = new TransactionsDto()
                            {
                                CreatedDate = DateTime.Now,
                                DataID = user.UserID,
                                TableName = "User",
                                DataDescription = $"Telefon Numarası: {user.UserPhone} , {user.UserName + " " + user.UserSurname} isimli ve numaralı bu kullanıcı tarih: {DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute} Kayıt Oldu.",
                                UserLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                            };
                            _transactionsService.Add(actionAdd);
                        }
                        
                       else if (userToCheck.IsActive == false)
                        {

                            var checkotp = new CheckOtpDto
                            {
                                PhoneNumber = userForRegister.UserPhone,
                                UserID = userToCheck.UserID
                            };
                            var sent = _checkOtpSent.CheckOtpSendMethod(checkotp);
                            if (sent.Success)
                            {
                                return new SuccessDataResult<User>();

                            }
                        }
            else
            {
                return new ErrorDataResult<User>(null, message: "Bu telefon numarası sistemde kayıtlıdır!.", 400);
            }


            //}


            //}
            //    else
            //    {
            //        return new ErrorDataResult<User>(null, message: "Lütfen Doğru Bir Şifre Giriniz", 401);
            //    }
            //}
            return new ErrorDataResult<User>(null, "Api ye Erişiminiz Bulunmuyor", 401);

        }

        public IResult UserExists(string phone)
        {
            if (_userService.GetUserPhone(phone) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists, 400);
            }
            return new SuccessResult();
        }

        public IResult ChangePassword(UserChangePassword userChangePassword)
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            foreach (var key in header)
            {
                if (key.Key == "apikey")
                {
                    if (key.Value.ToString() == apiKey.keyValue.ToString())
                    {
                        var user = _user.Get(p => p.UserID == userChangePassword.UserId);
                        bool result = HashingHelper.VerifyPasswordHash(userChangePassword.CurrentPassword, user.PasswordHash, user.PasswordSalt);
                        if (!result)
                        {
                            return new ErrorResult(Messages.WrongCurrentPassword, 404);
                        }
                        byte[] passwordHash, passwordSalt;
                        HashingHelper.CratePasswordHash(userChangePassword.NewPassword, out passwordHash, out passwordSalt);
                        user.PasswordHash = passwordHash;
                        user.PasswordSalt = passwordSalt;
                        _user.Update(user);
                        var actionAdd = new TransactionsDto()
                        {
                            CreatedDate = DateTime.Now,
                            DataID = user.UserID,
                            TableName = "User",
                            DataDescription = $"Telefon Numarası: {user.UserPhone} , {user.UserName + " " + user.UserSurname} isimli ve numaralı bu kullanıcı tarih: {DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute} Sifre Guncelleme Islemi Yaptı.",
                            UserLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                        };
                        _transactionsService.Add(actionAdd);
                        return new SuccessResult(Messages.PasswordChange, 200);
                    }
                    else
                    {
                        return new ErrorDataResult<User>(null, "Lütfen Doğru Bir Şifre Giriniz", 401);
                    }
                }
            }
            return new ErrorDataResult<User>(null, "Api ye Erişiminiz Bulunmuyor", 401);

        }

        public IResult ForgetPassword(UserForgetPassword userForgetPassword)
        {

            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            foreach (var key in header)
            {
                if (key.Key == "apikey")
                {
                    if (key.Value.ToString() == apiKey.keyValue.ToString())
                    {
                        var userToCheck = _userService.GetUserPhone(userForgetPassword.UserPhoneNumber);
                        if (userToCheck == null)
                        {
                            return new ErrorResult(Messages.ForgetPasswordIsNoRecord, 404);
                        }
                        byte[] passwordHash, passwordSalt;
                        HashingHelper.CratePasswordHash(userForgetPassword.ForgetPassword, out passwordHash, out passwordSalt);
                        userToCheck.PasswordHash = passwordHash;
                        userToCheck.PasswordSalt = passwordSalt;
                        _user.Update(userToCheck);
                        var actionAdd = new TransactionsDto()
                        {
                            CreatedDate = DateTime.Now,
                            DataID = userToCheck.UserID,
                            TableName = "User",
                            DataDescription = $"Telefon Numarası: {userToCheck.UserPhone} , {userToCheck.UserName + " " + userToCheck.UserSurname} isimli ve numaralı bu kullanıcı tarih: {DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute} Yeni Sifre Alma Islemi Yaptı.",
                            UserLoginIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                        };
                        _transactionsService.Add(actionAdd);
                        return new SuccessResult(Messages.ForgetPasswordChange, 200);
                    }
                    else
                    {
                        return new ErrorDataResult<User>(null, "Lütfen Doğru Bir Şifre Giriniz", 401);
                    }
                }
            }
            return new ErrorDataResult<User>(null, "Api ye Erişiminiz Bulunmuyor", 401);

        }
    }
}
