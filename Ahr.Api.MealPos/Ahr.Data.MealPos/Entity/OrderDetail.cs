using System;
using System.Collections.Generic;

namespace Ahr.Data.MealPos
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int MealId { get; set; }
        public int? MealAddOnId { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public int TotalAmt { get; set; }
        public string Notes { get; set; }
        public DateTime? WriteTime { get; set; }
        public string WriteUser { get; set; }
        public string WriteIp { get; set; }

        public virtual OrderMaster Master { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
