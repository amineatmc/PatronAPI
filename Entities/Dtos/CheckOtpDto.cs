﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CheckOtpDto
    {
        public string? PhoneNumber { get; set; }
        public int? UserID { get; set; }
        public string? OtpMessage { get; set; }
    }
}
