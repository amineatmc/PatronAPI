using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class TransactionsTbl:IEntity
    {
        [Key]
        public int ID { get; set; }
        public string? TableName { get; set; }
        public int DataID { get; set; }
        public string? DataDescription { get; set; }
        public string? UserLoginIp { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
