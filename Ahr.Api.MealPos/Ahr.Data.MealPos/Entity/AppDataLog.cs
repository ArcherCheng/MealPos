using System;
using System.Collections.Generic;

namespace Ahr.Data.MealPos
{
    public partial class AppDataLog
    {
        public long Id { get; set; }
        public string TableName { get; set; }
        public string InsertData { get; set; }
        public string DeleteData { get; set; }
        public byte WriteType { get; set; }
        public DateTime? WriteTime { get; set; }
    }
}
