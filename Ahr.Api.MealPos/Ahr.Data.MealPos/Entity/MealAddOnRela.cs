using System;
using System.Collections.Generic;

namespace Ahr.Data.MealPos
{
    public partial class MealAddOnRela
    {
        public int MealId { get; set; }
        public int AddOnId { get; set; }
        public DateTime? WriteTime { get; set; }
        public string WriteUser { get; set; }
        public string WriteIp { get; set; }

        public virtual MealAddOn AddOn { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
