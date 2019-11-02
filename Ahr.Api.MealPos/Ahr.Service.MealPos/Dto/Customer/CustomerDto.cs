using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EngName { get; set; }
        public string TaxNo { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string FaxNo { get; set; }
        public string PostNo { get; set; }
        public string Address { get; set; }
        public string Operator { get; set; }
        public string Notes { get; set; }
    }
}
