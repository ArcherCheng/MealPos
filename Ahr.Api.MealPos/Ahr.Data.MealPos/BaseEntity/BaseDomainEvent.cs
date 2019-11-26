using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Data
{
    public class BaseDomainEvent
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.Now;
    }
}
