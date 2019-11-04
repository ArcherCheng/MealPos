using System;
using System.Collections.Generic;

namespace Ahr.Data.MealPos
{
    public partial class MealAddOn
    {
        public MealAddOn()
        {
            MealAddOnRela = new HashSet<MealAddOnRela>();
        }

        public int Id { get; set; }
        public string AddOnName { get; set; }
        public int AddPrice { get; set; }
        public DateTime? WriteTime { get; set; }
        public string WriteUser { get; set; }
        public string WriteIp { get; set; }

        public virtual ICollection<MealAddOnRela> MealAddOnRela { get; set; }
    }
}
