using Core.Utilities.Result.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.VerimorOtp
{
    public interface ICheckOtpSend
    {
        IResult CheckOtpSendMethod(CheckOtpDto checkOtpDto);
        IResult CheckOtpVerification(CheckOtpDto checkOtpDto);
    }
}
