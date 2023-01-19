using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class CarTbl : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string? NumberPlate { get; set; }
        public int UserID { get; set; }
        public int RecipesID { get; set; }
        public int CityID { get; set; }
        public DateTime? AddedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
