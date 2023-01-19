using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class LDataTbl : IEntity
    {
        [Key]
        public int LDataId { get; set; }
        public string _id { get; set; }
        public string deviceId { get; set; }
        public string driverId { get; set; }
        public string travelId { get; set; }
        public string plate { get; set; }
        public string statu { get; set; }
        public string price { get; set; }
        public string distance { get; set; }
        public string processId { get; set; }
        public DateTime startTime { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int year { get; set; }
        public int week { get; set; }
        public DateTime created { get; set; }
        public int __v { get; set; }
        public string duration { get; set; }
        public DateTime finishTime { get; set; }
    }
}
