using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserUpdateDto
    {
        public string Username { get; set; }
        public string Usersurname { get; set; }
        public string UserPhone { get; set; }
        public string IdentityNumber { get; set; }
        public IFormFile? ProfilImage { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
