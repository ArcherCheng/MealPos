using System;
using System.Collections.Generic;

namespace Ahr.Data.MealPos
{
    public partial class Vender
    {
        public int Id { get; set; }
        public string VenderName { get; set; }
        public string EngName { get; set; }
        public string TaxId { get; set; }
        public string Operator { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string FaxNo { get; set; }
        public string PostNo { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string WriteUser { get; set; }
        public string WriteIp { get; set; }
    }
}
