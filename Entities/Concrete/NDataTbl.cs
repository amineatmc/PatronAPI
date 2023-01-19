using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class NDataTbl : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int fiyat { get; set; }
        public int toplamSure { get; set; }
        public int yillikSure { get; set; }
        public int aylikSure { get; set; }
        public int haftalikSure { get; set; }
        public int gunlukSure { get; set; }
        public decimal doluMesafe { get; set; }
        public int gunlukFiyat { get; set; }
        public int haftalikFiyat { get; set; }
        public decimal gunlukDoluMesafe { get; set; }
        public int aylikFiyat { get; set; }
        public decimal aylikDoluMesafe { get; set; }
        public int yillikFiyat { get; set; }
        public decimal yillikDoluMesafe { get; set; }
        public decimal haftalikDoluMesafe { get; set; }
        public int gunlukSayi { get; set; }
        public int yillikSayi { get; set; }
        public int aylikSayi { get; set; }
        public int haftalikSayi { get; set; }
        public int toplamSayi { get; set; }
    }
}
