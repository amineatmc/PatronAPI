using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class PermitImageTbl:IEntity
    {
        [Key]
        public int PermitID { get; set; }
        public int CarID { get; set; }
        public string? PermitPath { get; set; }
    }
}
