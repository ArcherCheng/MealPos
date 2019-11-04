using System;
using System.Collections.Generic;

namespace Ahr.Data.MealPos
{
    public partial class Meal
    {
        public Meal()
        {
            MealAddOnRela = new HashSet<MealAddOnRela>();
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string MealName { get; set; }
        public string MealType { get; set; }
        public string Unit { get; set; }
        public string BarCode { get; set; }
        public int Cost { get; set; }
        public int BrandPrice { get; set; }
        public int SalePrice { get; set; }
        public int CookMinutes { get; set; }
        public string Notes { get; set; }
        public DateTime? WriteTime { get; set; }
        public string WriteUser { get; set; }
        public string WriteIp { get; set; }

        public virtual ICollection<MealAddOnRela> MealAddOnRela { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
