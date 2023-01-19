using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserForgetPassword
    {
        public string UserPhoneNumber { get; set; }
        public string ForgetPassword { get; set; }
    }
}
