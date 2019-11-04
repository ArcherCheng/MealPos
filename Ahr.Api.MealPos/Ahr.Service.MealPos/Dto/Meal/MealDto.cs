using Ahr.Data.MealPos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Service.MealPos
{
    public class MealDto
    {
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
        public ICollection<MealAddOnDto> MealAddOnDtos { get; set; }
    }

    public class MealAddOnDto
    {
        public int Id { get; set; }
        public string AddOnName { get; set; }
        public int AddPrice { get; set; }
    }
}
