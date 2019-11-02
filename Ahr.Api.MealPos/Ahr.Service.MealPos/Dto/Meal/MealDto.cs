using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Service.MealPos
{
    public class MealDto
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public string BarCode { get; set; }
        public int Cost { get; set; }
        public int BrandPrice { get; set; }
        public int BestPrice { get; set; }
        public string Notes { get; set; }
    }
}
