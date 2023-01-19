using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CarAddDto
    {
        public string NumberPlate { get; set; }
        public int UserID { get; set; }
        public int RecipeID { get; set; }
        public int CityID { get; set; }
        public IFormFile[]? CarImages { get; set; }
    }
}
