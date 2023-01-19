using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class TransactionsDto
    {
        public string TableName { get; set; }
        public int DataID { get; set; }
        public string DataDescription { get; set; }
        public string UserLoginIp { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
