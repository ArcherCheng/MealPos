using System;
using System.Collections.Generic;

namespace Ahr.Data.MealPos
{
    public partial class Meal
    {
        public Meal()
        {
            MealAddOn = new HashSet<MealAddOn>();
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string MealName { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public string BarCode { get; set; }
        public int Cost { get; set; }
        public int BrandPrice { get; set; }
        public int BestPrice { get; set; }
        public string Notes { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string WriteUser { get; set; }
        public string WriteIp { get; set; }

        public virtual ICollection<MealAddOn> MealAddOn { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
