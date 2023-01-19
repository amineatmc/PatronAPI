using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class FinishLocation : IEntity
    {
        [Key]
        public int FLocationId { get; set; }
        public string type { get; set; }
        public string coordinates { get; set; }
    }
}
