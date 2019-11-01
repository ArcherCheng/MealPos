using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Infra
{
    public class BaseDomainEvent
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.Now;
    }
}
