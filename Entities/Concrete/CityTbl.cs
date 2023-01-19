using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CityTbl : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string? CityPlate { get; set; }
        public string? CityName { get; set; }
    }
}
