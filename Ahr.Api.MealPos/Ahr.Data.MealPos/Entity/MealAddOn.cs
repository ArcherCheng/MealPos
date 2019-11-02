using System;
using System.Collections.Generic;

namespace Ahr.Data.MealPos
{
    public partial class MealAddOn
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public string Descriptions { get; set; }
        public int AddPrice { get; set; }
        public string Notes { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string WriteUser { get; set; }
        public string WriteIp { get; set; }

        public virtual Meal Meal { get; set; }
    }
}
