using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Recipe:IEntity
    {
        public int Id { get; set; }
        public decimal acilisU { get; set; }
        public decimal indiBindiU { get; set; }
        public decimal yuzMetrelikMesafeU { get; set; }
        public decimal birDakikaBeklemeU { get; set; }
        public decimal birSaatBeklemeU { get; set; }
        public decimal birKmMesafeU { get; set; }
        
    }
}
