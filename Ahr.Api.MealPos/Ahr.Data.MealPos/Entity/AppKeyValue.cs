using System;
using System.Collections.Generic;

namespace Ahr.Data.MealPos
{
    public partial class AppKeyValue
    {
        public int Id { get; set; }
        public string KeyGroup { get; set; }
        public string KeyValue { get; set; }
        public DateTime? WriteTime { get; set; }
        public string WriteUser { get; set; }
        public string WriteIp { get; set; }
    }
}
