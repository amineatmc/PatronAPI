using Business.Abstract;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Business.VerimorOtp
{
    public class CheckOtpSend : ICheckOtpSend
    {
        private readonly IOtpSend _otpSend;
        private readonly IUser _user;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string rootDir = "./Content/OtpMessage/";
        public CheckOtpSend(IOtpSend otpSend, IUser user)
        {
            _otpSend = otpSend;
            _user= user;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public IResult CheckOtpSendMethod(CheckOtpDto checkOtpDto)
        {
            //var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            //foreach (var key in header)
            //{
            //    if (key.Key == "apikey")
            //    {
                    //if (key.Value.ToString() == apiKey.keyValue.ToString())
                    //{
                        Random random = new Random();
                        int number = random.Next(100000, 900000);

                        VerimorOtpSend otpSend = new VerimorOtpSend();
                        otpSend.Mesaj = "Guvenliginiz icin onay kodunuzu kimse ile paylasmayiniz onay kodunuz: " + number;
                        otpSend.UserPhone = "90" + checkOtpDto.PhoneNumber;
                        var res = _otpSend.SendOtp(otpSend);

                        checkOtpDto.OtpMessage = $"{number}";
                        string serializeOtp = JsonConvert.SerializeObject(checkOtpDto);


                        if (!Directory.Exists(rootDir))
                        {
                            Directory.CreateDirectory(rootDir);
                        }

                        string fileName = $"{checkOtpDto.UserID}_{checkOtpDto.PhoneNumber}.json";
                        string filePath = Path.Combine(rootDir, fileName);
                        File.WriteAllText(filePath, serializeOtp);

                        return new SuccessResult(message: "Otp Gönderimi Başarılı", 200);
                    //}
                    //else
                    //{
                    //    return new ErrorDataResult<User>(null, "Api ye Erişiminiz Bulunmuyor", 401);
                    //}
            //    }
            //}
            //return new ErrorDataResult<User>(null, "Lütfen Doğru Bir Şifre Giriniz", 401);

        }
        
        public IResult CheckOtpVerification(CheckOtpDto checkOtpDto)
        {
            //var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            //foreach (var key in header)
            //{
            //    if (key.Key == "apikey")
            //    {
            //        if (key.Value.ToString() == apiKey.keyValue.ToString())
            //        {
                        if (Directory.Exists(rootDir))
                        {
                            string newfile = $"{rootDir}{checkOtpDto.UserID}_{checkOtpDto.PhoneNumber}.json";
                            string messageJson = File.ReadAllText(newfile);

                            CheckOtpDto readOtp = JsonConvert.DeserializeObject<CheckOtpDto>(messageJson);

                            if (checkOtpDto.OtpMessage == readOtp.OtpMessage && checkOtpDto.PhoneNumber == readOtp.PhoneNumber)
                            {
                                File.Delete(newfile);
                                User user = _user.Get(p=>p.UserPhone==checkOtpDto.PhoneNumber);
                                user.IsActive = true;
                                _user.Update(user);
                                return new SuccessResult(message: "Otp Eşlemesi Başarılı", statusCode: 200);
                            }
                                                       
                        }
                        return new ErrorResult(message: "Otp Eşlemesi Başarısız.", statusCode: 404);
                    //}
                    //else
                    //{
                    //    return new ErrorDataResult<User>(null, "Api ye Erişiminiz Bulunmuyor", 500);
                    //}
            //    }
            //}
            //return new ErrorDataResult<User>(null, "Lütfen Doğru Bir Şifre Giriniz", 500);

          
        }

    }
}
