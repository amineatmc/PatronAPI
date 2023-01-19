using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CarListDto
    {
        public int ID { get; set; }
        public string NumberPlate { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public DateTime? AddedAt { get; set; }
        public bool IsActive { get; set; }
        public List<PermitImageTbl> CarImages { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
