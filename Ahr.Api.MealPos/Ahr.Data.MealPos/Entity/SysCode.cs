using System;
using System.Collections.Generic;

namespace Ahr.Data.MealPos
{
    public partial class SysCode
    {
        public int Id { get; set; }
        public string CodeGroup { get; set; }
        public string CodeValue { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string WriteUser { get; set; }
        public string WriteIp { get; set; }
    }
}
